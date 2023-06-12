using FluentValidation;
using System;
using System.Collections.Generic;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _repository;
        private readonly IValidator<Forecast> _validator;


        public WeatherForecastService(IWeatherForecastRepository repository, IValidator<Forecast> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public void AddWeatherForecast(Forecast forecast)
        {
            var validationResult = _validator.Validate(forecast);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var description = GetTemperatureDescription(forecast.Temperature);
            forecast.Description = description;
            _repository.AddForecast(forecast);
        }

        public List<Forecast> GetWeeklyForecast()
        {
            var forecasts = _repository.GetWeeklyForecast();
            var result = new List<Forecast>();

            foreach (var forecast in forecasts)
            {
                result.Add(forecast);
            }
            return result;
        }

        private string GetTemperatureDescription(int temperature)
        {
            if (temperature <= -20) return "Freezing";
   
            else if (temperature < 0) return "Bracing";
            
            else if (temperature < 10) return "Chilly";
            
            else if (temperature < 20) return "Cool";
            
            else if (temperature < 25) return "Mild";
            
            else if (temperature < 30) return "Warm";
          
            else if (temperature < 35) return "Balmy";
           
            else if (temperature < 40) return "Hot";
          
            else if (temperature < 45) return "Sweltering";
            
            else return "Scorching";
        }
    }
}