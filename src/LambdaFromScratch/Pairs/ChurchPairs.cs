using static LambdaFromScratch.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Pairs;

/// <summary>
/// PAIR   = λx.λy.λf.f x y
/// FIRST  = λp.p TRUE
/// SECOND = λp.p FALSE
/// A pair does not have to store two fields.
/// It can be a function that knows how to give its two values to another function.
/// </summary>
public static class ChurchPairs
{
    public static Func<T, ChurchPair<T>> Pair<T>(T x) =>
        y => selector => selector(x)(y);

    public static T First<T>(ChurchPair<T> p) =>
        p(True<T>);

    public static T Second<T>(ChurchPair<T> p) =>
        p(False<T>);
}
