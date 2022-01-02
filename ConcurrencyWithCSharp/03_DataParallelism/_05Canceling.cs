using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _05Canceling
    {
        private static void Main()
        {
            Console.WriteLine("Started");

            //RunWithBreak();
            //RunWithStop();
            RunWithCancelToken();
            
            Console.WriteLine("Completed");
        }

        private static void RunWithBreak()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(1, 1000);
            
            Parallel.ForEach(numbers, (i, state) =>
            {
                // wait random
                int delay = rnd.Next(1, 1001);
                Thread.Sleep(delay);
                
                // check if need to execute lower iterations
                if (state.ShouldExitCurrentIteration)
                {
                    if (state.LowestBreakIteration < i)
                        return;
                }
                
                // create message
                string number = i.ToString().PadLeft(3, '0');
                string message = $"{number}, task: {Task.CurrentId.ToString().PadLeft(3, '0')}";
                
                // check if need to break;
                if (i == 10)
                {
                    message += " Breaked here!";
                    state.Break();
                }
                
                Console.WriteLine(message);
            });
        }
        
        private static void RunWithStop()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(1, 1000);
            
            Parallel.ForEach(numbers, (i, state) =>
            {
                // wait random
                int delay = rnd.Next(1, 1001);
                Thread.Sleep(delay);
                
                // check if stopped
                if (state.IsStopped) 
                    return;

                // create message
                string number = i.ToString().PadLeft(3, '0');
                string message = $"{number}, task: {Task.CurrentId.ToString().PadLeft(3, '0')}";
                
                // check if need to stop
                if (i == 10)
                {
                    message += " Stopped here!";
                    state.Stop();
                }
                
                Console.WriteLine(message);
            });
        }
        
        private static void RunWithCancelToken()
        {
            // create token
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            
            // create task to cancel token
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(2000);
                cancellationTokenSource.Cancel();
                Console.WriteLine("Token has been cancelled");
            });
            
            // create loop options
            var loopOptions = new ParallelOptions()
            {
                CancellationToken = token,
            };
            
            // loop
            try
            {
                Parallel.For(0, 500, loopOptions, index =>
                {
                    Thread.Sleep(250);
                    Console.WriteLine($"Index {index}");
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation exception caught!");
            }
        }
    }
}