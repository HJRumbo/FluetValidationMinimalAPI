using EjemploFluentValidations.Entidades;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<Program>(lifetime: ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/person", ([FromServices] IValidator<Persona> validator, [FromBody] Persona persona) =>
{
    ValidationResult validationResult = validator.Validate(persona);

    if (!validationResult.IsValid)
    {
        return Task.FromResult(Results.ValidationProblem(validationResult.ToDictionary()));
    }

    return Task.FromResult(Results.Ok(persona));
})
.WithName("PostPersona");

app.MapPost("/compra", ([FromServices] IValidator<Compra> validator, [FromBody] Compra compra) =>
{
    ValidationResult validationResult = validator.Validate(compra);

    if (!validationResult.IsValid)
    {
        return Task.FromResult(Results.ValidationProblem(validationResult.ToDictionary()));
    }

    return Task.FromResult(Results.Ok(compra));
})
.WithName("PostCompra");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

