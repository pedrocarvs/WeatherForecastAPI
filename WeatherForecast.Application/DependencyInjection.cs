using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Services;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Validators;


namespace WeatherForecast.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IValidator<Forecast>, ForecastValidator>();
            return services;
        }
    }
}
