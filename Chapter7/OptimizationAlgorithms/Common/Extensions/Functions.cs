namespace OptimizationAlgorithms.Common.Extensions;

public static class Functions
{
    public static double EuclidNorm(List<FunctionVariableValue> values1, List<FunctionVariableValue> values2)
    {
        double sum = 0;
        foreach (FunctionVariableValue item1 in values1)
        {
            FunctionVariableValue item2 = values2.First(v => v.Id.Equals(item1.Id));
            sum += Math.Pow((double)item1.Value - (double)item2.Value, 2);
        }

        return Math.Sqrt(sum);
    }

    public static double GaussFunction(double x, double m, double d)
    {
        return (1 / (d * Math.Sqrt(2*Math.PI))) * Math.Exp(-1 * Math.Pow(x - m, 2) / (2 * Math.Pow(d, 2)));
    }
}