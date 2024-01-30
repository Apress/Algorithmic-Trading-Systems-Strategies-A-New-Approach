using DapperExample;
using DapperExample.Repositories;


Migrator.Up(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")!);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IExchangeOrderRepository, ExchangeOrderRepository>();

var app = builder.Build();
app.Run();
