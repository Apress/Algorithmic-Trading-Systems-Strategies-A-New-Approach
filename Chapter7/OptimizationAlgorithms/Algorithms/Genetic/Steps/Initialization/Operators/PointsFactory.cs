using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.Algorithms.Genetic.Steps.Initialization.Operators;

public static class PointsFactory
{
    public static List<AlgorithmPoint> Flat(List<FunctionVariable> functionVariables, PointMagicData? magicData, int populationSize)
    {
        int i = 0;
        List<AlgorithmPoint> generatedValues = GenerateValues(functionVariables, populationSize);
        List<AlgorithmPoint> distinctPopulation = AlgorithmPointDistinct(generatedValues);
        while (i < 30 && distinctPopulation.Count != generatedValues.Count)
        {
            generatedValues = GenerateValues(functionVariables, populationSize);
            distinctPopulation = AlgorithmPointDistinct(generatedValues);
            i++;
        }
        
        distinctPopulation.ForEach(p => p.MagicData = magicData.ToString());

        return distinctPopulation;
    }
    
    private static List<AlgorithmPoint> AlgorithmPointDistinct(List<AlgorithmPoint> algorithmPoints)
    {
        var result = new List<AlgorithmPoint>();
        foreach (var point in algorithmPoints)
        {
            if (!result.Exists(r => r == point))
                result.Add(point);
        }

        return result;
    }

    private static List<AlgorithmPoint> GenerateValues(List<FunctionVariable> functionVariables, int populationSize)
    {
        var result = new List<AlgorithmPoint>();
        
        List<FunctionVariableValue> allowedValues = GetAllowedValues(functionVariables);
        List<FunctionVariableValue> variablesForGeneration = GetVariableForGeneration(functionVariables, allowedValues, populationSize);

        var rand = new Random();
        for (int i = populationSize; i > 0; i--)
        {
            var point = new AlgorithmPoint();
            result.Add(point);
            foreach (var functionVariable in functionVariables)
            {
                int varNumber = rand.Next(0, i);
                FunctionVariableValue value = variablesForGeneration.Where(v => v.Id.Equals(functionVariable.Id)).ToArray()[varNumber];
                variablesForGeneration.Remove(value);
                point.Values.Add(value);
            }
        }

        return result;
    }

    private static List<FunctionVariableValue> GetAllowedValues(List<FunctionVariable> functionVariables)
    {
        var result = new List<FunctionVariableValue>();

        foreach (FunctionVariable functionVariable in functionVariables)
        {
            List<decimal> values = functionVariable.GetValues();
            foreach (decimal value in values)
            {
                result.Add(new FunctionVariableValue(functionVariable.Id, value));
            }
        }

        return result;
    }

    private static List<FunctionVariableValue> GetVariableForGeneration(
        List<FunctionVariable> functionVariables, 
        List<FunctionVariableValue> allowedVariables, 
        int populationSize)
    {
        var result = new List<FunctionVariableValue>();
        var rand = new Random();
        foreach (FunctionVariable functionVariable in functionVariables)
        {
            List<FunctionVariableValue> currentAllowedVariables =
                allowedVariables.Where(v => v.Id.Equals(functionVariable.Id)).ToList();
            int valuesCount = currentAllowedVariables.Count;
            for (int i = 0; i < populationSize/valuesCount; i++)
            {
                foreach (FunctionVariableValue allowValue in currentAllowedVariables)
                    result.Add(new FunctionVariableValue(allowValue.Id, allowValue.Value));
            }

            int remainder = populationSize - result.Count(r => r.Id.Equals(functionVariable.Id));
            if (remainder != 0)
            {
                int step = valuesCount / remainder;
                if (step == 1)
                {
                    for (int i = 0; i < remainder; i++)
                    {
                        int varNumber = rand.Next(0, valuesCount-1);
                        result.Add(new FunctionVariableValue(functionVariable.Id, currentAllowedVariables[varNumber].Value));
                    }
                }
                else
                {
                    for (int i = remainder; i > 0 ; i--)
                    {
                        int varNumber = rand.Next((i-1) * step, i * step);
                        result.Add(new FunctionVariableValue(functionVariable.Id, currentAllowedVariables[varNumber].Value));
                    }
                }
            }
        }

        return result; 
    }
}