namespace LambdaFromScratch.Kata.Booleans;

public static class ChurchLogic
{
    // NOT = λp.λx.λy.p y x
    public static ChurchBoolean<T> Not<T>(ChurchBoolean<T> p) =>
        x => y => throw new NotImplementedException();

    // IF = λp.λx.λy.p x y
    public static ChurchBoolean<T> If<T>(ChurchBoolean<T> p) =>
        x => y => throw new NotImplementedException();

    // AND = λp.λq.λx.λy.p (q x y) y
    public static Func<ChurchBoolean<T>, ChurchBoolean<T>> And<T>(ChurchBoolean<T> p) =>
        q => x => y => throw new NotImplementedException();

    // OR = λp.λq.λx.λy.p x (q x y)
    public static Func<ChurchBoolean<T>, ChurchBoolean<T>> Or<T>(ChurchBoolean<T> p) =>
        q => x => y => throw new NotImplementedException();
}
