using static LambdaFromScratch.Numerals.ChurchNumerals;

namespace LambdaFromScratch.Tests.Numerals;

public class ChurchNumeralTests
{
    [Fact]
    public void Zero_applies_a_function_zero_times()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Zero(addStar)("");

        Assert.Equal("", result);
    }

    [Fact]
    public void One_applies_a_function_once()
    {
        Func<string, string> addStar = value => value + "*";

        var result = One(addStar)("");

        Assert.Equal("*", result);
    }

    [Fact]
    public void Two_applies_a_function_twice()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Two(addStar)("");

        Assert.Equal("**", result);
    }

    [Fact]
    public void Three_applies_a_function_three_times()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Three(addStar)("");

        Assert.Equal("***", result);
    }
}
