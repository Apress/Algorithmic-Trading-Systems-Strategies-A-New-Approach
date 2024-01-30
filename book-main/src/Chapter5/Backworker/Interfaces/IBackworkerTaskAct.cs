namespace Backworker.Interfaces;

public interface IBackworkerTaskAct
{
    Task RunAsync(string magicString, CancellationToken cancellationToken);
}