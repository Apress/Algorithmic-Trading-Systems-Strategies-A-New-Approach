namespace OptimizationAlgorithms.Common;

public class AlgorithmPoint
{
    public string MagicData;
    public List<FunctionVariableValue> Values { get; } = new();
    
    public static bool operator ==(AlgorithmPoint? a, AlgorithmPoint? b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if(a is null || b is null)
            return false;

        if(a.Values.Count != b.Values.Count)
            return false;

        return a.Values
            .OrderBy(v => v.Id)
            .SequenceEqual(b.Values.OrderBy(v => v.Id));
    }
    
    public static bool operator !=(AlgorithmPoint a, AlgorithmPoint b)
    {
        return !(a == b);
    }
}