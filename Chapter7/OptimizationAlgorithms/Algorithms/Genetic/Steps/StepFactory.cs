using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps;

public class StepFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StepFactory(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IAlgorithmStep GetStep(
        AlgorithmInfo_Step step, 
        List<FunctionVariable> functionVariables)
    {
        IAlgorithmStep algorithmStep = GetStep(step.Type);
        algorithmStep.Init(step, functionVariables);
    
        return algorithmStep;
    }

    private IAlgorithmStep GetStep(StepTypes stepType)
    {
        return _serviceProvider.GetRequiredKeyedService<IAlgorithmStep>(stepType);
    }

    public IEnumerable<IAlgorithmStep> GetAll()
    {
        return _serviceProvider.GetServices<IAlgorithmStep>();
    }
}