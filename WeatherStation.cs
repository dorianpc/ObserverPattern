using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObserverPattern
{
    public class WeatherStation : IObservable<ICollection<WeatherInfo>>
    {
        private BlockingCollection<IObserver<ICollection<WeatherInfo>>> _observers = new BlockingCollection<IObserver<ICollection<WeatherInfo>>>();
        private Dictionary<string, WeatherInfo> _weatherList = new Dictionary<string, WeatherInfo>();

        public IDisposable Subscribe(IObserver<ICollection<WeatherInfo>> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public void ReportTemperature(string city, double temperatureF)
        {

            if (_weatherList.ContainsKey(city))
            {
                if (_weatherList[city].TemperatureF != temperatureF)
                {
                    _weatherList[city].TemperatureF = temperatureF;
                    foreach (var observer in _observers)
                        observer.OnNext(_weatherList.Values.ToList());
                }
            }
            else
            {
                _weatherList.Add(city, new WeatherInfo { City = city, TemperatureF = temperatureF, UpdateDateTime = DateTime.Now });
                foreach (var observer in _observers)
                    observer.OnNext(_weatherList.Values.ToList());
            }

        }
    }
}