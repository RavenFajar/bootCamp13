// Note n = new Note(2);        // 2 semitones from A
// double frequency = (double)n;        // Implicit conversion to frequency (double)
// Console.WriteLine(frequency);  // Output: Frequency of the note

// double freq = 554.37;        // Frequency of a note (in hertz)
// Note n2 = (Note)freq; 
// Console.WriteLine($"{n2.value}");

// public struct Note
// {
//     public int value;

//     public Note(int semitonesFromA)
//     {
//         value = semitonesFromA;
//     }

//     // Implicit conversion from Note to double (frequency in Hertz)
//     public static explicit operator double(Note x)
//     {
//         return 440 * Math.Pow(2, (double)x.value / 12);  // Convert to frequency
//     }
    
//     // Explicit conversion from double (frequency) to Note
//     public static implicit operator Note(double x)
//     {
//         int semitones = (int)(0.5 + 12 * (Math.Log(x / 440) / Math.Log(2)));  // Convert to nearest semitone
//         return new Note(semitones);
//     }
// }

Note B = new Note(2);    // Note B, 2 semitones from A
Note CSharp = B - 2;
Console.WriteLine(CSharp.value);
CSharp -= 2;
Console.WriteLine(CSharp.value);
public struct Note
{
    public int value;

    public Note(int semitonesFromA)
    {
        value = semitonesFromA;
    }

    // Overload the + operator to add an integer (semitones) to a Note
    public static Note operator -(Note x, int semitones)
    {
        return new Note(x.value + semitones);
    }
}
// //Operator Overloading
// class Program {
// 	static void Main() {
// 		Car car = new Car();
// 		Car car2 = new Car();
// 		Car result = car - car2;
// 		Console.WriteLine(result.price);
//         Car testingResult = car++;
// 		Console.WriteLine(testingResult.price);
        
// 	}	
// }
// class Car {
// 	public int price = 100;
// 	public static Car operator-(Car a, Car b){
// 		int result = a.price + b.price;
// 		Car car = new Car();
// 		car.price = result;
// 		return car;
// 	}
// 	public static Car operator++(Car a) {
// 		a.price*=5;
// 		return a;
// 	}
// }