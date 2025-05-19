﻿using System.Numerics;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Calculator<int> calc = new();
Console.WriteLine(calc.Add(3, 4));
Calculator<float> calc2 = new();
Console.WriteLine(calc2.Add(3.1f, 2.0f));
Calculator<decimal> calc3 = new();
calc3.Add(3.0M, 2.0M);
Calculator<double> calc4 = new();
calc4.Add(3.0, 2.0);

class Calculator<T> where T :  IAdditionOperators<T,T,T>
{
    public T Add(T a, T b)
    {
        return a + b;
    }

}