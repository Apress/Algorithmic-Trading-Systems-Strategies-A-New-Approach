using ProcessBot.Infrastructure.Database.Entities;

namespace ProcessBot.Infrastructure.Database.Repositories;

public interface INodeRepository
{
    public Task<IEnumerable<Node>> GetAllAsync();
}