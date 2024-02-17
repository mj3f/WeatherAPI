using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(); // Important if you're not using Minimal APIs!
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Weather API",
        Description = "A simple example ASP.NET Core Web API - Inspired by Nick Chapsas video (see Contact url)",
        Contact = new OpenApiContact
        {
            Name = "Mike Pratt",
            Email = string.Empty,
            Url = new Uri("https://www.youtube.com/watch?v=A2pKhNQoQUU&t=32s"),
        },
        License = new OpenApiLicense
        {
            Name = "MIT"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

// app.MapGet("/weatherforecast", () =>
    // {
    //     var forecast = Enumerable.Range(1, 5).Select(index =>
    //             new WeatherForecast
    //             (
    //                 DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //                 Random.Shared.Next(-20, 55),
    //                 summaries[Random.Shared.Next(summaries.Length)]
    //             ))
    //         .ToArray();
    //     return forecast;
    // })
    // .WithName("GetWeatherForecast")
    // .WithOpenApi();

app.Run();