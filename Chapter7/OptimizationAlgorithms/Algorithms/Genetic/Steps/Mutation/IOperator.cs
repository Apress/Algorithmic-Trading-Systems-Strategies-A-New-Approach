using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation;

public interface IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo();
    public void Init(AlgorithmInfo_Operator info);
    public decimal Mutation(FunctionVariable functionVariable, decimal parentValue);
}