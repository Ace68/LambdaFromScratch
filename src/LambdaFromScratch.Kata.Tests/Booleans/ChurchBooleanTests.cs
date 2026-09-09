using static LambdaFromScratch.Kata.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Kata.Tests.Booleans;

public class ChurchBooleanTests
{
    [Fact]
    public void True_selects_the_first_value()
    {
        var result = True("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void False_selects_the_second_value()
    {
        var result = False("yes")("no");

        Assert.Equal("no", result);
    }
}
