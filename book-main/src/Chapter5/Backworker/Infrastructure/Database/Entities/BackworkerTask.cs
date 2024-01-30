namespace Backworker.Infrastructure.Database.Entities;

public class BackworkerTask
{
    public int Id;
    public string Name;
    public int Type;
    public bool Active;
    public string MagicString;

    public int RepeatPeriodMs;
    public int RestartDelayMs;
    public int CrashRestartDelayMs;

    public bool NeedsToStart(
        DateTime now, 
        DateTime? lastStart, 
        DateTime? lastStop, 
        DateTime? lockTime)
    {
        return Active
               && TimeToStart(
                   now, 
                   lastStart ?? DateTime.MinValue, 
                   lastStop ?? DateTime.MinValue, 
                   lockTime);
    }
    
    private bool TimeToStart(
        DateTime now, 
        DateTime lastStart, 
        DateTime lastStop, 
        DateTime? lockTime)
    {
        bool scheduleStartTime = 
            (now - lastStart).TotalMilliseconds >= RepeatPeriodMs
            && (now - lastStop).TotalMilliseconds >= RestartDelayMs;

        bool restartCrash =
            lockTime.HasValue
            && (now - lockTime.Value).TotalMilliseconds >= CrashRestartDelayMs;

        return scheduleStartTime || restartCrash;
    }
}