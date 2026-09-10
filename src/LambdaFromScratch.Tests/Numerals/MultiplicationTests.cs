using LambdaFromScratch.Numerals;
using static LambdaFromScratch.Numerals.ChurchNumerals;
using static LambdaFromScratch.Observation.ChurchNumeralObservation;

namespace LambdaFromScratch.Tests.Numerals;

public class MultiplicationTests
{
    [Fact]
    public void Zero_times_three_is_zero()
    {
        ChurchNumeral<int> zero = Zero;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Multiply(zero)(three));

        Assert.Equal(0, result);
    }

    [Fact]
    public void One_times_three_is_three()
    {
        ChurchNumeral<int> one = One;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Multiply(one)(three));

        Assert.Equal(3, result);
    }

    [Fact]
    public void Two_times_three_is_six()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Multiply(two)(three));

        Assert.Equal(6, result);
    }

    [Fact]
    public void Three_times_two_is_six()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Multiply(three)(two));

        Assert.Equal(6, result);
    }

    [Fact]
    public void Three_times_three_is_nine()
    {
        ChurchNumeral<int> three = Three;

        var result = ToInt(Multiply(three)(three));

        Assert.Equal(9, result);
    }

    [Fact]
    public void Multiplication_repeats_a_repeated_transformation()
    {
        ChurchNumeral<string> two = Two;
        ChurchNumeral<string> three = Three;
        Func<string, string> addStar = value => value + "*";

        var six = Multiply(two)(three);
        var result = six(addStar)("");

        Assert.Equal("******", result);
    }
}
