using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> printMessage = message => Console.WriteLine(message);
            Console.WriteLine("Hello World!");
            printMessage("Hello, world!");

            Func<int, int, int> add = (x, y) => x + y;
            Console.WriteLine(add(2, 3));  // Outputs: 5

            Func<int, int, int, int> sum = (x, y, z) => x + y + z;
            Console.WriteLine(sum(10,20,30));   
        }
    }
}