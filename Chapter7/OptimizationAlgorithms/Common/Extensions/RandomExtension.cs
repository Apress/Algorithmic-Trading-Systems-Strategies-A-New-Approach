namespace OptimizationAlgorithms.Common.Extensions;

public static class RandomExtension
{
    public static List<T> GetList<T>(this Random random, List<T> list, int count)
    {
        if (list.Count == count)
            return list;

        List<T> result = new List<T>();
        List<T> changeList = new List<T>(list);
        while (result.Count < count)
        {
            int varNumber = random.Next(0, changeList.Count);
            T variable = list[varNumber];
            result.Add(variable);
            changeList.Remove(variable);
        }

        return result;
    }
}