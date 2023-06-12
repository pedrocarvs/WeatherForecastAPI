using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherForecast.Domain.Models
{
    public class Forecast
    {
           [JsonIgnore]
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int Temperature { get; set; }
            public string? Description { get; set; }


    }
}
