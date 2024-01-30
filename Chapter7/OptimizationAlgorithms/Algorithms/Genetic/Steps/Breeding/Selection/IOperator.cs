using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;

public interface IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo();
    public void Init(AlgorithmInfo_Operator info);
    public List<List<ObjectiveFunctionResult>> GetParentsList(List<ObjectiveFunctionResult> individuals, int groupsCount);
}