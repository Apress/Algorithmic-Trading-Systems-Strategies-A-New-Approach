using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering;

public class FilteringStep : IAlgorithmStep
{
    private readonly OperatorFactory _operatorFactory;
    private List<FunctionVariable> _functionVariables;
    private AlgorithmInfo_Step _step;
    public IOperator _operator;

    public FilteringStep(
        OperatorFactory operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }

    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        AlgorithmTypeInfo_AllowedStep info = new(StepTypes.Filtering);

        IEnumerable<IOperator> operators = _operatorFactory.GetAll();
        foreach (IOperator @operator in operators)
            info.AllowedOperators.Add(@operator.GetInfo());

        return info;
    }

    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables)
    {
        _step = step;
        _functionVariables = functionVariables;
        _operator = _operatorFactory.GetOperator(_step.Operator);
    }

    public List<ObjectiveFunctionResult> GetNextPopulation(
        List<ObjectiveFunctionResult> previousPopulation,
        List<ObjectiveFunctionResult> previousResults)
    {
        return _operator.GetNextPopulation(previousPopulation);
    }

    public List<AlgorithmPoint> GetNextPoints(List<ObjectiveFunctionResult>? population = null,
        PointMagicData? magicData = null)
    {
        return null;
    }
}