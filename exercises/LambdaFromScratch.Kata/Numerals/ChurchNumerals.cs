namespace LambdaFromScratch.Kata.Numerals;

public static class ChurchNumerals
{
    // ZERO = λf.λx.x
    public static Func<T, T> Zero<T>(Func<T, T> f) =>
        x => throw new NotImplementedException();

    // ONE = λf.λx.f x
    public static Func<T, T> One<T>(Func<T, T> f) =>
        x => throw new NotImplementedException();

    // TWO = λf.λx.f (f x)
    public static Func<T, T> Two<T>(Func<T, T> f) =>
        x => throw new NotImplementedException();

    // THREE = λf.λx.f (f (f x))
    public static Func<T, T> Three<T>(Func<T, T> f) =>
        x => throw new NotImplementedException();
}
