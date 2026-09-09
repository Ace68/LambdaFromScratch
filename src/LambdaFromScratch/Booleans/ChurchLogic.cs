namespace LambdaFromScratch.Booleans;

public static class ChurchLogic
{
    public static ChurchBoolean<T> Not<T>(ChurchBoolean<T> p) =>
        x => y => p(y)(x);

    public static ChurchBoolean<T> If<T>(ChurchBoolean<T> p) =>
        x => y => p(x)(y);

    public static Func<ChurchBoolean<T>, ChurchBoolean<T>> And<T>(ChurchBoolean<T> p) =>
        q => x => y => p(q(x)(y))(y);

    public static Func<ChurchBoolean<T>, ChurchBoolean<T>> Or<T>(ChurchBoolean<T> p) =>
        q => x => y => p(x)(q(x)(y));
}
