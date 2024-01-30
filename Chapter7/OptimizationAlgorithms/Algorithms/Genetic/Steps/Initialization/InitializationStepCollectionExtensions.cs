using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization.Operators;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization;

public static class InitializationStepCollectionExtensions
{
    public static IServiceCollection AddInitializationStep(this IServiceCollection services)
    {
        services
            .AddKeyedTransient<IAlgorithmStep, InitializationStep>(
                StepTypes.Initialization);

        services.AddKeyedTransient<IOperator, FlatOperator>(OperatorTypes.Flat);
        services.AddKeyedTransient<IOperator, FlatManagedOperator>(OperatorTypes.FlatManaged);

        services.AddSingleton<OperatorFactory>();

        return services;
    }
}