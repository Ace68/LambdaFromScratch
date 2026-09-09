using LambdaFromScratch.Kata.Booleans;
using static LambdaFromScratch.Kata.Booleans.ChurchBooleans;
using static LambdaFromScratch.Kata.Booleans.ChurchLogic;

namespace LambdaFromScratch.Kata.Tests.Booleans;

public class ChurchLogicTests
{
    [Fact]
    public void Not_of_true_selects_the_second_value()
    {
        ChurchBoolean<string> trueValue = True;

        var result = Not(trueValue)("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void Not_of_false_selects_the_first_value()
    {
        ChurchBoolean<string> falseValue = False;

        var result = Not(falseValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void If_with_true_selects_the_first_value()
    {
        ChurchBoolean<string> trueValue = True;

        var result = If(trueValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void If_with_false_selects_the_second_value()
    {
        ChurchBoolean<string> falseValue = False;

        var result = If(falseValue)("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void True_and_true_is_true()
    {
        ChurchBoolean<string> trueValue = True;

        var result = And(trueValue)(trueValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void True_and_false_is_false()
    {
        ChurchBoolean<string> trueValue = True;
        ChurchBoolean<string> falseValue = False;

        var result = And(trueValue)(falseValue)("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void False_and_true_is_false()
    {
        ChurchBoolean<string> trueValue = True;
        ChurchBoolean<string> falseValue = False;

        var result = And(falseValue)(trueValue)("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void False_and_false_is_false()
    {
        ChurchBoolean<string> falseValue = False;

        var result = And(falseValue)(falseValue)("yes")("no");

        Assert.Equal("no", result);
    }

    [Fact]
    public void True_or_true_is_true()
    {
        ChurchBoolean<string> trueValue = True;

        var result = Or(trueValue)(trueValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void True_or_false_is_true()
    {
        ChurchBoolean<string> trueValue = True;
        ChurchBoolean<string> falseValue = False;

        var result = Or(trueValue)(falseValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void False_or_true_is_true()
    {
        ChurchBoolean<string> trueValue = True;
        ChurchBoolean<string> falseValue = False;

        var result = Or(falseValue)(trueValue)("yes")("no");

        Assert.Equal("yes", result);
    }

    [Fact]
    public void False_or_false_is_false()
    {
        ChurchBoolean<string> falseValue = False;

        var result = Or(falseValue)(falseValue)("yes")("no");

        Assert.Equal("no", result);
    }
}
