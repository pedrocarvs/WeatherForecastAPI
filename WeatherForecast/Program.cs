using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using WeatherForecast.Application;
using WeatherForecast.Domain.Models;
using WeatherForecast.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
{                               

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("WeatherForecastDb"));



}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }



    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}


