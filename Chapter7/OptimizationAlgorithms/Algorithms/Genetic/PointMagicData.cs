using System.Text.Json;

namespace OptimizationAlgorithms.Algorithms.Genetic;

public class PointMagicData
{
    public int GenerationNumber { get; set; }
    public int StepIndex { get; set; }
    public StepMagicData StepMagicData { get; set; }
    
    public PointMagicData(string? jsonString = null)
    {
        if (jsonString != null)
        {
            PointMagicData? obj = JsonSerializer.Deserialize<PointMagicData>(jsonString);
            if (obj == null)
                throw new ArgumentException(nameof(jsonString));

            StepMagicData = obj.StepMagicData;
            GenerationNumber = obj.GenerationNumber;
            StepIndex = obj.StepIndex;
        }
    }

    public PointMagicData(int generationNumber, int stepIndex)
    {
        GenerationNumber = generationNumber;
        StepIndex = stepIndex;
    }
    
    public PointMagicData()
    {
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}