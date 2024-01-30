using ProcessBot;
using ProcessBot.Example.ProcessBot;
using ProcessBot.Example.ProcessBot.Acts;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddLogging(c =>
    {
        c.AddConsole().SetMinimumLevel(LogLevel.Trace);
    });
    
    services.AddProcessBot(
        options =>
        {
            options.DbConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
        }, 
        typeof(ProcessActFactory), 
        typeof(SayHelloAct).Assembly);
});

var app = builder.Build();

app.Run();