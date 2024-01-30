using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization.Operators;

public class FlatManagedOperator: IOperator
{
    private int _maxPopulationSize;
    private int _minPopulationSize;
    private decimal _coveredPercent;
    
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.FlatManaged);
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.MaxPopulationSize, 50));
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.MinPopulationSize, 1));
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.CoveredPercent, 1));

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _maxPopulationSize = (int) info.Params.First(p => p.Type == ParamTypes.MaxPopulationSize).Value;
        _minPopulationSize = (int) info.Params.First(p => p.Type == ParamTypes.MinPopulationSize).Value;
        _coveredPercent = info.Params.First(p => p.Type == ParamTypes.CoveredPercent).Value;
    }

public List<AlgorithmPoint> Generate(
    List<FunctionVariable> functionVariables, 
    PointMagicData? magicData)
{
    int populationSize = GetPopulationSize(functionVariables);
    return PointsFactory.Flat(functionVariables, magicData, populationSize);
}

private int GetPopulationSize(List<FunctionVariable> functionVariables)
{
    long variablesCount = 1;
    foreach (var variable in functionVariables)
    {
        List<decimal> values = variable.GetValues();
        variablesCount *= values.Count;
    }

    long populationSizeLong = (long)(_coveredPercent / 100 * variablesCount);

    int populationSize = (int)Math.Min(populationSizeLong, _maxPopulationSize);
    populationSize = Math.Max(populationSize, _minPopulationSize);
    
    return populationSize;
}
}