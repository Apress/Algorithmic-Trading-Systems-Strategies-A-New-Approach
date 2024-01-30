using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation.Operators;

public class GaussOperator : IOperator
{
    private int _standardDeviation;
    
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Gauss);
        
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.StandardDeviation, 1m));

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _standardDeviation = (int) info.Params.First(p => p.Type == ParamTypes.StandardDeviation).Value;
    }

    public decimal Mutation(FunctionVariable functionVariable, decimal parentValue)
    {
        double gaussFunctionValue = Functions.GaussFunction((double)parentValue, 0, _standardDeviation);
        decimal point = parentValue + (decimal) gaussFunctionValue;
        decimal mutation = functionVariable.GetCloseValue(point);
        return mutation;
    }
}