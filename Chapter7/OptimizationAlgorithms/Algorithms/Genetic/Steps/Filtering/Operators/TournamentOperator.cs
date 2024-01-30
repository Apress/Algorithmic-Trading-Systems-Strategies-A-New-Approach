using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Filtering.Operators;

public class TournamentOperator : IOperator
{
    private int _tournamentSize;
    private int _repeatCount;
    
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Tournament);
        
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.TournamentSize, 2));
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.RepeatCount, 3));
        
        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _tournamentSize = (int) info.Params.First(p => p.Type == ParamTypes.TournamentSize).Value;
        _repeatCount = (int) info.Params.First(p => p.Type == ParamTypes.RepeatCount).Value;
    }

    public List<ObjectiveFunctionResult> GetNextPopulation(List<ObjectiveFunctionResult> previousPopulation)
    {
        var individuals = new List<ObjectiveFunctionResult>();
        for (int i = 0; i < _repeatCount; i++)
        {
            List<List<ObjectiveFunctionResult>> groups = GetTournamentGroups(previousPopulation);
            List<ObjectiveFunctionResult> bestIndividuals = GetBest(groups);
            individuals.AddRange(bestIndividuals);
        }

        return individuals;
    }
    
    private List<List<ObjectiveFunctionResult>> GetTournamentGroups(List<ObjectiveFunctionResult> list)
    {
        var groups = new List<List<ObjectiveFunctionResult>>();
        var choiseList = new List<ObjectiveFunctionResult>(list);
        var random = new Random();
        while (choiseList.Count > 0)
        {
            int groupSize = Math.Min(choiseList.Count, _tournamentSize);
            List<ObjectiveFunctionResult> group = Roulette.SelectionDistinct(choiseList, groupSize, random);
            groups.Add(group);
            group.ForEach(e => choiseList.Remove(e));
        }

        return groups;
    }
    
    private List<ObjectiveFunctionResult> GetBest(List<List<ObjectiveFunctionResult>> groups)
    {
        var result = new List<ObjectiveFunctionResult>();

        foreach (List<ObjectiveFunctionResult> list in groups)
        {
            decimal maxValue = list.Max(e => e.Value);
            List<ObjectiveFunctionResult> best = list.Where(e => e.Value == maxValue).ToList();
            ObjectiveFunctionResult bestItem = Roulette.Selection(best);
            result.Add(bestItem);
        }
        
        return result;
    }
}