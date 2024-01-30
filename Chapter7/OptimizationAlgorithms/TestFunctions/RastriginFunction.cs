using OptimizationAlgorithms.Common;

namespace OptimizationAlgorithms.TestFunctions;

public static class RastriginFunction
{
    public static decimal Calculate(List<FunctionVariable> functionVariables, AlgorithmPoint point)
    {
        double result = 0;
        foreach (FunctionVariable variable in functionVariables)
        {
            double x = (double)point.Values.First(v => v.Id == variable.Id).Value;
            result += x * x - 10 * Math.Cos(2 * Math.PI * x) + 10;
        }

        return (decimal)result;
    }
}