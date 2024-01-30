namespace ProcessBot.Infrastructure.Database.Entities;

public class ProcessingQueueElement
{
    public string EntityId;
    public int NodeId;
    public DateTime ProcessingTime;
    public DateTime Timestamp;
}