using OptimizationAlgorithms.Algorithms.Genetic.Steps;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic;

public class GeneticAlgorithm : IOptimizationAlgorithm
{
    private readonly StepFactory _stepFactory;

    public AlgorithmInfo _info;
    public List<FunctionVariable> _functionVariables;
    public int _generationsCount;

    public GeneticAlgorithm(
        StepFactory stepFactory)
    {
        _stepFactory = stepFactory;
    }

    public void Init(AlgorithmInfo info, List<FunctionVariable> functionVariables)
    {
        _info = info;
        _functionVariables = functionVariables;

        _generationsCount = (int) _info.Params.First(p => p.Type == ParamTypes.GenerationsCount).Value;
    }

    public AlgorithmTypeInfo GetTypeInfo()
    {
        var info = new AlgorithmTypeInfo(AlgorithmTypes.Genetic);

        info.Params.Add(
            new AlgorithmTypeInfo_Param(ParamTypes.GenerationsCount, 10));

        IEnumerable<IAlgorithmStep> steps =
            _stepFactory.GetAll();
        foreach (IAlgorithmStep step in steps)
            info.AllowedSteps.Add(step.GetInfo());

        return info;
    }

    public (List<AlgorithmPoint> points, string magicString)? GetNextPoints(
        List<ObjectiveFunctionResult>? previousResults = null,
        string? magicString = null)
    {
        previousResults ??= new();
        MagicData currentMagicData = new MagicData(magicString);
        List<ObjectiveFunctionResult> currentPopulation =
            GetCurrentPopulation(currentMagicData, previousResults);
        List<ObjectiveFunctionResult> currentPreviousResults =
            GetCurrentPreviousResults(currentMagicData, previousResults);

        List<AlgorithmPoint>? nextPoints = null;
        var nextMagicData = new MagicData();
        while (nextPoints == null || !nextPoints.Any())
        {
            AlgorithmInfo_Step currentStepInfo = _info.Steps
                .First(s => s.Index == currentMagicData.StepIndex);
            IAlgorithmStep currentStep = _stepFactory
                .GetStep(currentStepInfo, _functionVariables);
            currentPopulation = currentStep
                .GetNextPopulation(currentPopulation, currentPreviousResults);

            nextMagicData.PopulationIds = currentPopulation
                .ConvertAll(i => i.Id);
            AlgorithmInfo_Step? nextStepInfo = GetNextStep(currentMagicData, previousResults);
            bool stoppingConditionFulfilled =
                CheckStoppingCondition(nextStepInfo, currentMagicData, currentStepInfo);
            if (stoppingConditionFulfilled)
                return null;

            nextMagicData.GenerationNumber = currentMagicData.GenerationNumber;
            nextMagicData.StepIndex = nextStepInfo.Index;
            if (nextStepInfo!.Index <= currentStepInfo.Index && currentPopulation.Any())
                nextMagicData.GenerationNumber++;

            IAlgorithmStep nextStep = _stepFactory.GetStep(nextStepInfo, _functionVariables);
            nextPoints = nextStep
                .GetNextPoints(
                    currentPopulation, new PointMagicData(nextMagicData.GenerationNumber, nextMagicData.StepIndex));
            currentMagicData = nextMagicData;
        }

        return (nextPoints, nextMagicData.ToString());
    }

    private bool CheckStoppingCondition(
        AlgorithmInfo_Step? nextStepInfo,
        MagicData currentMagicData,
        AlgorithmInfo_Step currentStepInfo
    )
    {
        return nextStepInfo == null || (nextStepInfo.Index <= currentStepInfo.Index &&
                                        currentMagicData.GenerationNumber == _generationsCount);
    }

    private List<ObjectiveFunctionResult> GetCurrentPreviousResults(
        MagicData currentMagicData, List<ObjectiveFunctionResult> previousResults)
    {
        return (
            from previousResult in previousResults
            let pointMagicData = new PointMagicData(previousResult.Point.MagicData)
            where pointMagicData.GenerationNumber == currentMagicData.GenerationNumber
                  && pointMagicData.StepIndex == currentMagicData.StepIndex
            select previousResult).ToList();
    }

    private List<ObjectiveFunctionResult> GetCurrentPopulation(
        MagicData currentMagicData, List<ObjectiveFunctionResult> previousResults)
    {
        bool afterInitStep = currentMagicData.GenerationNumber == 1 && currentMagicData.StepIndex == 0;
        return afterInitStep
            ? previousResults
            : previousResults
                .Where(item =>
                    currentMagicData.PopulationIds.Contains(item.Id)).ToList();
    }

private AlgorithmInfo_Step GetNextStep(
    MagicData currentMagicData, 
    List<ObjectiveFunctionResult> previousResults)
{
    if (!previousResults.Any())
        return _info.Steps.Single(s => s.Index == 0);

    AlgorithmInfo_Step? nextStep = 
        _info.Steps
            .Where(s => s.Index > currentMagicData.StepIndex)
            .MinBy(s => s.Index);
    if (nextStep == null)
        nextStep = _info.Steps
            .Where(s => s.Index > 0)
            .MinBy(s => s.Index);

    return nextStep!;
}
}