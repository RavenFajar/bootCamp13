using System.Runtime.CompilerServices;

string testingName = "Raven";
var testing = testingName.GetEnumerator();
Console.WriteLine($"{testing}");

using (testing)
{
    while (testing.MoveNext())
    {
        var element = testing.Current;
        Console.WriteLine(element);
    }

}

List<int> lists = [1, 2, 3, 45, 5, 6, 7, 8, 9, 10];
foreach (var item in lists)
{
    Console.WriteLine(item);

}

IEnumerable<string> Foo(bool breakEarly)
{
    yield return "One";
    yield return "Two";
    if (breakEarly)
        yield break;  // Exit early
    yield return "Three";
}

foreach (string s in Foo(true))
{
    Console.WriteLine(s);  // Output: One, Two
    Console.ReadKey();
}

int? i = null;
Console.WriteLine(i);
i = 5;
Console.WriteLine(i);

IEnumerable<int> Fibs(int fibCount)
{
    int prevFib = 0, curFib = 1;
    for (int i = 0; i < fibCount; i++)
    {
        yield return prevFib;
        int newFib = prevFib + curFib;
        prevFib = curFib;
        curFib = newFib;
    }
}

foreach (int fib in Fibs(6))
{
    Console.Write(fib + " ");
}