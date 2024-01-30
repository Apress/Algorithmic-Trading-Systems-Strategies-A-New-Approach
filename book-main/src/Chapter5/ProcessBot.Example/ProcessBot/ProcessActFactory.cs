using ProcessBot.Example.ProcessBot.Acts;
using ProcessBot.Interfaces;

namespace ProcessBot.Example.ProcessBot;

public class ProcessActFactory: IProcessActFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ProcessActFactory(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IProcessAct? GetAct(int actId) => actId switch
    {
        (int) ActTypes.SayHello => GetAct<SayHelloAct>(),
        (int) ActTypes.SaySomething => GetAct<SaySomethingAct>(),
        _ => throw new Exception($"unknown type '${actId}'")
    };
    
    private T? GetAct<T>() where T : class, IProcessAct
    {
        return _serviceProvider.GetRequiredService(typeof(T)) as T;
    }
}