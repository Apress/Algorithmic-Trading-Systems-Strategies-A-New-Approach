using ProcessBot.Infrastructure.Database.Entities;

namespace ProcessBot.Services;

public class ProcessMap
{
    private List<Node> _nodes;
    
    public ProcessMap(IEnumerable<Node> nodes)
    {
        _nodes = nodes.ToList();
    }

public int? GetNextNodeId(int nodeId, int eventType)
{
    Node currentNode = GetNode(nodeId);
    Node? nextNode =
        _nodes
            .Where(n =>
                n.Deleted == false
                && n.ParentId == currentNode.ParentId
                && n.Type == NodeType.Trigger
                && n.EventTypeId == eventType
                && n.Code <= currentNode.Code)
            .MinBy(n => n.Code);

    return nextNode?.Id;
}

public int? GetNextNodeId(int nodeId)
{
    Node currentNode = GetNode(nodeId);
    Node? nextNode =
        _nodes
            .Where(n =>
                n.Deleted == false
                && n.ParentId == currentNode.ParentId
                && n.Code <= currentNode.Code)
            .MinBy(n => n.Code);

    return nextNode?.Id;
}

    public Node GetNode(int nodeId)
    {
        return _nodes.First(n => n.Id == nodeId);
    }
}