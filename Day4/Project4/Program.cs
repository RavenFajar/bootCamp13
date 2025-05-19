// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
class NamaKelas
{
    // isi kelas
}

class Octopus
{
    public string Name;
    public static int Legs = 8;        // statis, dipakai bersama semua objek
    public readonly int Age = 5;       // hanya bisa di-set di awal
}
public class Test
{
    public const double PI = 3.14159;
}
public class MathOperations
{
    public int Add(int a, int b) => a + b;  // metode singkat
}
public class Car
{
    public string Make;
    public string Model;

    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }
}
public class Stock
{
    private decimal currentPrice;
    public decimal CurrentPrice
    {
        get { return currentPrice; }
        set { currentPrice = value; }
    }
    // public decimal CurrentPrice { get; set; }

}


