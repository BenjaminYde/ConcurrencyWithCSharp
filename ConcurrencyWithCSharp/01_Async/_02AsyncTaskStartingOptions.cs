using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _02AsyncTaskStartingOptions
    {
        private static void Main()
        {
            //Example1();
            //Example2();
            //Example3();
            //Example4_1();
            //Example4_2();
            //Example4_3();
        }

        // example 1
        
        private static void Example1()
        {
            var task = CreateCoffeeTask();
            task.Wait();
        }

        private static async Task CreateCoffeeTask()
        {
            Console.WriteLine("Creating coffee...");
            await Task.Delay(1000);
            Console.WriteLine("Created coffee!");
        }
        
        // example 2

        private static void Example2()
        {
            // returns a regular Task
            var task = Task.Run(async () =>
            {
                Console.WriteLine("Creating coffee...");
                await Task.Delay(1000);
                Console.WriteLine("Created coffee!");
            });
            task.Wait();
        }
        
        // example 3
        private static void Example3()
        {
            // returns a task
            var task1 = new Task(() => Console.WriteLine("Created coffee 1!"));
            task1.Start();
            task1.Wait();
            
            // returns a task
            var task2 = Task.Factory.StartNew(() => Console.WriteLine("Created coffee 2!"));
            task2.Wait();
        }

        // example 4
        private static void Example4_1() // will not work
        {
            // returns a task<task> (task in a task)
            // because 'async ()' gets seen as 'async Task'
            var task = Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Creating coffee 1..."); // code will be executed till here and will be completed
                await Task.Delay(1000);
                Console.WriteLine("Created coffee 1!");
            });
            task.Wait();
        }        
        
        private static void Example4_2() // will work
        {
            // returns a regular task
            var task = Task.Factory.StartNew(() =>
            {
                // creation of async child task
                var taskChild = Task.Run(async () =>
                {
                    Console.WriteLine("Creating coffee 2...");
                    await Task.Delay(1000);
                    Console.WriteLine("Created coffee 2!");
                });
                taskChild.Wait();
            });
            task.Wait();
        }
        
        private static void Example4_3() // will work
        {
            // returns a task<task> (task in a task)
            // because 'async ()' gets seen as 'async Task'
            var task = Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Creating coffee 3...");
                await Task.Delay(1000);
                Console.WriteLine("Created coffee 3!");
            }).Unwrap();
            task.Wait();
        }
    }
}