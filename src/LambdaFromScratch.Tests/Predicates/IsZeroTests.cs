using static LambdaFromScratch.Numerals.ChurchNumerals;
using static LambdaFromScratch.Predicates.ChurchPredicates;

namespace LambdaFromScratch.Tests.Predicates;

public class IsZeroTests
{
    [Fact]
    public void Zero_is_zero()
    {
        var result = IsZero<string>(Zero);

        Assert.Equal("yes", result("yes")("no"));
    }

    [Fact]
    public void One_is_not_zero()
    {
        var result = IsZero<string>(One);

        Assert.Equal("no", result("yes")("no"));
    }

    [Fact]
    public void Two_is_not_zero()
    {
        var result = IsZero<string>(Two);

        Assert.Equal("no", result("yes")("no"));
    }

    [Fact]
    public void Three_is_not_zero()
    {
        var result = IsZero<string>(Three);

        Assert.Equal("no", result("yes")("no"));
    }
}
