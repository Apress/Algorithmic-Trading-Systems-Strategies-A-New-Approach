using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Common;

public class AlgorithmFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AlgorithmFactory(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IOptimizationAlgorithm GetOptimizationAlgorithm(
        AlgorithmInfo info,
        List<FunctionVariable> functionVariables)
    {
        IOptimizationAlgorithm optimizationAlgorithm =
            GetOptimizationAlgorithm(info.Type);

        optimizationAlgorithm.Init(info, functionVariables);

        return optimizationAlgorithm;
    }

    public IOptimizationAlgorithm GetOptimizationAlgorithm(
        AlgorithmTypes type)
    {
        return _serviceProvider
            .GetRequiredKeyedService<IOptimizationAlgorithm>(type);
    }
}