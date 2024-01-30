using Microsoft.Extensions.Logging;
using ProcessBot.Infrastructure.Database.Entities;
using ProcessBot.Infrastructure.Database.Repositories;
using ProcessBot.Interfaces;
using ProcessBot.Services;

namespace ProcessBot;

public class ProcessBot
{
    private readonly int _maxEntitiesCount = 10;
    private readonly INodeRepository _nodeRepository;
    private readonly IProcessingQueueRepository _processingQueueRepository;
    private readonly IProcessEntityDataQueries _processEntityDataQueries;
    private readonly IEventRepository _eventRepository;
    private readonly IProcessActFactory _processActFactory;
    private readonly ILogger<ProcessBot> _logger;

    public ProcessBot(
        INodeRepository nodeRepository,
        IProcessingQueueRepository processingQueueRepository,
        IProcessEntityDataQueries processEntityDataQueries,
        IEventRepository eventRepository,
        IProcessActFactory processActFactory,
        ILogger<ProcessBot> logger)
    {
        _nodeRepository = nodeRepository;
        _processingQueueRepository = processingQueueRepository;
        _processEntityDataQueries = processEntityDataQueries;
        _eventRepository = eventRepository;
        _processActFactory = processActFactory;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        List<ProcessingQueueElement> queueElements = await _processingQueueRepository.GetAndLockEntitiesAsync(_maxEntitiesCount);
        if (!queueElements.Any())
            return;

        IEnumerable<Node> nodes = await _nodeRepository.GetAllAsync();
        ProcessMap processMap = new ProcessMap(nodes);
        await HandleEntitiesAsync(processMap, queueElements);
        await _processingQueueRepository.UnlockEntitiesAsync(queueElements.ConvertAll(e => e.EntityId));
    }

    private async Task HandleEntitiesAsync(ProcessMap processMap, List<ProcessingQueueElement> queueElements)
    {
        await HandleEventsAsync(processMap, queueElements);
        await HandleProcessingQueueAsync(processMap, queueElements);
    }

    private async Task HandleEventsAsync(ProcessMap processMap, List<ProcessingQueueElement> queueElements)
    {
        List<string> entitiesIds = queueElements.ConvertAll(e => e.EntityId);
        List<Event> unprocessedEvents = 
            (await _eventRepository.GetUnprocessedAsync(entitiesIds))
            .ToList();
        foreach (ProcessingQueueElement queueElement in queueElements)
        {
            var entityEvents =
                unprocessedEvents
                    .Where(e => e.EntityId == queueElement.EntityId)
                    .OrderBy(e => e.CreatedAt);
            foreach (Event entityEvent in entityEvents)
            {
                int? nextNodeId = processMap
                    .GetNextNodeId(queueElement.NodeId, entityEvent.Type);
                if (!nextNodeId.HasValue)
                    continue;

                await _eventRepository
                    .MarkAsProcessedAsync(entityEvent.Id);
                await _processingQueueRepository
                    .UpsertAsync(queueElement.EntityId, nextNodeId.Value);
            }
        }
    }
    
    private async Task HandleProcessingQueueAsync(
        ProcessMap processMap, 
        List<ProcessingQueueElement> queueElements)
    {
        IEnumerable<ProcessEntityData> entitiesData = 
            await _processEntityDataQueries
                .GetDataAsync(queueElements.ConvertAll(e => e.EntityId));
        List<ProcessEntityData> entitiesDataList = entitiesData.ToList();
        foreach (ProcessingQueueElement queueElement in queueElements)
        {
            try
            {
                int? nextNodeId = processMap.GetNextNodeId(queueElement.NodeId);
                if (nextNodeId == null)
                    throw new Exception("next node is empty");
                var entityData = entitiesDataList.First(d => d.Id == queueElement.EntityId);
                await MoveAsync(
                    processMap, 
                    nextNodeId, 
                    entityData, 
                    queueElement);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"processing entity error. Entity id '${queueElement.EntityId}'");
            }
        }
    }

    private async Task MoveAsync(
        ProcessMap processMap,
        int? nodeId,
        ProcessEntityData entityData,
        ProcessingQueueElement queueElement)
    {
        if (!nodeId.HasValue)
            return;

        Node node = processMap.GetNode(nodeId.Value);
        (bool move, int? nextNodeId) = node.Type switch
        {
            NodeType.Act=> await MakeActAsync(processMap, node, entityData),
            NodeType.Waiting => await MakeWaiting(processMap, node, entityData, queueElement),
            NodeType.Terminal => await MakeTerminalAsync(processMap, node, entityData),
            NodeType.Trigger => MakeNextStep(processMap, node),
            NodeType.Description => MakeNextStep(processMap, node),
            null when node.ItsParent => MakeNextStep(processMap, node),
            _ => throw new Exception($"unknown node type {node.Type}")
        };

        if (nextNodeId.HasValue)
            await _processingQueueRepository.UpsertAsync(entityData.Id, nextNodeId.Value);

        move = move && node.Fast;
        if (move)
            await MoveAsync(processMap, nextNodeId, entityData, queueElement);
    }

    private async Task<(bool move, int? nextNodeId)> MakeActAsync(
        ProcessMap processMap, 
        Node node, 
        ProcessEntityData entityData)
    {
        try
        {
            IProcessAct? act = _processActFactory.GetAct(node.ActId);
            if (act == null)
                throw new Exception($"unknown act with id {node.ActId}");
            
            (bool move, int? nextNodeId) = 
                await act.MakeAsync(node.Params, entityData);
            if (move && !nextNodeId.HasValue)
                return MakeNextStep(processMap, node);

            return (move, nextNodeId);
        }
        catch (Exception e)
        {
            _logger.LogError(
                e, 
                $"unknown error processing error entityId {entityData.Id} nodeId {node.Id}");
            return (false, null);
        }
    }
    
    private async Task<(bool move, int? nextNodeId)> MakeWaiting(
        ProcessMap processMap, 
        Node node, 
        ProcessEntityData entityData, 
        ProcessingQueueElement queueElement)
    {
        if (queueElement.NodeId == node.Id)
            return MakeNextStep(processMap, node);

        DateTime processingTime = DateTime.UtcNow
            .AddSeconds(node.WaitingSeconds);
        await _processingQueueRepository
            .UpsertAsync(entityData.Id, node.Id, processingTime);
        return (false, null);
    }
    
    private async Task<(bool move, int? nextNodeId)> MakeTerminalAsync(
        ProcessMap processMap, 
        Node node, 
        ProcessEntityData entityData)
    {
        await _processingQueueRepository.RemoveAsync(entityData.Id, node.Id);
        return (false, null);
    }
    
    private (bool move, int? nextNodeId) MakeNextStep(
        ProcessMap processMap, 
        Node node)
    {
        int? nextNodeId = processMap.GetNextNodeId(node.Id);
        return (true, nextNodeId);
    }
}















