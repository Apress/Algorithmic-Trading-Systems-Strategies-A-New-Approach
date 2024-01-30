namespace ProcessBot.Interfaces;

public interface IProcessActFactory
{
    IProcessAct? GetAct(int actId);
}