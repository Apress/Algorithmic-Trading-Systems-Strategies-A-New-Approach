using Backworker.Infrastructure.Database.Entities;

namespace Backworker.Infrastructure.Database.Repositories;

public interface IBackworkerTaskRepository
{
    public Task<BackworkerTask?> GetAndLockStartBackworkerTaskAsync();
    public Task UnlockBackworkerTask(int taskId);
}