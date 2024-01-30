using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding;

public static class BreedingStepCollectionExtensions
{
    public static IServiceCollection AddBreedingStep(this IServiceCollection services)
    {
        services.AddKeyedTransient<IAlgorithmStep, BreedingStep>(StepTypes.Breeding);

        services.AddSelectionStep();
        services.AddCrossingStep();
        
        return services;
    }
}