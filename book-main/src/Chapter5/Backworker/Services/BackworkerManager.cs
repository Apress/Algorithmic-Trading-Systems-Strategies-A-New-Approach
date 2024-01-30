using Backworker.Infrastructure.Database.Entities;
using Backworker.Infrastructure.Database.Repositories;
using Backworker.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backworker.Services;

public class BackworkerManager
{
    private readonly IBackworkerTaskRepository _backworkerTaskRepository;
    private readonly IBackworkerTaskLogRepository _backworkerTaskLogRepository;
    private readonly IBackworkerTaskFactory _backworkerTaskFactory;
    private readonly ILogger<BackworkerManager> _logger;

    public BackworkerManager(
        IBackworkerTaskRepository backworkerTaskRepository,
        IBackworkerTaskLogRepository backworkerTaskLogRepository,
        IBackworkerTaskFactory backworkerTaskFactory,
        ILogger<BackworkerManager> logger)
    {
        _backworkerTaskRepository = backworkerTaskRepository;
        _backworkerTaskLogRepository = backworkerTaskLogRepository;
        _backworkerTaskFactory = backworkerTaskFactory;
        _logger = logger;
    }
    
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        BackworkerTask? backworkerTask = 
            await _backworkerTaskRepository.GetAndLockStartBackworkerTaskAsync();

        if (backworkerTask == null)
            return;
    
        try
        {
            await _backworkerTaskLogRepository
                .UpsertStartAsync(backworkerTask.Id);
        
            IBackworkerTaskAct? task = 
                _backworkerTaskFactory.GetTask(backworkerTask.Type);
            await task.RunAsync(backworkerTask.MagicString, cancellationToken);
        
            await _backworkerTaskLogRepository
                .UpsertStopAsync(backworkerTask.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, 
                $"Unknown error run backworker task {backworkerTask}");
        }

        await _backworkerTaskRepository
            .UnlockBackworkerTask(backworkerTask.Id);
    }
}