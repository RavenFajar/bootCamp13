using HouseProperty;
using AnimalType;
using System.Drawing;
using Classified;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

House house1 = new House();
house1.Add2Door();
Console.WriteLine(house1.totalDoor);
Console.WriteLine(house1.totalWindow);

Dog husky = new Dog();
husky.eat();
husky.move();
husky.bark();

Stack stack = new Stack();
stack.Push("sausage");
stack.Push("bread");
stack.Push(5);  // Push a string
Console.WriteLine(stack.GetType());


int u = (int)stack.Pop();
Console.WriteLine(u);
Console.WriteLine(u.GetType());

string s = (string)stack.Pop();
Console.WriteLine(s);
Console.WriteLine(s.GetType());

MyClass myClass = new MyClass();
// myClass.PrivateMethod();

// int x = "1";
// object y = "5";
// int z = (int)y; 

Point p = new Point();
Console.WriteLine(p.GetType().FullName);  // Output: Point
Console.WriteLine(typeof(Point).Name); 

Car mercy = new Car("one");

Console.WriteLine(mercy.Make);
Console.WriteLine(mercy.Model);

BorderSide myVar = BorderSide.Left;
BorderSide myVar1 = BorderSide.Right;
BorderSide myVar2 = BorderSide.Top;
BorderSide myVar3 = BorderSide.Bottom;

Console.WriteLine((int)myVar);
Console.WriteLine((int)myVar1);
Console.WriteLine((int)myVar2);
Console.WriteLine((int)myVar3);

BorderSide leftRight = BorderSide.None | BorderSide.Right;
Console.WriteLine(leftRight);

if ((leftRight & BorderSide.Left) != 0)
    Console.WriteLine("Includes Left");

string formatted = leftRight.ToString();



public class Stack{
    int position;
    object[] data = new object[10];
    public void Push(object obj) 
    {
        data[position++] = obj;
    }

    public object Pop() 
    {
        return data[--position];
    }

}

public class Car
{
    public string Make;
    public string Model;

    public Car(string make) : this(make, "Unknown")
    {
    }

    public Car(string make, string model)
    {
        Make = make;
        Model = model;
    }
    public decimal Price { get; set; }
}
[Flags]
public enum BorderSide : byte
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8
    // Left,
    // Right,
    // Top,
    // Bottom
}

