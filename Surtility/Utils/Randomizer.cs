namespace Surtility.Utils;

public static class Randomizer
{
    private static Random _random = new(DateTime.Now.GetHashCode());

    public static void SetRandomSeed(int customSeed)
    {
        _random = new(customSeed);
    }

    public static bool GetHalfChance()
    {
        return _random.Next(0, 2) == 1;
    }

    public static bool ProcChance(float chance)
    {
        if (chance is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(chance), "Wrong parameter value was passed, expected in range of [0;1].");

        return _random.NextSingle() <= chance;
    }

    public static int GetInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static int GetInt(int max)
    {
        return _random.Next(max);
    }

    public static T GetRandomItem<T>(params T[] items)
    {
        return items[_random.Next(items.Length)];
    }
}
