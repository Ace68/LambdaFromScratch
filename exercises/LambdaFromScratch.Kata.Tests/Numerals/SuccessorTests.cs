using LambdaFromScratch.Kata.Numerals;
using static LambdaFromScratch.Kata.Numerals.ChurchNumerals;

namespace LambdaFromScratch.Kata.Tests.Numerals;

public class SuccessorTests
{
    [Fact]
    public void Successor_of_zero_behaves_like_one()
    {
        ChurchNumeral<int> zero = Zero;

        var result = Successor(zero)(x => x + 1)(0);

        Assert.Equal(1, result);
    }

    [Fact]
    public void Successor_of_one_behaves_like_two()
    {
        ChurchNumeral<int> one = One;

        var result = Successor(one)(x => x + 1)(0);

        Assert.Equal(2, result);
    }

    [Fact]
    public void Successor_of_two_behaves_like_three()
    {
        ChurchNumeral<int> two = Two;

        var result = Successor(two)(x => x + 1)(0);

        Assert.Equal(3, result);
    }

    [Fact]
    public void Successor_adds_one_more_application()
    {
        ChurchNumeral<string> two = Two;
        Func<string, string> addStar = value => value + "*";

        var result = Successor(two)(addStar)("");

        Assert.Equal("***", result);
    }
}
