using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Start the asynchronous method
        await DisplayPrimeCountsAsync();
    }

    static async Task DisplayPrimeCountsAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            var primeCount = await GetPrimesCountAsync(i * 1000000 + 2, 1000000);
            Console.WriteLine($"{primeCount} primes between {i * 1000000} and {(i + 1) * 1000000 - 1}");
        }
        Console.WriteLine("Done!");
    }

    static async Task<int> GetPrimesCountAsync(int start, int count)
    {
        return await Task.Run(() =>
        {
            return Enumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)
            );
        });
    }
}