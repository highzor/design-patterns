using System.Xml.Linq;

public class Program
{
    private static void Main(string[] args)
    {
        var stock = new Stock();
        var bank = new Bank("UnitBank", stock);
        var broker = new Broker("Oliver Twist", stock);
        stock.Market();
        broker.StopTrade();
        stock.Market();

        Console.Read();
    }
}

public interface IObserver
{
    void Update(object anyInfo);
}

public interface IObservable
{
    void RegisterObderver(IObserver observer);
    void RemoveObderver(IObserver observer);
    void NotifyObservers();
}

public class Stock : IObservable
{
    StockInfo ExchangeInfo { get; set; }
    List<IObserver> Observers { get; set; }

    public Stock()
    {
        ExchangeInfo = new StockInfo();
        Observers = new List<IObserver>();
    }

    public void RegisterObderver(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void RemoveObderver(IObserver observer)
    {
        Observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in Observers)
        {
            observer.Update(ExchangeInfo);
        }
    }

    public void Market()
    {
        Random rnd = new Random();
        ExchangeInfo.USD = rnd.Next(20, 40);
        ExchangeInfo.EU = rnd.Next(30, 50);
        Console.WriteLine($"Stock has updated");
        NotifyObservers();
    }
}

public class StockInfo
{
    public int USD { get; set; }
    public int EU { get; set; }
}

public class Broker : IObserver
{
    string Name { get; set; }
    IObservable Stock { get; set; }

    public Broker(string name, IObservable observe)
    {
        Name = name;
        Stock = observe;
        Stock.RegisterObderver(this);
    }

    public void Update(object anyInfo)
    {
        var stockInfo = anyInfo as StockInfo;
        if (stockInfo?.USD > 30)
            Console.WriteLine($"Broker {Name} is selling dollars; USD: {stockInfo?.USD}");
        else
            Console.WriteLine($"Broker {Name} is buying dollars; USD: {stockInfo?.USD}");
    }

    public void StopTrade()
    {
        Stock.RemoveObderver(this);
        Stock = null;
    }
}

public class Bank : IObserver
{
    string Name { get; set; }
    IObservable Stock { get; set; }

    public Bank(string name, IObservable observe)
    {
        Name = name;
        Stock = observe;
        Stock.RegisterObderver(this);
    }

    public void Update(object anyInfo)
    {
        var stockInfo = anyInfo as StockInfo;
        if (stockInfo?.EU > 30)
            Console.WriteLine($"Bank {Name} is selling euro; EU: {stockInfo?.EU}");
        else
            Console.WriteLine($"Bank {Name} is buying euro; EU: {stockInfo?.EU}");
    }

    public void StopTrade()
    {
        Stock.RemoveObderver(this);
        Stock = null;
    }
}