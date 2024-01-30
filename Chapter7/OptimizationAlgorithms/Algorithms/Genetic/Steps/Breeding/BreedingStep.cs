using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding;

public class BreedingStep: IAlgorithmStep
{
    private List<FunctionVariable> _functionVariables;
    private int _groupsCount;
    private bool _replaceWorst;
    private readonly ISelectionStep _selectionStep;
    private readonly ICrossingStep _crossingStep;

    public BreedingStep(
        ISelectionStep selectionStep,
        ICrossingStep crossingStep
    )
    {
        _selectionStep = selectionStep;
        _crossingStep = crossingStep;
    }
    
    public AlgorithmTypeInfo_AllowedStep GetInfo()
    {
        AlgorithmTypeInfo_AllowedStep info = new(StepTypes.Breeding);
        
        info.Steps.Add(_selectionStep.GetInfo());
        info.Steps.Add(_crossingStep.GetInfo());
        
        return info;
    }

    public void Init(AlgorithmInfo_Step step, List<FunctionVariable> functionVariables)
    {
        _functionVariables = functionVariables;
        _groupsCount = (int) step.Params.First(p => p.Type == ParamTypes.GroupsCount).Value;
        _replaceWorst = step.Params.First(p => p.Type == ParamTypes.ReplaceWorst).Value == 1;
        
        var selectionStepInfo = step.Steps.First(s => s.Type == StepTypes.Selection);
        _selectionStep.Init(selectionStepInfo, _functionVariables);
        var crossingStepInfo = step.Steps.First(s => s.Type == StepTypes.Crossing);
        _crossingStep.Init(crossingStepInfo, _functionVariables);
    }

public List<ObjectiveFunctionResult> GetNextPopulation(
    List<ObjectiveFunctionResult> previousPopulation, 
    List<ObjectiveFunctionResult> previousResults)
{
    if (previousPopulation.Count == 1)
        return previousPopulation;
    
    var individuals = new List<ObjectiveFunctionResult>();

    if (_replaceWorst)
    {
        int needParentsCount = 
            previousPopulation.Count>previousResults.Count 
                ? previousPopulation.Count - previousResults.Count 
                : 0;
        var needParents = previousPopulation
            .OrderByDescending(p => p.Value).Take(needParentsCount);
        individuals.AddRange(needParents);
        individuals.AddRange(previousResults);
    }
    else
    {
        individuals.AddRange(previousPopulation);
        individuals.AddRange(previousResults);
    }

    individuals = individuals.GroupBy(i => i.Id)
        .Select(g => g.First()).ToList();

    return individuals;
}

public List<AlgorithmPoint> GetNextPoints(
    List<ObjectiveFunctionResult>? population = null, 
    PointMagicData? magicData = null)
{
    var nextGeneration = new List<AlgorithmPoint>();
    if (population != null && population.Count < 2)
        return nextGeneration;
    
    List<List<ObjectiveFunctionResult>> parentsList = 
        _selectionStep.GetParentsList(population, _groupsCount);
    foreach (List<ObjectiveFunctionResult> parents in parentsList)
    {
        List<AlgorithmPoint> children = _crossingStep
            .Crossing(parents, _functionVariables);
        if (magicData != null)
        {
            string magicDataString = magicData.ToString();
            children.ForEach(c => c.MagicData = magicDataString);
        }
        nextGeneration.AddRange(children);
    }

    return nextGeneration;
}
}