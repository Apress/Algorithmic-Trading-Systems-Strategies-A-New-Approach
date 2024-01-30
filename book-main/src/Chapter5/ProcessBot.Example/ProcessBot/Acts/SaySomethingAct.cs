using ProcessBot.Interfaces;

namespace ProcessBot.Example.ProcessBot.Acts;

public class SaySomethingAct : IProcessAct
{
    private readonly ILogger<SaySomethingAct> _logger;

    public SaySomethingAct(ILogger<SaySomethingAct> logger)
    {
        _logger = logger;
    }
    
    public Task<(bool move, int? nextNodeId)> MakeAsync(string actParams, ProcessEntityData entityData)
    {
        _logger.LogInformation(actParams);
        return Task.FromResult<(bool move, int? nextNodeId)>((true, null));
    }
}