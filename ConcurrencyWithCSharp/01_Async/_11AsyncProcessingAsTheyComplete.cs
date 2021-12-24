using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _11AsyncProcessingAsTheyComplete
    {
        private static void Main()
        { 
            // start method
            var task = UseOrderByCompletionAsync();
            task.Wait();
            
            Console.WriteLine("Completed!");
        }
        
        private static async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }
        
        private static async Task UseOrderByCompletionAsync()
        {
            // create a sequence of tasks.
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            
            Task<int>[] tasks = new[] { taskA, taskB, taskC };

            // add continue with task
            foreach (var task in tasks)
            {
                task.ContinueWith((_) =>
                {
                    Console.WriteLine($"Completed with result {task.Result}");
                }, TaskContinuationOptions.ExecuteSynchronously);
                
                task.ContinueWith((_) =>
                {
                    Console.WriteLine($"Completed again with result {task.Result}");
                }, TaskContinuationOptions.ExecuteSynchronously);
            }
            
            // await all processing to complete
            await Task.WhenAll(tasks);
        }
    }
}