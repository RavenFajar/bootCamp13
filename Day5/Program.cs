// ﻿//Delegate Return
// public delegate int MyDelegate(int a, int b);
// class Program {
//     static void Main()
//     {
//         Calculator calc = new();
//         MyDelegate del = calc.Add;
//         del += calc.Sub;
//         del += calc.Mul;

//         int result = del.Invoke(5, 3);
//         Console.WriteLine(result);
//         del -= calc.Mul;
//         int result2 = del.Invoke(5, 3);
//         Console.WriteLine(result2);
//         del -= calc.Sub;

//         int result3 = del.Invoke(5, 3);
//         Console.WriteLine(result3);

//     }
// }
// class Calculator {
// 	public int Add(int a, int b) {
// 		return  a + b;
// 	}
// 	public int Sub(int a, int b) {
// 		return a - b;
// 	}
// 	public int Mul(int a, int b) {
// 		return a * b;
// 	}
// }
// try
// {
// 	int? x = null;
// 	int y = (int)x;
// }
// catch (Exception)
// {
// 	Console.WriteLine($"TErdapat error");
// 	Console.WriteLine("Lanjut");
// }


//Delegate Invocation List
// public delegate int MyDelegate(int a, int b);
// class Program {
// 	static void Main() {
// 		Calculator calc = new();
// 		MyDelegate del = calc.Add;
// 		del += calc.Sub;
// 		del += calc.Mul;

// 		int[] result = new int[3];

// 		Delegate[] invocationList = del.GetInvocationList();

// 		int count = 0;
// 		foreach(MyDelegate method in invocationList) {
// 			result[count] = method.Invoke(5,3);
// 			count++;
// 		}
// 		foreach(var i in result) 
//         {
//             Console.WriteLine(i);
//         }
// 	}
// }
// class Calculator {
// 	public int Add(int a, int b) {
// 		return  a + b;
// 	}
// 	public int Sub(int a, int b) {
// 		return a - b;
// 	}
// 	public int Mul(int a, int b) {
// 		return a * b;
// 	}
// }
// Together together = new Together();

// ProgressReporter reporter = together.WriteProgressToConsole;
// reporter += together.WriteProgressToFile;

// // Invoke the delegate
// reporter(50);  // Invokes both WriteProgressToConsole and WriteProgressToFile
// reporter -= together.WriteProgressToFile;
// reporter(20);

// delegate void ProgressReporter(int percentComplete);
// public class Together
// {

// 	// Define two methods
// 	public void WriteProgressToConsole(int percentComplete)
// 	{
// 		Console.WriteLine(percentComplete);
// 	}

// 	public void WriteProgressToFile(int percentComplete)
// 	{
// 		Console.WriteLine($"Selesai : {percentComplete}");
// 	}
// }


// A method that accepts an object

class Program
{
	delegate void StringAction(string s);
	// delegate void ActOnObject(object o);
	static void Main()
	{
		// Top-level statements must precede namespace and type declarations.
		void ActOnObject(object o) => Console.WriteLine(o);
		// void StringAction(string s) => Console.WriteLine(s);


		StringAction sa = new StringAction(ActOnObject);
		// ActOnObject ob = new ActOnObject(StringAction);

		// // We can pass a string, as string is an object
		sa("hello");

		int? z =null;
		// if (z.HasValue)
		// { 
		// 	Console.WriteLine($"isi z : {z}");
		// }
		Console.WriteLine($"{z}");
		z = 5;
		Console.WriteLine($"{z}");
		
			
		
		int? x = null;
		int? y = 5;
		if (x.HasValue)
		{
			y = (int)x;
		}
		else
		{
			Console.WriteLine($"x adalah null");

		}
		x = 10;
		Console.WriteLine($"{x}");
		Console.WriteLine($"{y}");

		// value.This is similar to how NULL values are handled in SQL.
		bool? n = null;
		bool? f = false;
		bool? t = true;

		Console.WriteLine(n | n);  // Output: null
		Console.WriteLine(n | f);  // Output: null
		Console.WriteLine(n | t);  // Output: true
		Console.WriteLine(n & f);  // Output: false
		Console.WriteLine(n & t);  // Output: null
		Console.WriteLine(t & t);
		Console.WriteLine(t & f);
		Console.WriteLine(f & f);




	}
}


