using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.BruteForce;
using OptimizationAlgorithms.Algorithms.Genetic;
using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms;

public static class OptimizationAlgorithmsCollectionExtensions
{
public static IServiceCollection AddOptimizationAlgorithms(this IServiceCollection services)
{
    services.AddSingleton<AlgorithmFactory>();

    services.AddBruteForceAlgorithm();
    services.AddGeneticAlgorithm();

    return services;
}
}