using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _03MoreForLoops
    {
        private static void Main()
        {
            //RunParallelForLoop();
            //RunParallelForEachLoop();
            RunParallelForLoopDegreeOfParallelism();
        }

        private static void RunParallelForLoop()
        {
            var result = Parallel.For(1, 20, index =>
            {
                Console.WriteLine($"Index {index} executing on Task Id {Task.CurrentId}");
            });
            Console.WriteLine($"IsCompleted = {result.IsCompleted}");
        }
        
        private static void RunParallelForLoopDegreeOfParallelism()
        {
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
            var result = Parallel.For(1, 20, options, index =>
            {
                Console.WriteLine($"Index {index} executing on Task Id {Task.CurrentId}");
            });
            Console.WriteLine($"IsCompleted = {result.IsCompleted}");
        }
        
        private static void RunParallelForEachLoop()
        {
            var items = Enumerable.Range(1, 20);
            var result = Parallel.ForEach(items, item =>
            {
                Console.WriteLine($"Index {item} executing on Task Id {Task.CurrentId}");
            });
            Console.WriteLine($"IsCompleted = {result.IsCompleted}");
        }
    }
}