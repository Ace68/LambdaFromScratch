namespace LambdaFromScratch.Kata.Functions;

public static class IdentityFunction
{
    // Implement ID = λx.x
    public static T Identity<T>(T x) => default!;
}
