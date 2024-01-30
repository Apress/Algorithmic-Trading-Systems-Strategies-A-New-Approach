using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.BruteForce;

public static class BruteForceAlgorithmCollectionExtensions
{
public static IServiceCollection AddBruteForceAlgorithm(this IServiceCollection services)
{
    services
        .AddKeyedTransient<IOptimizationAlgorithm, BruteForceAlgorithm>(
            AlgorithmTypes.BruteForce);

    return services;
}
}