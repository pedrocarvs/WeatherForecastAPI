using System;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Application.Services
{
        public interface IWeatherForecastService
        {
            void AddWeatherForecast(Forecast forecast);
            List<Forecast> GetWeeklyForecast();
        }
}