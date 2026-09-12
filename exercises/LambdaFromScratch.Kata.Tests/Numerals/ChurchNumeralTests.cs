using static LambdaFromScratch.Kata.Numerals.ChurchNumerals;

namespace LambdaFromScratch.Kata.Tests.Numerals;

public class ChurchNumeralTests
{
    [Fact]
    public void Zero_applies_a_string_transformation_zero_times()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Zero(addStar)("");

        Assert.Equal("", result);
    }

    [Fact]
    public void One_applies_a_string_transformation_once()
    {
        Func<string, string> addStar = value => value + "*";

        var result = One(addStar)("");

        Assert.Equal("*", result);
    }

    [Fact]
    public void Two_applies_a_string_transformation_twice()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Two(addStar)("");

        Assert.Equal("**", result);
    }

    [Fact]
    public void Three_applies_a_string_transformation_three_times()
    {
        Func<string, string> addStar = value => value + "*";

        var result = Three(addStar)("");

        Assert.Equal("***", result);
    }
}
