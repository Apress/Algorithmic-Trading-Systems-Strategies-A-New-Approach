using Dapper.Contrib.Extensions;

namespace DapperExample.Entities;

[Table("\"ExchangeOrders\"")]
public class ExchangeOrder
{
    public int id { get; set; }
    public int ExchangeId { get; set; }
    public int InstrumentId { get; set; }
    public decimal Amount { get; set; }
    
    public IEnumerable<ExchangeDeal> Deals;
}