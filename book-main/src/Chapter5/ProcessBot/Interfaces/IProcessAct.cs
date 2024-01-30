namespace ProcessBot.Interfaces;

public interface IProcessAct
{
    Task<(bool move, int? nextNodeId)> MakeAsync(string actParams, ProcessEntityData entityData);
}