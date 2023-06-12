using FluentValidation;
using System;

using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.Validators
{
    public class ForecastValidator : AbstractValidator<Forecast>
    {
        public ForecastValidator()
        {
            RuleFor(x => x.Temperature)
                .InclusiveBetween(-60, 60)
                .WithMessage("Temperature must be between -60 and 60 degrees.");

            RuleFor(x => x.Date)
                  .NotEmpty().WithMessage("Date is required.")
                  .Must(BeInPast).WithMessage("Forecast date cannot be in the past.");
        }
        private bool BeInPast(DateTime date)
        {
            return date >= DateTime.Today;
        }
    }
}
