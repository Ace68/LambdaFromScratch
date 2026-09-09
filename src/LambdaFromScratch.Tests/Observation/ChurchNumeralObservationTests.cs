using static LambdaFromScratch.Numerals.ChurchNumerals;
using static LambdaFromScratch.Observation.ChurchNumeralObservation;

namespace LambdaFromScratch.Tests.Observation;

public class ChurchNumeralObservationTests
{
    [Fact]
    public void ToInt_observes_zero()
    {
        var result = ToInt(Zero);

        Assert.Equal(0, result);
    }

    [Fact]
    public void ToInt_observes_one()
    {
        var result = ToInt(One);

        Assert.Equal(1, result);
    }

    [Fact]
    public void ToInt_observes_two()
    {
        var result = ToInt(Two);

        Assert.Equal(2, result);
    }

    [Fact]
    public void ToInt_observes_three()
    {
        var result = ToInt(Three);

        Assert.Equal(3, result);
    }
}
