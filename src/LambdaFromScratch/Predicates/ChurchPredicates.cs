using LambdaFromScratch.Booleans;
using LambdaFromScratch.Numerals;
using static LambdaFromScratch.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Predicates;

/// <summary>
/// ISZERO = λn.n (λx.FALSE) TRUE
/// ZERO applies the supplied function zero times, so the initial TRUE survives.
/// Every other Church numeral applies the supplied function at least once, replacing the result with FALSE.
/// </summary>
public static class ChurchPredicates
{
    public static ChurchBoolean<T> IsZero<T>(ChurchNumeral<ChurchBoolean<T>> n) =>
        n(_ => False<T>)(True<T>);
}
