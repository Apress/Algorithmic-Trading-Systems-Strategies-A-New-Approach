using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation.Operators;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Mutation;

public static class MutationStepCollectionExtensions
{
    public static IServiceCollection AddMutationStep(this IServiceCollection services)
    {
        services
            .AddKeyedTransient<IAlgorithmStep, MutationStep>(
                StepTypes.Mutation);
        services.AddSingleton<OperatorFactory>();

        services.AddKeyedTransient<IOperator, RandomOperator>(OperatorTypes.Random);
        services.AddKeyedTransient<IOperator, GaussOperator>(OperatorTypes.Gauss);

        return services;
    }
}