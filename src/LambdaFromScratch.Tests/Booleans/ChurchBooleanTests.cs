using static LambdaFromScratch.Booleans.ChurchBooleans;

namespace LambdaFromScratch.Tests.Booleans;

public class ChurchBooleanTests
{
    [Fact]
    public void True_selects_the_first_string()
    {
        var result = True("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void False_selects_the_second_string()
    {
        var result = False("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void True_selects_the_first_integer()
    {
        var result = True(1)(2);

        Assert.Equal(1, result);
    }

    [Fact]
    public void False_selects_the_second_integer()
    {
        var result = False(1)(2);

        Assert.Equal(2, result);
    }
}
