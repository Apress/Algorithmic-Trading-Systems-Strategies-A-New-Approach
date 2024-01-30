using FluentMigrator;

namespace DapperExample.Migrations;

[Migration(202312171002)]
public class Migration_202312171002: Migration
{
    public override void Up()
    {
        Create.Table("ExchangeOrders")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("ExchangeId").AsInt32().NotNullable()
            .WithColumn("InstrumentId").AsInt32().NotNullable()
            .WithColumn("Amount").AsDecimal(20, 10).NotNullable();

        Create.Table("ExchangeDeals")
            .WithColumn("ExchangeOrderId").AsInt32().NotNullable()
            .WithColumn("Amount").AsDecimal(20, 10).NotNullable()
            .WithColumn("Price").AsDecimal(20, 10).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("ExchangeOrders");
        Delete.Table("ExchangeDeals");
    }
}