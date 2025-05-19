object obj = 9;  // Boxing
Console.WriteLine(obj.GetType().FullName);
Console.WriteLine(obj);

Stack stack = new Stack();
stack.Push("sausage");  // Push a string
stack.Push(10);  // Push a string
stack.Push('A');  // Push a string
Console.WriteLine(stack.GetType().FullName);
Console.WriteLine(stack);
Console.WriteLine(stack.data.GetType().FullName);

Point newPoint = new Point();
newPoint.x = 1;
Console.WriteLine(newPoint.x);
newPoint.y = newPoint.x;
Console.WriteLine(newPoint.x);
Console.WriteLine(newPoint.y);
Console.WriteLine("barier");
newPoint.y = 5;
Console.WriteLine(newPoint.x);
Console.WriteLine(newPoint.y);
Console.WriteLine("barier");
Point p1 = new Point();
Console.WriteLine(p1.x);
Console.WriteLine(p1.y);
Console.WriteLine("barier");
Point p2 = default;
Console.WriteLine(p2.x);
Console.WriteLine(p2.y);
Console.WriteLine("barier");
Car carOne = new Car();
Console.WriteLine(carOne.model);

newPoint.afui = "Sip";
newPoint.af = 'A';

// Console.WriteLine(newPoint.x);
if (newPoint.afui == null)
{ 
    System.Console.WriteLine("afui adalah null");
}
if (newPoint.af == '\0'){ 
    System.Console.WriteLine("af adalah '\0'");
}
Console.WriteLine(newPoint.afui);
Console.WriteLine(newPoint.af);




// string su = (string)stack.Pop();  // Pop the string and cast it back
// Console.WriteLine(su);


BorderSides leftRight = BorderSides.Left | BorderSides.None;

Console.WriteLine(leftRight);


if ((leftRight | BorderSides.Right) != BorderSides.Left)
    Console.WriteLine("Includes Left");

string formatted = leftRight.ToString();

BorderSides sides = BorderSides.Left;
sides |= BorderSides.Right;  // Combines Left and Right
Console.WriteLine(sides == leftRight); // True

sides ^= BorderSides.Right;  // Toggles Right (removes Right)
Console.WriteLine(sides);

Asset a = new Stock { Name = "MSFT", SharesOwned = 1000 };
Stock ? s = a as Stock;  // s is null, no exception thrown

if (s != null)
{
    Console.WriteLine(s.SharesOwned);
}
else
{
    Console.WriteLine("The cast failed.");
}

struct Point
{
    public Point() => z = 1;
    public int x = 1, y = 2, z;
    public string afui;
    public char af;   
}

[Flags]
public enum BorderSides
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8
}

class Car
{
   protected string model = "Mustang";
}

class SportsCar : Car
{
    public string Model()
    {
        string carModel = model;
        return carModel;
    }
}

public class Stack
{
    int position;
    public object[] data = new object[10];
    
    
    public void Push(object obj) 
    {
        data[position++] = obj;
    }

    public object Pop() 
    {
        return data[--position];
    }
}

// public static class MathUtilities
// {
//     public static double Add(double a, double b) => a + b;
// }

// public static class MathAja : MathUtilities
// {
//     // public static double Add(double a, double b) => a + b;
// }


public class Asset
{
    public string? Name;
}
public class Stock : Asset
{
    public long SharesOwned;
}

public class House : Asset
{
    public decimal Mortgage;
}
