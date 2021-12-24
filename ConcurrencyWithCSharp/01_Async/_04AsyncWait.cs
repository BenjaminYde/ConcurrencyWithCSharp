using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _04AsyncWait
    {
        private static void Main()
        { 
            // start method
            var task = CreateToast();
            
            // synchronously block, waiting for the async method to complete
            task.Wait();
            
            Console.WriteLine("Completed!");
        }

        private static async Task CreateToast()
        {
            Console.WriteLine("Creating toast...");
            await Task.Delay(3000);
            Console.WriteLine("Created toast!");
        }
    }
}