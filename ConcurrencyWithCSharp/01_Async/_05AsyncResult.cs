using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _05AsyncResult
    {
        private static void Main()
        { 
            // start method
            var task = MakeBreakfast();
            
            // synchronously block, waiting for the async method to complete
            task.Wait();
            
            Console.WriteLine("Completed!");
        }

        private static async Task MakeBreakfast()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var coffeeTask = CreateCoffee();
            var toastTask =  CreateToast();

            string coffeeName = await coffeeTask;
            int toastCount = await toastTask;
            
            sw.Stop();
            
            Console.WriteLine($"I made you a {coffeeName} and {toastCount}x toast!");
            Console.WriteLine($"Total time = {sw.Elapsed.Seconds}");
        }

        private static async Task<string> CreateCoffee()
        {
            Console.WriteLine("Creating coffee...");
            await Task.Delay(2000);
            Console.WriteLine("Created coffee!");

            return "Latte Macchiato!";
        }

        private static async Task<int> CreateToast()
        {
            Console.WriteLine("Creating toast...");
            await Task.Delay(3000);
            Console.WriteLine("Created toast!");

            return 2;
        }
    }
}