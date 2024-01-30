using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProcessBot;

public class ProcessBotHostedService : IHostedService
{
    private readonly ProcessBot _processBot;
    private readonly ILogger<ProcessBotHostedService> _logger;
    
    private bool _stop;

    public ProcessBotHostedService(
        ProcessBot processBot,
        ILogger<ProcessBotHostedService> logger)
    {
        _processBot = processBot;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ProcessBot start");
        _stop = false;

        Stopwatch stopwatch = Stopwatch.StartNew();
        while (!_stop && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                stopwatch.Restart();

                await _processBot.RunAsync(cancellationToken);

                stopwatch.Stop();

                var sleepTime = 1000 - (int)stopwatch.ElapsedMilliseconds;
                if (sleepTime > 0 && !_stop)
                    await Task.Delay(sleepTime, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "unknown error");
                await Task.Delay(5000, cancellationToken);
            }
        }

        _logger.LogInformation("ProcessBot stop");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => _stop = true, cancellationToken);
    }
}