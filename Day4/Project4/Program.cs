// Define the delegate type
delegate int Transformer(int x);

// A method that matches the delegate signature
int Square(int x) => x * x;
int Rectangle(int x) => x * x * x;

// Assign the method to a delegate variable
Transformer t = Square;
Transformer t = Rectangle;


// Invoke the delegate
int result = t(3);  // result will be 9

Console.WriteLine(result);  // Outputs: 9
Console.WriteLine(result);
