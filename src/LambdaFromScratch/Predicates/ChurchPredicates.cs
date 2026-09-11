using LambdaFromScratch.Booleans;
using LambdaFromScratch.Numerals;
using static LambdaFromScratch.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Predicates;

public static class ChurchPredicates
{
    public static ChurchBoolean<T> IsZero<T>(ChurchNumeral<ChurchBoolean<T>> n) =>
        n(_ => False<T>)(True<T>);
}
