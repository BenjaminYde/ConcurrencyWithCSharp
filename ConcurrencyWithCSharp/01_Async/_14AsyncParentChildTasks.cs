using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _14AsyncParentChildTasks
    {
        private static void Main()
        {
            //DetachedTasks();
            AttachedTasks();
        }

        private static void AttachedTasks()
        {
            // create parent task
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task started...");
                // create child task
                Task childTask = Task.Factory.StartNew(() => 
                {
                    Console.WriteLine("Child task started...");
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Parent task finished!");
            });
            
            // wait
            parentTask.Wait();
            Console.WriteLine("Completed!");
        }
        
        private static void DetachedTasks()
        {
            // create parent task
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task started...");
                // create child task
                Task childTask = Task.Factory.StartNew(() => 
                {
                    Console.WriteLine("Child task started...");
                });
                Console.WriteLine("Parent task finished!");
            });
            
            // wait
            parentTask.Wait();
            Console.WriteLine("Completed!");
        }
    }
}