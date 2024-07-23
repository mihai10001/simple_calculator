using Calculator.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapCalculateEndpoints();

app.Run();
