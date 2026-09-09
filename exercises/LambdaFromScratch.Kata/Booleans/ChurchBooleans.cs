namespace LambdaFromScratch.Kata.Booleans;

public static class ChurchBooleans
{
    // Implement TRUE = λx.λy.x
    public static Func<T, T> True<T>(T x) => y => default!;

    // Implement FALSE = λx.λy.y
    public static Func<T, T> False<T>(T x) => y => default!;
}
