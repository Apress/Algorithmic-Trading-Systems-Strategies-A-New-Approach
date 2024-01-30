using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;

public interface IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo();
    public void Init(AlgorithmInfo_Operator info);

    public List<AlgorithmPoint> Crossing(
        List<ObjectiveFunctionResult> parents,
        List<FunctionVariable> functionVariables);
}