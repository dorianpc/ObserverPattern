using System;

namespace ObserverPattern
{
    public class WeatherInfo
    {
        public string City { get; set; }

        public double TemperatureF { get; set; }

        public double TemperatureC => (TemperatureF - 32) * ((double)5 / 9);

        public DateTime UpdateDateTime { get; set; }

    }
}