namespace LambdaFromScratch.Booleans;

/// <summary>
/// A boolean does not have to be represented as a stored `bool`.
/// It can be represented as behavior.
/// TRUE  = λx.λy.x
/// FALSE = λx.λy.y
/// </summary>
public static class ChurchBooleans
{
    // Uncurried versions of True and False
    public static Func<T, T> True<T>(T x) => y => x;

    public static Func<T, T> False<T>(T x) => y => y;

    // Curried versions of True and False
    public static Func<T, Func<T, T>> True<T>() =>
    x => y => x;

    public static Func<T, Func<T, T>> False<T>() =>
    x => y => y;
}
