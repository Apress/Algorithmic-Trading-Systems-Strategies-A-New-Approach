namespace ProcessBot.Infrastructure.Database.Entities;

public class Node
{
    public int Id;
    public int Code;
    public string Name;
    public bool ItsParent;
    public int ParentId;
    public NodeType? Type;
    public bool Fast;
    public string Params;
    public int WaitingSeconds;
    public int ActId;
    public int EventTypeId;
    public bool Deleted;
}

public enum NodeType
{
    Act = 1,
    Waiting = 2,
    Terminal = 3,
    Trigger = 4,
    Description = 5
}