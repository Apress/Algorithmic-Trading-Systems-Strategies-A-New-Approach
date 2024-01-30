using ProcessBot.Infrastructure.Database.Entities;

namespace ProcessBot.Infrastructure.Database.Repositories;

public interface IProcessingQueueRepository
{
    public Task<List<ProcessingQueueElement>> GetAndLockEntitiesAsync(int count);
    public Task UnlockEntitiesAsync(IEnumerable<string> entitiesIds);
    public Task UpsertAsync(string entityId, int nodeId, DateTime? processingTime = null);
    public Task RemoveAsync(string entityId, int nodeId);
}