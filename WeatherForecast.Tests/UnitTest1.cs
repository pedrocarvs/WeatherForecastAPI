using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using WeatherForecast.Api.Controllers;
using WeatherForecast.Application.Services;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Validators;
using Xunit;

namespace WeatherForecast.Tests
{
    public class UnitTest1
    {
        public class WeatherForecastTests
        {
            private WeatherForecastService _service;
            private Mock<IWeatherForecastRepository> _repositoryMock;
            private ForecastValidator _validator;
            private WeatherForecastController  _controller;
            public WeatherForecastTests()
            {
                _validator = new ForecastValidator();
                _repositoryMock = new Mock<IWeatherForecastRepository>();
                _service = new WeatherForecastService(_repositoryMock.Object, _validator);
                _controller = new WeatherForecastController(_service);
            }

            [Fact]
            public void AddWeatherForecast_ValidForecast_ReturnsOk()
            {
                // Arrange
                var forecast = new Forecast
                {
                    Date = DateTime.Today.AddDays(1),
                    Temperature = 25,
                    Description = "Sunny"
                };

                // Act
                var result = _controller.AddWeatherForecast(forecast);

                // Assert
                Assert.IsType<OkResult>(result);
                _repositoryMock.Verify(r => r.AddForecast(It.IsAny<Forecast>()), Times.Once);
            }
            [Fact]
            public void AddWeatherForecast_ForecastDateInPast_ReturnsBadRequest()
            {
                // Arrange
                var forecast = new Forecast
                {
                    Date = DateTime.Today.AddDays(-2),
                    Temperature = 25,
                    Description = "Sunny"
                };

                // Act
                var result = _controller.AddWeatherForecast(forecast);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Contains(@"Validation failed: 
 -- Date: Forecast date cannot be in the past. Severity: Error", badRequestResult.Value.ToString());
                _repositoryMock.Verify(r => r.AddForecast(It.IsAny<Forecast>()), Times.Never);
            }
            [Fact]
            public void AddWeatherForecast_InvalidTemperature_ReturnsBadRequest()
            {
                // Arrange
                var forecast = new Forecast
                {
                    Date = DateTime.Today.AddDays(1),
                    Temperature = 100,
                    Description = "Sunny"
                };

                // Act
                var result = _controller.AddWeatherForecast(forecast);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Contains(@"Validation failed: 
 -- Temperature: Temperature must be between -60 and 60 degrees. Severity: Error", badRequestResult.Value.ToString());
                _repositoryMock.Verify(r => r.AddForecast(It.IsAny<Forecast>()), Times.Never);
            }

            [Fact]
            public void GetWeeklyWeatherForecast_ReturnsOkWithForecasts()
            {
                // Arrange
                var forecasts = new List<Forecast>
            {
                new Forecast { Date = DateTime.Today, Temperature = 20, Description = "Cloudy" },
                new Forecast { Date = DateTime.Today.AddDays(1), Temperature = 25, Description = "Sunny" },
                new Forecast { Date = DateTime.Today.AddDays(2), Temperature = 18, Description = "Rainy" },
            };
                _repositoryMock.Setup(r => r.GetWeeklyForecast()).Returns(forecasts);

                // Act
                var result = _controller.GetWeeklyWeatherForecast();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(forecasts, okResult.Value);
            }
        }
    }
}