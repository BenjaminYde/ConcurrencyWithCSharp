using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _13TaskContinuation
    {
        private static void Main()
        {
            //CreateSimpleTaskChain1();
            //CreateSimpleTaskChain2();
            CreateTaskChainWithOptions();
        }

        private static void CreateSimpleTaskChain1()
        {
            var task = CreateComputeTask1().ContinueWith((t1) => Console.WriteLine($"Result is {t1.Result}"));
            task.Wait();
        }
        
        private static void CreateSimpleTaskChain2()
        {
            var task = CreateComputeTask1().ContinueWith((t1) => CreateComputeTask2(t1.Result)).Unwrap();
            task.Wait();

            // result is 3
            Console.WriteLine($"Result is {task.Result}");
        }
        
        private static void CreateTaskChainWithOptions()
        {
            var task = CreateComputeTask1()
                .ContinueWith(
                    (t1) => Console.WriteLine($"Result is {t1.Result}"), 
                    TaskContinuationOptions.OnlyOnCanceled);
            task.Wait();
        }

        private static async Task<int> CreateComputeTask1()
        {
            Console.WriteLine("Started task 1...");
            await Task.Delay(1000);
            Console.WriteLine("Finished task 1!");
            return 1;
        }
        
        private static async Task<int> CreateComputeTask2(int toAdd)
        {
            Console.WriteLine("Started task 2...");
            await Task.Delay(1000);
            Console.WriteLine("Finished task 2!");
            return 2 + toAdd;
        }
    }
}