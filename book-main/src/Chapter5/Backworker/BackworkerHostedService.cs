using System.Diagnostics;
using Backworker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backworker;

public class BackworkerHostedService : IHostedService
{
    private readonly BackworkerManager _backworkerManager;
    private readonly ILogger<BackworkerHostedService> _logger;
    
    private bool _stop;

    public BackworkerHostedService(
        BackworkerManager backworkerManager,
        ILogger<BackworkerHostedService> logger)
    {
        _backworkerManager = backworkerManager;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Backworker start");
        _stop = false;

        Stopwatch stopwatch = Stopwatch.StartNew();
        while (!_stop && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                stopwatch.Restart();

                await _backworkerManager.RunAsync(cancellationToken);

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

        _logger.LogInformation("Backworker stop");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => _stop = true, cancellationToken);
    }
}