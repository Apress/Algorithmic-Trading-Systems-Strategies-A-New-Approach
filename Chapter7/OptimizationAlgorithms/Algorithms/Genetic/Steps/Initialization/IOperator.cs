using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization;

public interface IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo();
    public void Init(AlgorithmInfo_Operator info);
    public List<AlgorithmPoint> Generate(List<FunctionVariable> functionVariables, PointMagicData? magicData);
}