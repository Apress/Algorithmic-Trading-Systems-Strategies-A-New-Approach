using Backworker.Interfaces;

namespace Backworker.Example.Backworker.Tasks;

public class SaySomethingTask : IBackworkerTaskAct
{
    private readonly ILogger<SaySomethingTask> _logger;

    public SaySomethingTask(ILogger<SaySomethingTask> logger)
    {
        _logger = logger;
    }

    public Task RunAsync(string magicString, CancellationToken cancellationToken)
    {
        _logger.LogInformation(magicString);
        return Task.CompletedTask;
    }
}