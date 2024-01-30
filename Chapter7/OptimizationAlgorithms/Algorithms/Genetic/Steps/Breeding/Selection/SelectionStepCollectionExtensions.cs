using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection.Operators;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection;

public static class SelectionStepCollectionExtensions
{
    public static IServiceCollection AddSelectionStep(this IServiceCollection services)
    {
        services.AddTransient<ISelectionStep, SelectionStep>();
        services.AddSingleton<OperatorFactory>();

        services.AddKeyedTransient<IOperator, SelectionOperator>(OperatorTypes.Selection);
        services.AddKeyedTransient<IOperator, PanmixiaOperator>(OperatorTypes.Panmixia);
        
        return services;
    }
}