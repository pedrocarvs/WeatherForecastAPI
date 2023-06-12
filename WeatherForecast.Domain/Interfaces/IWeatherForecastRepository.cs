using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.Interfaces
{
    public interface IWeatherForecastRepository
    {
        void AddForecast(Forecast forecast);
        List<Forecast> GetWeeklyForecast();
    }
}
