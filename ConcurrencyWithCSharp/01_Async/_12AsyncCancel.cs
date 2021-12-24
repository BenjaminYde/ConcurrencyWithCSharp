using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _12AsyncCancel
    {
        private static void Main()
        {
            //CreateTasksWithTokens();
            CancelTask();
        }

        private static void CreateTasksWithTokens()
        {
            // create token
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            
            // create tasks
            var task1 = new Task<int>(() => 1, token);
            task1.Start();

            var task2 = Task.Factory.StartNew<int>(() => 2, token);

            var task3 = Task.Run<int>(() => 3, token);
            
            Console.WriteLine("Completed!");   
        }

        private static void CancelTask()
        {
            // create token
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            token.Register(() => Console.WriteLine("Executed cancel!"));
            
            // create main task
            var task = Task.Run(async () =>
            {
                Console.WriteLine("Task started!");
                for (int i = 0; i < 1000; i++)
                {
                    await Task.Delay(2);

                    if (token.IsCancellationRequested)
                        return;
                }
                Console.WriteLine("Task finished!");
            }, token);
            
            // cancel
            cancellationTokenSource.CancelAfter(500);

            task.Wait();
            
            if(token.IsCancellationRequested)
                Console.WriteLine("Task is canceled!");
        }
    }
}