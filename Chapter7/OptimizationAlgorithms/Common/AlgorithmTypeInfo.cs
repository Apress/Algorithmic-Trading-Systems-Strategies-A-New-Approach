using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Common;

public record AlgorithmTypeInfo(AlgorithmTypes Type)
{
    public List<AlgorithmTypeInfo_AllowedStep> AllowedSteps { get; } = new();
    public List<AlgorithmTypeInfo_Param> Params { get; } = new();
}

public record AlgorithmTypeInfo_AllowedStep(StepTypes Type, int? Index = null)
{
    public List<AlgorithmTypeInfo_Operator> AllowedOperators { get; } = new();
    public List<AlgorithmTypeInfo_AllowedStep> Steps { get; } = new();
    public List<AlgorithmTypeInfo_Param> Params { get; } = new();
}

public record AlgorithmTypeInfo_Operator(OperatorTypes Type)
{
    public List<AlgorithmTypeInfo_Param> Params = new();
}

public record AlgorithmTypeInfo_Param(ParamTypes Type, decimal DefaultValue);