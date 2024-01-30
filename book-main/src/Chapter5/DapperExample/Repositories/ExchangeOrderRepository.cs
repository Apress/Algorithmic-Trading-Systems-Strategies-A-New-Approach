using DapperExample.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DapperExample.Repositories;

public class ExchangeOrderRepository: IExchangeOrderRepository
{
    private readonly DbConnector _dbConnector;

    public ExchangeOrderRepository(
        DbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }
    
    public Task<int> InsertOrderAsync(ExchangeOrder exchangeOrder)
    {
        return _dbConnector.PerformDbActionAsync(connection => connection.InsertAsync(exchangeOrder));
    }

    public Task InsertExchangeDealAsync(ExchangeDeal exchangeDeal)
    {
        return _dbConnector.PerformDbActionAsync(connection => connection.InsertAsync(exchangeDeal));
    }
    
    public Task<ExchangeOrder> GetOrderAsync(int id)
    {
        string sqlOrders = "select * from \"ExchangeOrders\" where id = @id";
        string sqlDeals = "select * from \"ExchangeDeals\" where \"ExchangeOrderId\" = @id";
        
        return _dbConnector.PerformDbActionAsync(async connection =>
        {
            var multipleResult = await connection.QueryMultipleAsync($"{sqlOrders};{sqlDeals}", new {id});
            ExchangeOrder exchangeOrder = await multipleResult.ReadFirstOrDefaultAsync<ExchangeOrder>();
            if (exchangeOrder == null) 
                return null;
            
            IEnumerable<ExchangeDeal> deals = await multipleResult.ReadAsync<ExchangeDeal>();
            exchangeOrder.Deals = deals;
            return exchangeOrder;
        });
    }
}