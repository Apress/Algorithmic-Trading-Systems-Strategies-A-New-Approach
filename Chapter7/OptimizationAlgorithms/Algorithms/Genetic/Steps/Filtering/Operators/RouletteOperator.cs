using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering.Operators;

public class RouletteOperator : IOperator
{
    private int _populationSize;

    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Roulette);

        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.PopulationSize, 10));

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _populationSize = (int) info.Params.First(p => p.Type == ParamTypes.PopulationSize).Value;
    }

    public List<ObjectiveFunctionResult> GetNextPopulation(
        List<ObjectiveFunctionResult> previousPopulation)
    {
        List<(ObjectiveFunctionResult Entity, double ProbabilityWeight)> listForRoulette =
            previousPopulation.ConvertAll(i => (i, (double) i.Value));
        List<ObjectiveFunctionResult> nextIndividuals =
            Roulette.Selection(listForRoulette, _populationSize);

        return nextIndividuals;
    }
}