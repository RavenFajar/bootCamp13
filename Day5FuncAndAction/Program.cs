using System;

namespace HelloWorld
{
    class Program
    {
        delegate void D();

        // Define a method to match the signature
        static void Main(string[] args)
        {
            void Method1() { }

            // Create a D1 delegate and assign it to a D2 delegate
            D d1 = Method1;
            // D d2 = Method1;
            D d2 = Method1;
            Console.WriteLine(d1 == d2);
            Console.WriteLine($"{d2.GetHashCode()}");
            Console.WriteLine($"{d1.GetHashCode()}");
            D d3 = Method1;
            Console.WriteLine($"{d3.GetHashCode()}");
            d3 += Method1;
            Console.WriteLine($"{d3.GetHashCode()}");


            d2();
            Action<string> printMessage = message => Console.WriteLine(message);
            Console.WriteLine("Hello World!");
            printMessage("Hello, world!");

            Func<int, int, int> add = (x, y) => x + y;
            Console.WriteLine(add(2, 3));  // Outputs: 5
            Func<int, int, int> divide = (x, y) => x / y;
            Console.WriteLine(divide(10, 2));


            Func<int, int, int, int> sum = (x, y, z) => x + y + z;
            Console.WriteLine(sum(10, 20, 30));
            Action<string> jiwa = pesan => Console.WriteLine($"{pesan}");

            jiwa("makan");

        }
    }
}