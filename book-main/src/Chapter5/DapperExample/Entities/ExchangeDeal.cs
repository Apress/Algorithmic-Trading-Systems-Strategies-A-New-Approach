using Dapper.Contrib.Extensions;

namespace DapperExample.Entities;

[Table("\"ExchangeDeals\"")]
public class ExchangeDeal
{
    public int ExchangeOrderId { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}