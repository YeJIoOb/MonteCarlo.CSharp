// ReSharper disable PossibleMultipleEnumeration
namespace MonteCarlo.Fun;

internal static class ThreadLocalRandom
{
    [ThreadStatic]
    private static Random? _local; // only accessed 

    public static Random Instance => _local ??= new Random();
}

public class Dice
{
    private readonly double _chance;

    public Dice(double chance)
    {
        _chance = chance;
    }

    public int Check()
    {
        return ThreadLocalRandom.Instance.NextDouble() < _chance ? 1 : 0;
    }
}

public class FeatureDice
{
    private readonly double _chanceToSuccess;
    private readonly IEnumerable<KeyValuePair<int, double>> _chancesToResult;
        
    public FeatureDice(double chanceToSuccess, Dictionary<int, double> chancesToResult)
    {
        _chanceToSuccess = chanceToSuccess;
        var prop = from entry in chancesToResult orderby entry.Value ascending select entry;
        _chancesToResult = prop.Select((kvp, index) =>
        {
            var slice = prop.Where((_, index2) => index < index2);
            var agg = slice.Aggregate(0.0, (accumulator, kvp3) => accumulator + kvp3.Value);
            return new KeyValuePair<int, double>(kvp.Key, agg + kvp.Value);
        }).Reverse();

    }

    public int Check()
    {
        var isSuccess = ThreadLocalRandom.Instance.NextDouble() < _chanceToSuccess;
        if (!isSuccess) return 0;
        var checkToValProb = ThreadLocalRandom.Instance.NextDouble();
        foreach (var kvp in _chancesToResult)
        {
            var (key, value) = kvp;
            if (checkToValProb < value) return key;
        }

        return 0;
    }
}