namespace OptimizationAlgorithms.Common;

public record FunctionVariable
{
    public IVariableId Id { get; }

    public decimal? MinValue { get; }
    public decimal? MaxValue { get; }
    public decimal? Step { get; }

    public List<decimal>? Values { get; }

    public FunctionVariable(
        IVariableId id,
        decimal minValue,
        decimal maxValue,
        decimal step)
    {
        Id = id;
        MinValue = minValue;
        MaxValue = maxValue;
        Step = step;
    }

    public FunctionVariable(IVariableId id, List<decimal>? values)
    {
        Id = id;
        Values = values;
    }

    public List<decimal> GetValues()
    {
        var values = new List<decimal>();
        if (MinValue.HasValue)
        {
            decimal currentValue = MinValue.Value;
            while (currentValue <= MaxValue)
            {
                values.Add(currentValue);
                currentValue += Step!.Value;
            }
        }
        else
        {
            foreach (decimal currentValue in Values!)
            {
                values.Add(currentValue);
            }
        }

        return values;
    }
    
    public decimal GetCloseValue(decimal point)
    {
        decimal result = 0;
        
        List<decimal> values = GetValues();
        decimal? previousValue = 0;
        foreach (var value in values.OrderBy(v => v))
        {
            if (!previousValue.HasValue && value > point)
                return value;

            if (previousValue.HasValue && previousValue < point && point > value)
            {
                if (Math.Abs((double) (point - previousValue)) >= Math.Abs((double) (value - point)))
                    return value;
                else
                    return previousValue.Value;
            }
            else
                result = value;
            
            previousValue = value;
        }

        return result;
    }
}