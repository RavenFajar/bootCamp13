// Define a delegate that matches the event signature
using System.ComponentModel;

public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

public class Broadcaster
{
    // Declare an event using the delegate
    public event PriceChangedHandler PriceChanged;

    // A method to trigger the event
    public void ChangePrice(decimal newPrice)
    {
        decimal oldPrice = 100m; // Example old price
        PriceChanged?.Invoke(oldPrice, newPrice); // Fire the event
        Console.WriteLine($"dari void changeprice {oldPrice} ke {newPrice}");
        
    }
}

public class Subscriber
{
    public void OnPriceChanged(decimal oldPrice, decimal newPrice)
    {
        Console.WriteLine($"Price changed from {oldPrice} to {newPrice}");
    }
}

class Program
{
    static void Main()
    {
        Broadcaster broadcaster = new Broadcaster();
        Subscriber subscriber = new Subscriber();

        // Subscribe to the event
        // broadcaster.PriceChanged += subscriber.OnPriceChanged;

        // Trigger the event
        broadcaster.ChangePrice(120m);

        // Unsubscribe from the event
        broadcaster.PriceChanged -= subscriber.OnPriceChanged;

        broadcaster.ChangePrice(90m);
        

    }
}