using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization;

public class InitializationStep : IAlgorithmStep
{
    private readonly OperatorFactory _operatorFactory;
    
    public AlgorithmInfo_Step _step;
    public List<FunctionVariable> _functionVariables;
    public IOperator _operator;

    public InitializationStep(
        OperatorFactory operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }

    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        AlgorithmTypeInfo_AllowedStep info = new(StepTypes.Initialization, 0);

        IEnumerable<IOperator> operators = _operatorFactory.GetAll();
        foreach (IOperator @operator in operators)
            info.AllowedOperators.Add(@operator.GetInfo());

        return info;
    }

public void Init(
    AlgorithmInfo_Step step, 
    List<FunctionVariable> functionVariables)
{
    _step = step;
    _functionVariables = functionVariables;
    _operator = _operatorFactory.GetOperator(_step.Operator);
}

    public List<ObjectiveFunctionResult> GetNextPopulation(List<ObjectiveFunctionResult> previousPopulation, List<ObjectiveFunctionResult> previousResults)
    {
        return previousResults;
    }

public List<AlgorithmPoint> GetNextPoints(
    List<ObjectiveFunctionResult>? population = null, 
    PointMagicData? magicData = null)
{
    return _operator.Generate(_functionVariables, magicData);
}
}