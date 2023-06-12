using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Services;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
       
        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost]
        [Route("AddWeatherForecast")]
        public IActionResult AddWeatherForecast([FromBody] Forecast forecast)
        {
            try
            {
                _weatherForecastService.AddWeatherForecast(forecast);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetWeeklyWeatherForecast")]
        public IActionResult GetWeeklyWeatherForecast()
        {
            var forecastList = _weatherForecastService.GetWeeklyForecast();
           
            return Ok(forecastList);
        }
    }
}