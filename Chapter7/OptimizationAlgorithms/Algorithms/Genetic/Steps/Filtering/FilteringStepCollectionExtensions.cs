using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering.Operators;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering;

public static class FilteringStepCollectionExtensions
{
    public static IServiceCollection AddFilteringStep(this IServiceCollection services)
    {
        services
            .AddKeyedTransient<IAlgorithmStep, FilteringStep>(
                StepTypes.Filtering);

        services.AddKeyedTransient<IOperator, ElitismOperator>(OperatorTypes.Elitism);
        services.AddKeyedTransient<IOperator, RouletteOperator>(OperatorTypes.Roulette);
        services.AddKeyedTransient<IOperator, TournamentOperator>(OperatorTypes.Tournament);

        services.AddSingleton<OperatorFactory>();

        return services;
    }
}