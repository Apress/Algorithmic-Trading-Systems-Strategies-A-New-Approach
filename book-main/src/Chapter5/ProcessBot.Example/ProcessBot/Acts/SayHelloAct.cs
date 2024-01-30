using ProcessBot.Interfaces;

namespace ProcessBot.Example.ProcessBot.Acts;

public class SayHelloAct : IProcessAct
{
    private readonly ILogger<SayHelloAct> _logger;

    public SayHelloAct(ILogger<SayHelloAct> logger)
    {
        _logger = logger;
    }
    
    public Task<(bool move, int? nextNodeId)> MakeAsync(string actParams, ProcessEntityData entityData)
    {
        _logger.LogInformation("Hello");
        return Task.FromResult<(bool move, int? nextNodeId)>((true, null));
    }
}