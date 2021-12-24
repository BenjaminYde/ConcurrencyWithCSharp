using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _02Async
    {
        private static void Main()
        {
            var task = MakeBreakfast();
            task.Wait();
        }

        private static async Task MakeBreakfast()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            await CreateCoffee();
            await CreateToast();
            
            sw.Stop();
            
            Console.WriteLine($"Total time = {sw.Elapsed.Seconds}");
        }

        private static async Task CreateCoffee()
        {
            Console.WriteLine("Creating coffee...");
            await Task.Delay(2000);
            Console.WriteLine("Created coffee!");
        }

        private static async Task CreateToast()
        {
            Console.WriteLine("Creating toast...");
            await Task.Delay(3000);
            Console.WriteLine("Created toast!");
        }
    }
}