using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _10AsyncWaitingOnTasks
    {
        public static void Main()
        {
            var normalWaitTask = Task.Factory.StartNew(() => Console.WriteLine("Started new task..."));
            normalWaitTask.Wait();

            Console.WriteLine("Completed normal wait task!");

            WhenAnyDemo();
            WhenAllDemo();
            WaitAnyDemo();
            WaitAllDemo();
        }
        
        private static void WhenAnyDemo()
        {
            Console.WriteLine("WhenAny Started!");
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("    TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("    TaskB finished"));
            Task.WhenAny(taskA, taskB);
            Console.WriteLine("WhenAny Finished!");
        }

        private static void WhenAllDemo()
        {
            Console.WriteLine("WhenAll Started!");
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("    TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("    TaskB finished"));
            Task.WhenAll(taskA, taskB);
            Console.WriteLine("WhenAll Finished!");
        }

        private static void WaitAnyDemo()
        {
            Console.WriteLine("WaitAny Started!");
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("    TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("    TaskB finished"));
            Task.WaitAny(taskA, taskB);
            Console.WriteLine("WaitAny Finished!");
        }

        private static void WaitAllDemo()
        {
            Console.WriteLine("WaitAll Started!");
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("    TaskA finished"));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("    TaskB finished"));
            Task.WaitAll(taskA, taskB);
            Console.WriteLine("WaitAll Finished!");
        }
    }
}