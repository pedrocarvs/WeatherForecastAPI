using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;
using WeatherForecast.Infrastructure.DataAccess;

namespace WeatherForecast.Infrastructure.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WeatherForecastDbContext _dbContext;

        public WeatherForecastRepository(WeatherForecastDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddForecast(Forecast forecast)
        {
            _dbContext.Forecasts.Add(forecast);
            _dbContext.SaveChanges();
        }

        public List<Forecast> GetWeeklyForecast()
        {
            var endDate = DateTime.Today.AddDays(7);
            return _dbContext.Forecasts.Where(f => f.Date.Date <= endDate.Date).ToList();
        }
    }
}
