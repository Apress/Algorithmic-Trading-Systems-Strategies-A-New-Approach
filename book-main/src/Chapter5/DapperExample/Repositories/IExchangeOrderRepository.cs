using DapperExample.Entities;

namespace DapperExample.Repositories;

public interface IExchangeOrderRepository
{
    Task<int> InsertOrderAsync(ExchangeOrder exchangeOrder);
    Task InsertExchangeDealAsync(ExchangeDeal exchangeDeal);
    Task<ExchangeOrder> GetOrderAsync(int id);
}