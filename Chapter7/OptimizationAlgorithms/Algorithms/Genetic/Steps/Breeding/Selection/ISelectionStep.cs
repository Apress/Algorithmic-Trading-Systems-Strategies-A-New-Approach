using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;

public interface ISelectionStep
{
    public AlgorithmTypeInfo_AllowedStep GetInfo();
    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables);

    public List<List<ObjectiveFunctionResult>> GetParentsList(
        List<ObjectiveFunctionResult> individuals,
        int groupsCount);
}