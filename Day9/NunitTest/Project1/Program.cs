using System.Globalization;

namespace Name1
{

    public interface IFlyable
    {
        void Fly();
    }

    public class Bird
    {
        public void Eat()
        {
            Console.WriteLine("Bird is eating");
        }
    }

    public class Sparrow : Bird, IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Sparrow is flying");
        }
    }

    public class Penguin : Bird
    {
        public void Swim()
        {
            Console.WriteLine("Penguin is swimming");
        }
    }

    public class Convert
    {
        Dictionary<string, string> _dictionary = new Dictionary<string, string>()
        {
            { "satu", "1" },
            { "dua", "2" },
            { "tiga", "3" },
            { "empat", "4" },
            { "lima", "5" },
            { "enam", "6" },
            { "tujuh", "7" },
            { "delapan", "8" },
            { "sembilan", "9" },
        };
        public string ConvertToInt(string key)
        {
            string hasil = _dictionary[key];

            return hasil;
        }



        
        
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Convert convert = new Convert();
            string kata = "dua puluh satu";
            string [] katakata = kata.Split(' ');
            foreach (var item in katakata)
            {
                convert.ConvertToInt(item);
                
            }

            // string result = convert.ConvertToInt(input);
            // Console.WriteLine($"The integer value of '{input}' is: {result}");

            Bird bird = new Sparrow();
            bird.Eat();
            // bird.Fly();  // This will cause a compile-time error
            // IFlyable flyableBird = bird as IFlyable;
            // flyableBird?.Fly();  // Works fine with Sparrow

            Sparrow sparrow = new Sparrow();
            sparrow.Eat();
            sparrow.Fly();


            bird = new Penguin();
            bird.Eat();
            // No flying method is called for Penguin, so no error occurs

            List<int> numbers = new List<int>()
            {
                1, 2, 3, 4, 100
            };
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }


            List<List<int>> allNumber = new List<List<int>>()
            { new List<int>{ 1, 2, 3, 4, 5 },
                new List<int>{ 6, 7, 8, 9, 10 }
            };
            int length = allNumber.Count;

            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < allNumber[i].Count; j++)
                {
                    if (i == 0)
                    {
                        sum1 += allNumber[i][j];
                    }
                    else
                    {
                        sum2 += allNumber[i][j];
                    }
                }

            }

            int totalInside = allNumber[0][0];
            Console.WriteLine(totalInside);

            Console.WriteLine(length);
            Console.WriteLine(sum1);
            Console.WriteLine(sum2);


            Console.WriteLine(allNumber);

        }
    }
}