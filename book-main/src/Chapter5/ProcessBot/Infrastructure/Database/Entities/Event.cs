namespace ProcessBot.Infrastructure.Database.Entities;

public class Event
{
    public long Id;
    public string EntityId;
    public int Type;
    public string? Message;
    public DateTime CreatedAt;
    public DateTime? ProcessedAt;
}