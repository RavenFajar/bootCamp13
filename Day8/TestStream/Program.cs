using System;
using System.IO;

namespace FileHandlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\MyTextFile1.txt";
            int a, b, result;
            a = 15;
            b = 20;
            result = a + b;

            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write("Sum of {0} + {1} = {2}", a, b, result);
            }
            Console.WriteLine("Saved");
            Console.ReadKey();
        }
    }
}
