using System.Diagnostics;
using Cache.Main;

class Program
{
    public static void Main(string[] args)
    {
        var cacheStore = new CacheStore();
        Stopwatch stopwatch = Stopwatch.StartNew();
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(1000);
        List<int> exampleList = [1, 2, 3, 4, 5, 6];
        
        cacheStore.Add("One", 1);
        cacheStore.Add("Two", exampleList);
        cacheStore.Add("Three", "String1");
        cacheStore.Add("Four", 'c');
        cacheStore.Add("Zive", "String");
        cacheStore.Remove("Three");
        cacheStore.Add("Six", 105.5);
        cacheStore.Add("Seven", stopwatch);
        cacheStore.Add("Eight", timeSpan);
    }
}


