﻿using System;
using System.Threading;

namespace ThreadingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(ThreadMethod);
            Console.WriteLine("Is t1 alive? " + t1.IsAlive);
            Console.WriteLine("Starting thread t1...");
            t1.Start();
            Console.WriteLine("Is t1 alive? " + t1.IsAlive);

            t1.Join(); // Wait for t1 to complete

            Console.WriteLine("Thread t1 has finished executing.");

            // Check if a thread is still running
            Console.WriteLine("Is t1 alive? " + t1.IsAlive);
            Console.ReadLine();
        }

        static void ThreadMethod()
        {
            Console.WriteLine("ThreadMethod started.");
            Thread.Sleep(3000);
            Console.WriteLine("ThreadMethod finished.");
        }
    }
}