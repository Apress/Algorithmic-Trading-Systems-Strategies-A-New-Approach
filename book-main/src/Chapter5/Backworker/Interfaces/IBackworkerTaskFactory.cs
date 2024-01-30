namespace Backworker.Interfaces;

public interface IBackworkerTaskFactory
{
    IBackworkerTaskAct? GetTask(int type);
}