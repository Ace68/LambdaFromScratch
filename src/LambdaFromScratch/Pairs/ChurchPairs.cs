using static LambdaFromScratch.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Pairs;

public static class ChurchPairs
{
    public static Func<T, ChurchPair<T>> Pair<T>(T x) =>
        y => selector => selector(x)(y);

    public static T First<T>(ChurchPair<T> p) =>
        p(True<T>);

    public static T Second<T>(ChurchPair<T> p) =>
        p(False<T>);
}
