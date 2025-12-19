var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

// Make Program accessible for testing (WebApplicationFactory and Architecture tests)
namespace Api
{
    public partial class Program;
}