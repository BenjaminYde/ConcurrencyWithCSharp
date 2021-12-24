using System;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _02ParallelInvoke
    {
        private static void Main()
        {
            Parallel.Invoke(() => Console.WriteLine("[Invoke 1] Action 1"));
            
            Parallel.Invoke(
                () => Console.WriteLine("[Invoke 2] Action 1"),
                () => Console.WriteLine("[Invoke 2] Action 2"),
                () => Console.WriteLine("[Invoke 2] Action 3"));
        }
    }
}