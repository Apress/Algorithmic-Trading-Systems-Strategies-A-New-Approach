using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation.Operators;

public class RandomOperator: IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Random);

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
    }

    public decimal Mutation(FunctionVariable functionVariable, decimal parentValue)
    {
        var random = new Random();
        var variableValues = functionVariable.GetValues();
        variableValues.Remove(parentValue);
        int i = random.Next(0, variableValues.Count-1);
        return variableValues[i];
    }
}