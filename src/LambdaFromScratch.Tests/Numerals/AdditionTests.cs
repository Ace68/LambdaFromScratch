using LambdaFromScratch.Numerals;
using static LambdaFromScratch.Numerals.ChurchNumerals;
using static LambdaFromScratch.Observation.ChurchNumeralObservation;

namespace LambdaFromScratch.Tests.Numerals;

public class AdditionTests
{
    [Fact]
    public void Zero_plus_zero_is_zero()
    {
        ChurchNumeral<int> zero = Zero;

        var result = ToInt(Add(zero)(zero));

        Assert.Equal(0, result);
    }

    [Fact]
    public void Zero_plus_two_is_two()
    {
        ChurchNumeral<int> zero = Zero;
        ChurchNumeral<int> two = Two;

        var result = ToInt(Add(zero)(two));

        Assert.Equal(2, result);
    }

    [Fact]
    public void Two_plus_zero_is_two()
    {
        ChurchNumeral<int> zero = Zero;
        ChurchNumeral<int> two = Two;

        var result = ToInt(Add(two)(zero));

        Assert.Equal(2, result);
    }

    [Fact]
    public void One_plus_two_is_three()
    {
        ChurchNumeral<int> one = One;
        ChurchNumeral<int> two = Two;

        var result = ToInt(Add(one)(two));

        Assert.Equal(3, result);
    }

    [Fact]
    public void Two_plus_three_is_five()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Add(two)(three));

        Assert.Equal(5, result);
    }

    [Fact]
    public void Three_plus_two_is_five()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = ToInt(Add(three)(two));

        Assert.Equal(5, result);
    }

    [Fact]
    public void Addition_combines_function_applications()
    {
        ChurchNumeral<string> two = Two;
        ChurchNumeral<string> three = Three;
        Func<string, string> addStar = value => value + "*";

        var result = Add(two)(three)(addStar)("");

        Assert.Equal("*****", result);
    }
}
