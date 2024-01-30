using System.Data.Common;
using Npgsql;

namespace DapperExample.Repositories;

public class DbConnector
{
    private NpgsqlConnection dbConnection => new (Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));
    
    public async Task<T> PerformDbActionAsync<T>(Func<DbConnection, Task<T>> dbAction)
    {
        try
        {
            await using var connection = dbConnection;
            await connection.OpenAsync();

            await using var dbTransaction = await connection.BeginTransactionAsync();
            try
            {
                T actionResult = await dbAction.Invoke(connection);
                await dbTransaction.CommitAsync();
                return actionResult;
            }
            catch (Exception)
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }
        finally
        {
            dbConnection.Dispose();
        }
    }
}