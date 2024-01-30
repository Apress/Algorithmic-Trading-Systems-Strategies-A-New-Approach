using Backworker;
using Backworker.Example.Backworker;
using Backworker.Example.Backworker.Tasks;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddLogging(c =>
    {
        c.AddConsole().SetMinimumLevel(LogLevel.Trace);
    });
    
    services.AddBackworker(
        options =>
        {
            options.DbConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
        }, 
        typeof(BackworkerTaskFactory), 
        typeof(SayHelloTask).Assembly);
});

var app = builder.Build();

app.Run();