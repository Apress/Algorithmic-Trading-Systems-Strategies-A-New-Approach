using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering.Operators;

public class ElitismOperator : IOperator
{
    private decimal _elitePercent;
    private int _populationSize;

    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Elitism);

        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.ElitePercent, 10));
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.PopulationSize, 10));

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _elitePercent = info.Params.First(p => p.Type == ParamTypes.ElitePercent).Value;
        _populationSize = (int) info.Params.First(p => p.Type == ParamTypes.PopulationSize).Value;
    }

    public List<ObjectiveFunctionResult> GetNextPopulation(
        List<ObjectiveFunctionResult> previousPopulation)
    {
        var individuals = new List<ObjectiveFunctionResult>();
        int eliteCount = (int) (_elitePercent / 100 * previousPopulation.Count);
        individuals.AddRange(
            previousPopulation
                .OrderByDescending(i => i.Value).ToList()
                .GetRange(0, eliteCount - 1));
        int remainder = _populationSize - individuals.Count;

        List<(ObjectiveFunctionResult Entity, double ProbabilityWeight)> listForRoulette =
            previousPopulation.ConvertAll(i => (i, (double) i.Value));
        List<ObjectiveFunctionResult> rouletteIndividuals = Roulette.Selection(listForRoulette, remainder);
        individuals.AddRange(rouletteIndividuals);

        return rouletteIndividuals;
    }
}