using LambdaFromScratch.Kata.Booleans;
using LambdaFromScratch.Kata.Numerals;

namespace LambdaFromScratch.Kata.Predicates;

public static class ChurchPredicates
{
    // ISZERO = λn.n (λx.FALSE) TRUE
    public static ChurchBoolean<T> IsZero<T>(ChurchNumeral<ChurchBoolean<T>> n) =>
        x => y => default!;
}
