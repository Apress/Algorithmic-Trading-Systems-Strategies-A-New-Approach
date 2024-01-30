using OptimizationAlgorithms.Common;
using OptimizationAlgorithms.Common.Enums;

namespace OptimizationAlgorithms.Algorithms.BruteForce;

public class BruteForceAlgorithm: IOptimizationAlgorithm
{
    private int _pointsCount;
    private List<FunctionVariable> _functionVariables;

    public void Init(AlgorithmInfo info, List<FunctionVariable> functionVariables)
    {
        _pointsCount = 
            (int)info.Params
                .First(p => p.Type == ParamTypes.PointsCount).Value;
        _functionVariables = functionVariables;
    }
    
    public AlgorithmTypeInfo GetTypeInfo()
    {
        var info = new AlgorithmTypeInfo(AlgorithmTypes.BruteForce);
        info.Params
            .Add(new AlgorithmTypeInfo_Param(ParamTypes.PointsCount, 100));

        return info;
    }

public (List<AlgorithmPoint> points, string? magicString)? GetNextPoints(List<ObjectiveFunctionResult>? previousResults = null, string? magicString = null)
{
    List<AlgorithmPoint> allPoints = GetPoints();
    IEnumerable<AlgorithmPoint> nextPoints = allPoints
        .Where(allPoint => 
            previousResults == null 
            || !previousResults.Exists(r => r.Point == allPoint))
        .Take(_pointsCount);
    return (nextPoints.ToList(), null);
}

private List<AlgorithmPoint> GetPoints(AlgorithmPoint? pointTemplate = null)
{
    pointTemplate ??= new AlgorithmPoint();
    
    FunctionVariable? remainderVariable = GetRemainderVariable();
    if (remainderVariable == null)
        return new List<AlgorithmPoint>(){ pointTemplate };
    
    var newTemplates = GetTemplates();
    
    var result = new List<AlgorithmPoint>();
    foreach (AlgorithmPoint template in newTemplates)
    {
        List<AlgorithmPoint> points = GetPoints(template);
        result.AddRange(points);
    }

    return result;
    
    FunctionVariable? GetRemainderVariable()
    {
        foreach (FunctionVariable functionVariable in _functionVariables)
        {
            if (!pointTemplate!.Values
                    .Exists(v => v.Id.Equals(functionVariable.Id))) 
                return functionVariable;
        }

        return null;
    }

    IEnumerable<AlgorithmPoint> GetTemplates()
    {
        List<decimal> values = remainderVariable.GetValues();
        var templates = new List<AlgorithmPoint>();
        foreach (decimal value in values)
        {
            AlgorithmPoint newTemplate = new AlgorithmPoint();
            newTemplate.Values
                .Add(new FunctionVariableValue(remainderVariable.Id, value));
        
            foreach (FunctionVariableValue item in pointTemplate.Values)
            {
                newTemplate.Values
                    .Add(new FunctionVariableValue(item.Id, item.Value));
            }
        
            templates.Add(newTemplate);
        }

        return templates;
    }
}
}











