// public interface IObserver//subscriber
// {
//   void update(int temperature, int humidity, int pressure);
// }
//
//
// public class WeatherData {
//   private int temperature;
//   private int humidity;
//   private int pressure;
//
//   public delegate void WeaterChange(int temperature, int humidity, int pressure);
//
//   public event WeaterChange Change;
//
//   public WeatherData()
//   {
//
//   }
//
// }
//
//
// public class CurrentConditionsDisplay: IObserver
// {
//   private int temperature;
//   private int humidity;
//   private int pressure;
//   private WeatherData weatherData;
// ///chisht
//   public CurrentConditionsDisplay()
//   {
//     this.weatherData = new weatherData;
//     weatherData.Change += update;
//   }
// ///
//   public void update(int temperature, int humidity, int pressure)
//   {
//
//     this.temperature = temperature;
//     this.humidity = humidity;
//     this.pressure = pressure;
//     display();
//   }
//
//   public void display()
//   {
//     System.Console.WriteLine("Current conditions:");
//     System.Console.WriteLine("-Temperature: " + temperature.ToString() + " C");
//     System.Console.WriteLine("-Humidity: " + humidity.ToString() + "%");
//     System.Console.WriteLine("-Pressure: " + pressure.ToString() + " mmHg");
//     System.Console.WriteLine();
//   }
// }

using System;
namespace wildert
{
    public class Metronome
    {
        public event TickHandler Tick;
        public EventArgs e = null;
        public delegate void TickHandler(Metronome m, EventArgs e, int temperature, int humidity, int pressure);
        public void Start()
        {
            while (true)
            {
              System.Random rand = new System.Random();
              int temperature = rand.Next(0, 50);
              int humidity = rand.Next(0, 100);
              int pressure = rand.Next(300, 600);


                if (Tick != null)
                {
                    Tick(this, e, temperature, humidity, pressure);

                }
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
        public class Listener
        {
            public void Subscribe(Metronome m)
            {
                m.Tick += new Metronome.TickHandler(HeardIt);
            }
            private void HeardIt(Metronome m, EventArgs e, int temperature, int humidity, int pressure)
            {
                System.Console.WriteLine(temperature);
                System.Console.WriteLine(humidity);
                System.Console.WriteLine(pressure);

            }

        }
    class Test
    {
        static void Main()
        {
            Metronome m = new Metronome();
            Listener l = new Listener();
            l.Subscribe(m);
            m.Start();
        }
    }
}
