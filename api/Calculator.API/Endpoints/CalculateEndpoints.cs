using Calculator.Domain.Entities;
using Calculator.Services;

namespace Calculator.API.Endpoints;

public static class CalculateEndpoints
{
    public static void MapCalculateEndpoints(this WebApplication app)
    {
        app.MapPost("/calculate", async (HttpRequest request) =>
        {
            var requestBody = await request.ReadFromJsonAsync<CalculateRequestBody>();
            if (requestBody == null || string.IsNullOrWhiteSpace(requestBody.Expression))
            {
                throw new ArgumentException("Expression in body is required.");
            }

            var result = CalculatorService.CalculateUsingDataTable(requestBody.Expression);
            return Results.Ok(result);
        });
    }
}
