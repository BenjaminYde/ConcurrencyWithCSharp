using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _04Partitioning
    {
        private static void Main()
        {
            RunRangePartitioning();
            //RunChunkPartitioning();
        }

        private static void RunRangePartitioning()
        {
            var items = Enumerable.Range(0, 100).ToArray();
            var rangePartitioner = Partitioner.Create(0, items.Length, 20);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                Console.WriteLine($"StartIndex is {range.Item1} & EndIndex is {range.Item2}");
            });
        }
        
        private static void RunChunkPartitioning()
        {
            var items = Enumerable.Range(0, 100).ToArray();
            var rangePartitioner = Partitioner.Create(items, true);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                Console.WriteLine($"StartIndex is {range}");
            });
        }
    }
}