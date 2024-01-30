using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;

public class SelectionStep: ISelectionStep
{
    private readonly OperatorFactory _operatorFactory;
    private IOperator _operator;

    public SelectionStep(
        OperatorFactory operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }

    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        var info = new AlgorithmTypeInfo_AllowedStep(StepTypes.Selection);
        
        IEnumerable<IOperator> operators = _operatorFactory.GetAll();
        foreach (IOperator @operator in operators)
            info.AllowedOperators.Add(@operator.GetInfo());

        return info;
    }

    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables)
    {
        _operator = _operatorFactory.GetOperator(step.Operator);
    }

    public List<List<ObjectiveFunctionResult>> GetParentsList(List<ObjectiveFunctionResult> individuals, int groupsCount)
    {
        return _operator.GetParentsList(individuals, groupsCount);
    }
}