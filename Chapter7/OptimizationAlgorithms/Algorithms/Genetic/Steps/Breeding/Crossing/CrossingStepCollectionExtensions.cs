using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing.Operations;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;

public static class CrossingStepCollectionExtensions
{
    public static IServiceCollection AddCrossingStep(this IServiceCollection services)
    {
        services.AddTransient<ICrossingStep, CrossingStep>();
        services.AddSingleton<OperatorFactory>();

        services.AddKeyedTransient<IOperator, FlatOperator>(OperatorTypes.Flat);
        services.AddKeyedTransient<IOperator, DiscreteOperator>(OperatorTypes.Discrete);
        
        return services;
    }
}