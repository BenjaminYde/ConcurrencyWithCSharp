using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    class _01Sync
    {
        private static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            CreateCoffee();
            CreateToast();

            sw.Stop();
            
            Console.WriteLine($"Total time = {sw.Elapsed.Seconds}");
        }

        private static void CreateCoffee()
        {
            Console.WriteLine("Creating coffee...");
            Task.Delay(2000).Wait();
            Console.WriteLine("Created coffee!");
        }

        private static void CreateToast()
        {
            Console.WriteLine("Creating toast...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Created toast!");
        }
    }
}