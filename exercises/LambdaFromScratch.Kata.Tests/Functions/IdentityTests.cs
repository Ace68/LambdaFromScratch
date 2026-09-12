using static LambdaFromScratch.Kata.Functions.IdentityFunction;

namespace LambdaFromScratch.Kata.Tests.Functions;

public class IdentityTests
{
    [Fact]
    public void Identity_returns_the_same_string()
    {
        var result = Identity("hello");

        Assert.Equal("hello", result);
    }

    [Fact]
    public void Identity_returns_the_same_integer()
    {
        var result = Identity(42);

        Assert.Equal(42, result);
    }
}
