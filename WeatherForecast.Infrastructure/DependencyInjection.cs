using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Infrastructure.DataAccess;
using WeatherForecast.Infrastructure.Repositories;

namespace WeatherForecast.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string? connectionString )
        {
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            var dbContext = services.AddDbContext<WeatherForecastDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
