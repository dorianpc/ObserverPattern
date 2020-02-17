using System;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation provider = new WeatherStation();
            WeatherChannel observer = new WeatherChannel("Channel 1");
            WeatherChannel observer2 = new WeatherChannel("Channel 2");

            observer.Subscribe(provider);
            provider.ReportTemperature("Miami", 90);
            provider.ReportTemperature("New York", 72.5);
            provider.ReportTemperature("Miami", 90);
            provider.ReportTemperature("Miami", 95);
            observer.Unsubscribe();

            observer2.Subscribe(provider);
            provider.ReportTemperature("Cleveland", 79);
            provider.ReportTemperature("Miami", 85);
            observer2.Unsubscribe();



        }
    }
}
