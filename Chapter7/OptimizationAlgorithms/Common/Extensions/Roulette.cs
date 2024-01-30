namespace OptimizationAlgorithms.Common.Extensions;

public static class Roulette
{
    public static T Selection<T>(List<T> list, Random? rand = null)
    {
        var random = rand ?? new Random();
        int i = random.Next(0, list.Count - 1);
        return list[i];
    }

    public static List<T> SelectionDistinct<T>(List<T> list, int size, Random? rand = null)
    {
        var result = new List<T>();
        var random = rand ?? new Random();

        var choiseList = new List<T>(list);
        for (int i = 0; i < size; i++)
        {
            var item = Selection(choiseList, random);
            result.Add(item);
            choiseList.Remove(item);
        }

        return result;
    }

public static List<T> Selection<T>(
    List<(T Entity, double ProbabilityWeight)> list, 
    int size, 
    Random? rand = null)
{
    var result = new List<T>();
    
    List<ProbabilityElement<T>> probabilityElements = 
        GetProbabilityElements(list);
    var random = rand ?? new Random();
    for (int i = 0; i < size; i++)
    {
        double u = random.NextDouble();
        ProbabilityElement<T> nextPoint = 
            probabilityElements
                .First(e => u > e.MinInterval && u <= e.MaxInterval);
        result.Add(nextPoint.ListElement.Entity);
    }

    return result;
}
    
    public static List<T> SelectionDistinct<T>(List<(T Entity, double ProbabilityWeight)> list, int size, Random? rand = null)
    {
        var result = new List<T>();

        var changeList = new List<(T Entity, double ProbabilityWeight)>(list);
        List<ProbabilityElement<T>> probabilityElements = GetProbabilityElements(changeList);
        var random = rand ?? new Random();
        for (int i = 0; i < size; i++)
        {
            double u = random.NextDouble();
            ProbabilityElement<T> nextPoint = probabilityElements.First(e => u > e.MinInterval && u <= e.MaxInterval);
            result.Add(nextPoint.ListElement.Entity);

            changeList.Remove(nextPoint.ListElement);
            probabilityElements = GetProbabilityElements(changeList);
        }

        return result;
    }
    
    private static List<ProbabilityElement<T>> GetProbabilityElements<T>(List<(T Entity, double ProbabilityWeight)> list)
    {
        var result = new List<ProbabilityElement<T>>();
        
        double allWeight = list.Sum(i => i.ProbabilityWeight);
        double lastMaxInterval = 0;
        foreach ((T Entity, double ProbabilityWeight) item in list)
        {
            double weight = item.ProbabilityWeight / allWeight;
            var element = new ProbabilityElement<T>()
            {
                ListElement = item,
                MinInterval = lastMaxInterval,
                MaxInterval = lastMaxInterval + weight
            };
            result.Add(element);
            
            lastMaxInterval += weight;
        }

        return result;
    }

    private record ProbabilityElement<T>
    {
        public (T Entity, double ProbabilityWeight) ListElement;
        public double ProbabilityWeight;
        public double MinInterval;
        public double MaxInterval;
    }
}