namespace OptimizationAlgorithms.Common;

public interface IOptimizationAlgorithm
{
    public void Init(AlgorithmInfo info, List<FunctionVariable> functionVariables);
    public AlgorithmTypeInfo GetTypeInfo();
    
(List<AlgorithmPoint> points, string? magicString)? 
    GetNextPoints(
        List<ObjectiveFunctionResult>? previousResults = null, 
        string? magicString = null);
}