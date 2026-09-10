using LambdaFromScratch.Kata.Numerals;
using static LambdaFromScratch.Kata.Numerals.ChurchNumerals;

namespace LambdaFromScratch.Kata.Tests.Numerals;

public class MultiplicationTests
{
    [Fact]
    public void One_times_three_is_three()
    {
        ChurchNumeral<int> one = One;
        ChurchNumeral<int> three = Three;

        var result = Multiply(one)(three)(x => x + 1)(0);

        Assert.Equal(3, result);
    }

    [Fact]
    public void Two_times_three_is_six()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = Multiply(two)(three)(x => x + 1)(0);

        Assert.Equal(6, result);
    }

    [Fact]
    public void Three_times_two_is_six()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = Multiply(three)(two)(x => x + 1)(0);

        Assert.Equal(6, result);
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
