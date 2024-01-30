using Microsoft.Extensions.DependencyInjection;
using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing;

public class OperatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public OperatorFactory(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IOperator GetOperator(
        AlgorithmInfo_Operator info)
    {
        IOperator @operator = GetOperator(info.Type);
        @operator.Init(info);
        return @operator;
    }

    public IOperator GetOperator(OperatorTypes type)
    {
        return _serviceProvider
            .GetRequiredKeyedService<IOperator>(type);
    }

    public IEnumerable<IOperator> GetAll()
    {
        return _serviceProvider.GetServices<IOperator>();
    }
}