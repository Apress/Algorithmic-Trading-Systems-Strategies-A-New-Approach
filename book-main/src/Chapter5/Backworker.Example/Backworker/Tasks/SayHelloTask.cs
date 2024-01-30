using Backworker.Interfaces;

namespace Backworker.Example.Backworker.Tasks;

public class SayHelloTask : IBackworkerTaskAct
{
    private readonly ILogger<SayHelloTask> _logger;

    public SayHelloTask(ILogger<SayHelloTask> logger)
    {
        _logger = logger;
    }

    public Task RunAsync(string magicString, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello");
        return Task.CompletedTask;
    }
}