using System.Data;
using System.Security.Claims;

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");
        int x = Convert.ToInt32(Console.ReadLine());
        MyClass program = new MyClass();
        program.PrintFooBarJazzHuzz(x);

    }
}
class MyClass
{
    public bool isChecked;
    public Dictionary<int, string> rules = new Dictionary<int, string>();
    public MyClass()
    {
        AddRules(3, "foo");
        AddRules(5, "bar");
        AddRules(7, "jazz");
        AddRules(9, "huzz");
    }

    public void AddRules(int number, string rule)
    {
        rules.Add(number, rule);
    }

    public void PrintFooBarJazzHuzz(int x)
    {
        for (int i = 1; i <= x; i++)
        {   
            isChecked = false;
            foreach (var rule in rules)
            {
                if (i % rule.Key == 0)
                {
                    Console.Write(rule.Value);
                    isChecked = true;
                }
            }
            if (isChecked == false)
            {
                Console.Write(i);
            }
            Console.Write(", ");

        }
    }
}