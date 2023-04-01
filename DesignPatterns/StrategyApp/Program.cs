//The Strategy Pattern defines a family of algorithms,
//encapsulates each one, and makes them interchangeable.
//Strategy lets the algorithm vary independently from
//clients that use it.

public class Program
{
    private static void Main(string[] args)
    {
        var car = new Car(4, "Toyota", new PetrolMove());
        car.MakeMove();
        car.MoveBehavior = new ElectricMove();
        car.MakeMove();

        Console.ReadLine();
    }
}

public interface IMovable
{
    void Move();
}

public class PetrolMove : IMovable
{
    public void Move()
    {
        Console.WriteLine("Petrol move!");
    }
}

public class ElectricMove : IMovable
{
    public void Move()
    {
        Console.WriteLine("Electric move!");
    }
}

public class Car
{
    public IMovable MoveBehavior { get; set; }
    public int Passengers { get; set; }
    public string Model { get; set; }

    public Car(int passengers, string model, IMovable movable)
    {
        Passengers = passengers;
        Model = model;
        MoveBehavior = movable;
    }

    public void MakeMove()
    {
        MoveBehavior.Move();
    }
}