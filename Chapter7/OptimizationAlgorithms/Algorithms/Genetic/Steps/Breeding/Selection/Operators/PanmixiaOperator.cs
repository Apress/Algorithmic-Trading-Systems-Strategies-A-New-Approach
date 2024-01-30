using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Selection.Operators;

public class PanmixiaOperator: IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Panmixia);

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
    }

    public List<List<ObjectiveFunctionResult>> GetParentsList(List<ObjectiveFunctionResult> individuals, int groupsCount)
    {
        var parentsList = new List<List<ObjectiveFunctionResult>>();
        
        for (int i = 0; i < groupsCount; i++)
        {
            List<ObjectiveFunctionResult> parentsGroup = Roulette.SelectionDistinct(individuals, 2);
            parentsList.Add(parentsGroup);
        }

        return parentsList;
    }
}