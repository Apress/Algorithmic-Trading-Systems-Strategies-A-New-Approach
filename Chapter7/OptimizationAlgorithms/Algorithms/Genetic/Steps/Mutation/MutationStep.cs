using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Extensions;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation;

public class MutationStep: IAlgorithmStep
{
    private readonly OperatorFactory _operatorFactory;
    public AlgorithmInfo_Step _step;
    public List<FunctionVariable> _functionVariables;
    public IOperator _operator;
    public int _mutationCount;
    
    public MutationStep(
        OperatorFactory operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }

    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        var info = new AlgorithmTypeInfo_AllowedStep(StepTypes.Mutation);
        
        info.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.MutationCount, 1));

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
        _mutationCount = (int) _step.Params.First(p => p.Type == ParamTypes.MutationCount).Value;
    }

public List<ObjectiveFunctionResult> 
    GetNextPopulation(List<ObjectiveFunctionResult> previousPopulation, 
        List<ObjectiveFunctionResult> previousResults)
{
    List<ObjectiveFunctionResult> nextPopulation = new();
    nextPopulation.AddRange(previousPopulation);
    nextPopulation.AddRange(previousResults);
    return nextPopulation;
}

public List<AlgorithmPoint> GetNextPoints(
    List<ObjectiveFunctionResult>? population = null, 
    PointMagicData? magicData = null)
{
    var points = new List<AlgorithmPoint>();
    if (population == null)
        return points;
    
    foreach (ObjectiveFunctionResult parent in population)
    {
        AlgorithmPoint mutant = Mutation(parent);
        if (magicData != null)
            mutant.MagicData = magicData.ToString();
        points.Add(mutant);
    }

    return points;
}

private AlgorithmPoint Mutation(ObjectiveFunctionResult parent)
{
    var mutant = new AlgorithmPoint();
    
    List<IVariableId> changeableVariables = new();
    foreach (FunctionVariable functionVariable in _functionVariables)
    {
        List<decimal> variations = functionVariable.GetValues();
        if (variations.Count>1)
            changeableVariables.Add(functionVariable.Id);
    }

    var random = new Random();
    List<IVariableId> variablesForMutation = 
        random.GetList(changeableVariables, _mutationCount);
    foreach (var parentVariableValue in parent.Point.Values)
    {
        if (variablesForMutation.Contains(parentVariableValue.Id))
        {
            FunctionVariable functionVariable = 
                _functionVariables.First(v => v.Id.Equals(parentVariableValue.Id));
            decimal parentValue = parentVariableValue.Value;
            decimal mutationValue = _operator.Mutation(functionVariable, parentValue);
            mutant.Values.Add(new FunctionVariableValue(parentVariableValue.Id, mutationValue));
        }
        else
            mutant.Values.Add(parentVariableValue);
    }

    return mutant;
}
}