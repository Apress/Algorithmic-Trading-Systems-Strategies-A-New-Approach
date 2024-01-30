using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection.Operators;

public class SelectionOperator: IOperator
{
    private int _elitePercent;
    
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Selection);
        
        @operator.Params.Add(new AlgorithmTypeInfo_Param(ParamTypes.ElitePercent, 10));

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
        _elitePercent = (int) info.Params.First(p => p.Type == ParamTypes.ElitePercent).Value;
    }

    public List<List<ObjectiveFunctionResult>> GetParentsList(List<ObjectiveFunctionResult> individuals, int groupsCount)
    {
        var parentsList = new List<List<ObjectiveFunctionResult>>();
        
        var elite = new List<ObjectiveFunctionResult>();
        int eliteCount = _elitePercent / 100 * individuals.Count;
        elite.AddRange(individuals.OrderByDescending(i => i.Value).ToList()
            .GetRange(0, eliteCount-1));
        
        List<(ObjectiveFunctionResult Entity, double ProbabilityWeight)> listForRoulette =
            elite.ConvertAll(i => (i, (double)i.Value));
        for (int i = 0; i < groupsCount; i++)
        {
            List<ObjectiveFunctionResult> parentsGroup = 
                Roulette.SelectionDistinct(listForRoulette, 2);
            parentsList.Add(parentsGroup);
        }

        return parentsList;
    }
}