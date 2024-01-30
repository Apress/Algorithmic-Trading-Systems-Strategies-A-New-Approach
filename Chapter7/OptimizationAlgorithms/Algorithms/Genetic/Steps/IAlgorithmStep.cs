using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps;

public interface IAlgorithmStep
{
    public AlgorithmTypeInfo_AllowedStep GetInfo();
    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables);
    public List<ObjectiveFunctionResult> GetNextPopulation(List<ObjectiveFunctionResult> previousPopulation, List<ObjectiveFunctionResult> previousResults);
    public List<AlgorithmPoint> GetNextPoints(List<ObjectiveFunctionResult>? population = null, PointMagicData? magicData = null);
}