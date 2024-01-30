using DapperExample.Migrations;
using FluentMigrator.Runner;

namespace DapperExample;

public static class Migrator
{
    public static void Up(string connectionString)
    {
        var services = new ServiceCollection();
        services.AddFluentMigratorCore()
            .ConfigureRunner(configure => configure
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Migration_202312171002).Assembly).For.Migrations());
        
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var migratorRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
        migratorRunner.MigrateUp();
    }
    
}