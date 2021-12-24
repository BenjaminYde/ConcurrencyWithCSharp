using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _09AsyncReportProgress
    {
        private static void Main()
        {
            var task = CreateCoffee();
            task.Wait();
        }
        
        private static async Task CreateCoffee()
        {
            Console.WriteLine($"Started creating coffee!");

            var progress = new Progress<float>();
            progress.ProgressChanged += (sender, f) => 
            {
                Console.WriteLine($"Creating coffee... {f}%");
            };
            
            await MyMethodAsync(progress);
            
            Console.Write("Completed!");
        }
        
        private static async Task MyMethodAsync(IProgress<float> progress)
        {
            var rnd = new Random();
            float percentageCompleted = 0;
            
            while (true)
            {
                // increase percentage
                float percentageToIncrease = rnd.Next(0, 15);
                percentageCompleted += percentageToIncrease;
                percentageCompleted = Math.Clamp(percentageCompleted, 0, 100);
                
                // work random time
                int msToWait = rnd.Next(100, 250);
                await Task.Delay(msToWait);
                
                // report
                progress?.Report(percentageCompleted);

                // check if need to stop
                if (percentageCompleted >= 100)
                    return;
            }
        }
    }
}