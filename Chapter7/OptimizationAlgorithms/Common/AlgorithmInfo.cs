using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Common;

public record AlgorithmInfo(AlgorithmTypes Type)
{
    public List<AlgorithmInfo_Param> Params { get; } = new();
    public List<AlgorithmInfo_Step> Steps { get; } = new();
}

public record AlgorithmInfo_Step(int Index, StepTypes Type, AlgorithmInfo_Operator? Operator)
{
    public List<AlgorithmInfo_Param> Params { get; } = new();
    public List<AlgorithmInfo_Step> Steps { get; } = new();
}

public record AlgorithmInfo_Operator(OperatorTypes Type)
{
    public List<AlgorithmInfo_Param> Params { get; } = new();
}

public record AlgorithmInfo_Param(ParamTypes Type, decimal Value);