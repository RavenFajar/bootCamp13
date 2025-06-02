class Program
{
    public bool isChecked;
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");
        int x = Convert.ToInt32(Console.ReadLine());
        Program program = new Program();
        program.PrintFooBarJazzHuzz(x);
        
    }

    public void PrintFooBarJazzHuzz(int x)
    {
        for (int i = 1; i <= x; i++)
        {
            isChecked = false;
            if (i % 9 == 0)
            {
                Console.Write("huzz");
                isChecked = true;
            }
            if (i % 7 == 0)
            {
                Console.Write("jazz");
                isChecked = true;
            }
            if (i % 5 == 0)
            {
                Console.Write("bar");
                isChecked = true;
            }
            if (i % 3 == 0)
            {
                Console.Write("foo");
                isChecked = true;
            }
            if (isChecked == false)
            {
                Console.Write(i);
            }
            Console.Write(", ");

        }
    }
}
// void PrintFooBarJazzHuzz(int x)
// {
//     isChecked = true;

// }

// {   if (i % 3 == 0 && i % 5 == 0 && i % 7 == 0 && i% 9 == 0)
//     {
//         Console.WriteLine("foobarjazzhuzz");
//     }
//     else if (i % 3 == 0 && i % 5 == 0 && i % 7 == 0)
//     {
//         Console.WriteLine("foobarjazz");
//     }
//     else if (i % 3 == 0 && i % 5 == 0)
//     {
//         Console.WriteLine("foobar");
//     }
//     else if (i % 7 == 0 && i % 3 == 0)
//     {
//         Console.WriteLine("foojazz");
//     }
//     else if (i % 7 == 0 && i % 5 == 0)
//     {
//         Console.WriteLine("barjazz");
//     }
//     else if (i % 9 == 0)
//     {
//         Console.WriteLine("foo");
//     }
//     else if (i % 5 == 0)
//     {
//         Console.WriteLine("bar");
//     }
//     else if (i % 3 == 0)
//     {
//         Console.WriteLine("huzz");
//     }
//     else
//     {
//         Console.WriteLine(i);
//     }
// }