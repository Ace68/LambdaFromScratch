using LambdaFromScratch.Numerals;

namespace LambdaFromScratch.Observation;

public static class ChurchNumeralObservation
{
    public static int ToInt(ChurchNumeral<int> numeral) =>
        numeral(x => x + 1)(0);
}
