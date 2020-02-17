using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ObserverPattern
{
    public class Unsubscriber : IDisposable
    {
        private BlockingCollection<IObserver<ICollection<WeatherInfo>>> _observers;
        private IObserver<ICollection<WeatherInfo>> _observer;

        internal Unsubscriber(BlockingCollection<IObserver<ICollection<WeatherInfo>>> observers, IObserver<ICollection<WeatherInfo>> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            IObserver<ICollection<WeatherInfo>> observer;

            _observers.TryTake(out observer);

            if (observer != null)
                observer = null;
        }
    }
}