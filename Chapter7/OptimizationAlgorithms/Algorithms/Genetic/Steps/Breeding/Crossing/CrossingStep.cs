using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;

public class CrossingStep: ICrossingStep
{
    private readonly OperatorFactory _operatorFactory;
    public IOperator _operator;

    public CrossingStep(
        OperatorFactory operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }

    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        var info = new AlgorithmTypeInfo_AllowedStep(StepTypes.Crossing);
        
        IEnumerable<IOperator> operators = _operatorFactory.GetAll();
        foreach (IOperator @operator in operators)
            info.AllowedOperators.Add(@operator.GetInfo());

        return info;
    }

    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables)
    {
        _operator = _operatorFactory.GetOperator(step.Operator);
    }

    public List<AlgorithmPoint> Crossing(List<ObjectiveFunctionResult> parents, List<FunctionVariable> functionVariables)
    {
        return _operator.Crossing(parents, functionVariables);
    }
}