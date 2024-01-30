using System.Text.Json;

namespace OptimizationAlgorithms.Algorithms.Genetic;

public class MagicData
{
    public int GenerationNumber { get; set; }
    public int StepIndex { get; set; }
    public IList<int> PopulationIds { get; set; }

    public MagicData(){}

    public MagicData(string? jsonString = null)
    {
        if (jsonString != null)
        {
            MagicData? obj = JsonSerializer
                .Deserialize<MagicData>(jsonString);
            if (obj == null)
                throw new ArgumentException(nameof(jsonString));
            
            GenerationNumber = obj.GenerationNumber;
            StepIndex = obj.StepIndex;
            PopulationIds = obj.PopulationIds;
        }
    }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}