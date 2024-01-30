using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization.Operators;

public class FlatOperator: IOperator
{
    private int _populationSize;
    
public AlgorithmTypeInfo_Operator GetInfo()
{
    var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Flat);
    @operator.Params.Add(
        new AlgorithmTypeInfo_Param(ParamTypes.PopulationSize, 10));

    return @operator;
}

    public void Init(AlgorithmInfo_Operator info)
    {
        _populationSize = (int) info.Params.First(p => p.Type == ParamTypes.PopulationSize).Value;
    }

public List<AlgorithmPoint> Generate(
    List<FunctionVariable> functionVariables, 
    PointMagicData? magicData)
{
    return PointsFactory.Flat(functionVariables, magicData, _populationSize);
}
}