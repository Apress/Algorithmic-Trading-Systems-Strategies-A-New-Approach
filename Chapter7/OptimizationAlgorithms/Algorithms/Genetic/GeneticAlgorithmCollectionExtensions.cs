using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic;

public static class GeneticAlgorithmCollectionExtensions
{
public static IServiceCollection AddGeneticAlgorithm(this IServiceCollection services)
{
    services
        .AddKeyedTransient<IOptimizationAlgorithm, GeneticAlgorithm>(
            AlgorithmTypes.Genetic);
    
    services.AddSingleton<StepFactory>();

    services.AddInitializationStep();
    services.AddMutationStep();
    services.AddFilteringStep();
    services.AddBreedingStep();

    return services;
}
}