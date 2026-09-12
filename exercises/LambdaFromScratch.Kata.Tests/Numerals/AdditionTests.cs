using LambdaFromScratch.Kata.Numerals;
using static LambdaFromScratch.Kata.Numerals.ChurchNumerals;

namespace LambdaFromScratch.Kata.Tests.Numerals;

public class AdditionTests
{
    [Fact]
    public void Zero_plus_two_is_two()
    {
        ChurchNumeral<int> zero = Zero;
        ChurchNumeral<int> two = Two;

        var result = Add(zero)(two)(x => x + 1)(0);

        Assert.Equal(2, result);
    }

    [Fact]
    public void One_plus_two_is_three()
    {
        ChurchNumeral<int> one = One;
        ChurchNumeral<int> two = Two;

        var result = Add(one)(two)(x => x + 1)(0);

        Assert.Equal(3, result);
    }

    [Fact]
    public void Two_plus_three_is_five()
    {
        ChurchNumeral<int> two = Two;
        ChurchNumeral<int> three = Three;

        var result = Add(two)(three)(x => x + 1)(0);

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
