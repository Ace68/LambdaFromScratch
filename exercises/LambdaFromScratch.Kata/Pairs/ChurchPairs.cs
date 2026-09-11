namespace LambdaFromScratch.Kata.Pairs;

public static class ChurchPairs
{
    // PAIR = λx.λy.λf.f x y
    public static Func<T, ChurchPair<T>> Pair<T>(T x) =>
        y => selector => default!;

    // FIRST = λp.p TRUE
    public static T First<T>(ChurchPair<T> p) =>
        default!;

    // SECOND = λp.p FALSE
    public static T Second<T>(ChurchPair<T> p) =>
        default!;
}
