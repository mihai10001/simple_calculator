using Calculator.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Calculator.Services;

public partial class CalculatorService : ICalculatorService
{
    public static double CalculateUsingDataTable(string expression)
    {
        if (!IsValidExpressionGeneratedRegex().IsMatch(expression))
        {
            throw new ArgumentException("Invalid characters in the expression.");
        }

        var result = EvaluateExpression(expression);

        if (result == double.PositiveInfinity || result == double.NegativeInfinity)
        {
            throw new OverflowException("The result is too large or too small. You probably shouldn't divide by zero.");
        }

        return result;
    }

    static double EvaluateExpression(string expression)
    {
        try
        {
            var dataTable = new System.Data.DataTable();
            var value = dataTable.Compute(expression, string.Empty);
            return Convert.ToDouble(value);
        }
        catch
        {
            throw new ArgumentException("Invalid expression format.");
        }
    }

    [GeneratedRegex(@"^[0-9+\-*/(). ]+$")]
    private static partial Regex IsValidExpressionGeneratedRegex();
}
