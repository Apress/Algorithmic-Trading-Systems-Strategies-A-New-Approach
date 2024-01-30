using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;

public interface ICrossingStep
{
    public AlgorithmTypeInfo_AllowedStep GetInfo();
    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables);
    public List<AlgorithmPoint> Crossing(List<ObjectiveFunctionResult> parents,
        List<FunctionVariable> functionVariables);
}