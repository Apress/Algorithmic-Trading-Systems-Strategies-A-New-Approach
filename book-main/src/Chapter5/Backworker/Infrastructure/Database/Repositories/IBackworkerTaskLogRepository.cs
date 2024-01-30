using Backworker.Infrastructure.Database.Entities;

namespace Backworker.Infrastructure.Database.Repositories;

public interface IBackworkerTaskLogRepository
{
    public Task UpsertStartAsync(int taskId);
    public Task UpsertStopAsync(int taskId);
}