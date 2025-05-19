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
try
{
	int? x = null;
	int y = (int)x;
}
catch (Exception)
{
	Console.WriteLine($"TErdapat error");
	Console.WriteLine("Lanjut");
}


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