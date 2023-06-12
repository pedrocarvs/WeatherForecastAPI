using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Infrastructure.DataAccess
{
    public class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
        {
        }
        public DbSet<Forecast> Forecasts { get; set; }

    }
}
