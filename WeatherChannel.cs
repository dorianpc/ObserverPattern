using System.Collections;
using System.Collections.Generic;
using System;
namespace ObserverPattern
{

    public class WeatherChannel : IObserver<ICollection<WeatherInfo>>
    {
        private string _name;
        private IDisposable _cancellation;
        private DateTime _lastUpdateTime;
        private ICollection<WeatherInfo> _weatherList = new List<WeatherInfo>();

        public WeatherChannel(string name)
        {
            this._name = name;
        }

        public virtual void Subscribe(WeatherStation provider)
        {
            _cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _cancellation.Dispose();
            _weatherList.Clear();
        }

        public void OnCompleted()
        {
            _weatherList.Clear();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ICollection<WeatherInfo> weatherList)
        {
            _lastUpdateTime = DateTime.Now;
            _weatherList = weatherList;

            System.Console.WriteLine($"Reporting from {this._name} - {this._lastUpdateTime}");
            foreach (var weatherInfo in _weatherList)
                Console.WriteLine($"{weatherInfo.City}: {FormatTemperature(weatherInfo.TemperatureF, 'F')}, {FormatTemperature(weatherInfo.TemperatureC, 'C')}");
        }

        public string FormatTemperature(double temperature, char symbol)
        {
            return string.Format("{0}°" + symbol, Math.Round(temperature, 2));
        }
    }
}