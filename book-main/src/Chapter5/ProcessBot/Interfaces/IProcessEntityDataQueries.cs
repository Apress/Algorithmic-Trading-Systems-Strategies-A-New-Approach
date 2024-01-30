namespace ProcessBot.Interfaces;

public interface IProcessEntityDataQueries
{
    Task<IEnumerable<ProcessEntityData>> GetDataAsync(IEnumerable<string> entitiesIds);
}