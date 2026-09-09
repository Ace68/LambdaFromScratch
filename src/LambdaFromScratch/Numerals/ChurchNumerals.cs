namespace LambdaFromScratch.Numerals;

public static class ChurchNumerals
{
    public static Func<T, T> Zero<T>(Func<T, T> f) =>
        x => x;

    public static Func<T, T> One<T>(Func<T, T> f) =>
        x => f(x);

    public static Func<T, T> Two<T>(Func<T, T> f) =>
        x => f(f(x));

    public static Func<T, T> Three<T>(Func<T, T> f) =>
        x => f(f(f(x)));

    public static Func<T, T> Four<T>(Func<T, T> f) =>
        x => f(f(f(f(x))));

    public static Func<T, T> Five<T>(Func<T, T> f) =>
        x => f(f(f(f(f(x)))));

    public static Func<T, T> Six<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(x))))));

    public static Func<T, T> Seven<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(x)))))));

    public static Func<T, T> Eight<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(x))))))));

    public static Func<T, T> Nine<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(f(x)))))))));

    public static Func<T, T> Ten<T>(Func<T, T> f) =>
        x => f(f(f(f(f(f(f(f(f(f(x))))))))));

    public static ChurchNumeral<T> Successor<T>(ChurchNumeral<T> n) =>
        f => x => f(n(f)(x));
}
