using LambdaFromScratch.Booleans;
using LambdaFromScratch.Pairs;
using static LambdaFromScratch.Pairs.ChurchPairs;

namespace LambdaFromScratch.Tests.Pairs;

public class ChurchPairTests
{
    [Fact]
    public void First_returns_the_first_value()
    {
        var pair = Pair("left")("right");

        var result = First(pair);

        Assert.Equal("left", result);
    }

    [Fact]
    public void Second_returns_the_second_value()
    {
        var pair = Pair("left")("right");

        var result = Second(pair);

        Assert.Equal("right", result);
    }

    [Fact]
    public void Pair_works_with_integers()
    {
        var pair = Pair(10)(20);

        Assert.Equal(10, First(pair));
        Assert.Equal(20, Second(pair));
    }

    [Fact]
    public void Pair_gives_both_values_to_a_custom_selector()
    {
        var pair = Pair("left")("right");
        ChurchBoolean<string> join = x => y => $"{x}:{y}";

        var result = pair(join);

        Assert.Equal("left:right", result);
    }
}
