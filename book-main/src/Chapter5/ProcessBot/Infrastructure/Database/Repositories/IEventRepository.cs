using ProcessBot.Infrastructure.Database.Entities;

namespace ProcessBot.Infrastructure.Database.Repositories;

public interface IEventRepository
{
    public Task<IEnumerable<Event>> GetUnprocessedAsync(IEnumerable<string> entitiesIds);
    public Task MarkAsProcessedAsync(long eventId);
}