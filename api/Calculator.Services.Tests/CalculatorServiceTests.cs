using FluentAssertions;
using Xunit;

namespace Calculator.Services.Tests;

public class CalculatorServiceTests
{
    [Fact]
    public void CalculateUsingDataTable_ReturnsResult()
    {
        var result = CalculatorService.CalculateUsingDataTable("1+1");
        result.Should().Be(2);

        result = CalculatorService.CalculateUsingDataTable("1-1");
        result.Should().Be(0);

        result = CalculatorService.CalculateUsingDataTable("1*1");
        result.Should().Be(1);

        result = CalculatorService.CalculateUsingDataTable("1/1");
        result.Should().Be(1);

        result = CalculatorService.CalculateUsingDataTable("1+1*2");
        result.Should().Be(3);

        result = CalculatorService.CalculateUsingDataTable("1+1*2/2");
        result.Should().Be(2);

        result = CalculatorService.CalculateUsingDataTable("1+1*2/2-1");
        result.Should().Be(1);
    }

    [Fact]
    public void CalculateUsingDataTable_ReturnsComplexResult()
    {
        var result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)");
        result.Should().Be(-42.333333333333336);

        result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)+7*(8-9)");
        result.Should().Be(-49.333333333333336);

        result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)+7*(8-9)+10/(11-12)");
        result.Should().Be(-59.333333333333336);

        result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)+7*(8-9)+10/(11-12)-13*(14+15)");
        result.Should().Be(-436.3333333333333);

        result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)+7*(8-9)+10/(11-12)-13*(14+15)+16/(17-18)");
        result.Should().Be(-452.3333333333333);

        result = CalculatorService.CalculateUsingDataTable("1+(2/3)-4*(5+6)+7*(8-9)+10/(11-12)-13*(14+15)+16/(17-18)-19*(20+21)");
        result.Should().Be(-1231.3333333333333);
    }

    [Fact]
    public void CalculateUsingDataTable_ThrowsInvalidCharactersException()
    {
        List<char> invalidCharacters = [
            '!', '"', '#', '$', '&', '\'', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '{', '|', '}', '~', '`',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        ];

        foreach (var invalidCharacter in invalidCharacters)
        {
            var act = () => CalculatorService.CalculateUsingDataTable($"{invalidCharacter}");
            act.Should().Throw<ArgumentException>().WithMessage("Invalid characters in the expression.");
        }
    }

    [Fact]
    public void CalculateUsingDataTable_ThrowsOverflowException()
    {
        var act = () => CalculatorService.CalculateUsingDataTable("1/0");
        act.Should().Throw<OverflowException>().WithMessage("The result is too large or too small. You probably shouldn't divide by zero.");

        act = () => CalculatorService.CalculateUsingDataTable("1+1/0");
        act.Should().Throw<OverflowException>().WithMessage("The result is too large or too small. You probably shouldn't divide by zero.");

        act = () => CalculatorService.CalculateUsingDataTable("1/-0");
        act.Should().Throw<OverflowException>().WithMessage("The result is too large or too small. You probably shouldn't divide by zero.");
    }

    [Fact]
    public void CalculateUsingDataTable_ThrowsInvalidExpressionFormatException()
    {
        var act = () => CalculatorService.CalculateUsingDataTable("1+");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("1+1+");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("2//");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("3**");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("4..");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("5()");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("6(1)");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("7(1+1)");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");

        act = () => CalculatorService.CalculateUsingDataTable("8(1+1))");
        act.Should().Throw<ArgumentException>().WithMessage("Invalid expression format.");
    }
}
