﻿using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;
using OptimizationAlgorithms.Common.Extensions;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Breeding.Crossing.Operations;

public class DiscreteOperator: IOperator
{
    public AlgorithmTypeInfo_Operator GetInfo()
    {
        var @operator = new AlgorithmTypeInfo_Operator(OperatorTypes.Discrete);

        return @operator;
    }

    public void Init(AlgorithmInfo_Operator info)
    {
    }

    public List<AlgorithmPoint> Crossing(List<ObjectiveFunctionResult> parents, List<FunctionVariable> functionVariables)
    {
        var nextGeneration = new List<AlgorithmPoint>();

        ObjectiveFunctionResult parent1 = parents[0];
        ObjectiveFunctionResult parent2 = parents[1];
        var point = new AlgorithmPoint();
        nextGeneration.Add(point);
        foreach (FunctionVariableValue value1 in parent1.Point.Values)
        {
            FunctionVariableValue value2 = parent2.Point.Values.First(v => v.Id.Equals(value1.Id));
            FunctionVariable functionVariable = functionVariables.First(v => v.Id.Equals(value1.Id));
            decimal best = parent1.Value > parent2.Value ? value1.Value : value2.Value;
            decimal worst = parent1.Value < parent2.Value ? value1.Value : value2.Value;
            decimal crossValue = Crossing(best, worst, functionVariable);
            point.Values.Add(new FunctionVariableValue(value1.Id, crossValue));
        }

        return nextGeneration;
    }
    
    private decimal Crossing(decimal best, decimal worst, FunctionVariable functionVariable)
    {
        var crossValue = Roulette.Selection(new List<decimal>{best, worst});
        return crossValue;
    }
}