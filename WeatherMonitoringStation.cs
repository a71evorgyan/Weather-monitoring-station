using System.Collections;
using System;

public interface IObserver//subscriber
{
  void update(int temperature, int humidity, int pressure);
}

public interface ISubject//publisher
{
    void registerObserver(IObserver o);
    void removeObserver(IObserver o);
    void notifyObservers();
}


public class WeatherData : ISubject
{

  private int temperature;
  private int humidity;
  private int pressure;
  private ArrayList observers;



  public WeatherData()
  {
    observers = new ArrayList();
  }

  public void registerObserver(IObserver o)
  {
    observers.Add(o);
  }

  public void removeObserver(IObserver o)
  {
    int i = observers.IndexOf(o);
    if(i >= 0){
      observers.RemoveAt(i);
    }
  }

  public void notifyObservers()
  {
    for (int i = 0; i < observers.Count; i++)
    {
      IObserver observer = (IObserver) observers[i];
      observer.update(temperature, humidity, pressure);
    }
  }

  public void measurmentsChanged()
  {
    notifyObservers();
  }

  public void Subscribe(WeatherStation w)
  {
    w.Event += new WeatherStation.EventHandler(setMeasurments);
  }

  public void setMeasurments(WeatherStation w, EventArgs e, int temperature, int humidity, int pressure)
  {
    this.temperature = temperature;
    this.humidity = humidity;
    this.pressure = pressure;
    measurmentsChanged();//called notifyObservers() which update observers,which in Array
  }

}

public class CurrentConditionsDisplay : IObserver
{
  private int temperature;
  private int humidity;
  private int pressure;
  private ISubject weatherData; //pointer to WeatherData

  public CurrentConditionsDisplay(ISubject weatherData)
  {
    this.weatherData = weatherData;
    weatherData.registerObserver(this);
  }

  public void update(int temperature, int humidity, int pressure)
  {
    this.temperature = temperature;
    this.humidity = humidity;
    this.pressure = pressure;
    display();
  }

  public void display()
  {
    System.Console.WriteLine("Current conditions:");
    System.Console.WriteLine("-Temperature: " + temperature.ToString() + " C");
    System.Console.WriteLine("-Humidity: " + humidity.ToString() + "%");
    System.Console.WriteLine("-Pressure: " + pressure.ToString() + " mmHg");
    System.Console.WriteLine();
  }
}


public class WeatherStation
{
  public event EventHandler Event;
  public EventArgs e = null;
  public delegate void EventHandler(WeatherStation w, EventArgs e, int temperature, int humidity, int pressure);
  public void start()
  {
    while(true)
    {
      System.Random rand = new System.Random();
      int temperature = rand.Next(0, 50);
      int humidity = rand.Next(0, 100);
      int pressure = rand.Next(300, 600);
      if(Event != null)
      {
        Event(this, e, temperature, humidity, pressure);
      }
      System.Threading.Thread.Sleep(2000);
    }
    }
}

public class WeatherMonitoringStation
{
 public static void Main()
 {
   WeatherData weatherData = new WeatherData();
   CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherData);
   weatherData.Subscribe(weatherStation);
   weatherStation.start();
 }
}
