using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _6ThreadStorage
    {
        private static void Main()
        {
            //RunWithoutThreadLocalVariable();
            //RunThreadLocalVariableFor();
            RunThreadLocalVariableForEach();
            //RunThreadLocalVariableForEach_WithPartitioner();
        }

        private static void RunWithoutThreadLocalVariable()
        {
            int totalSum = 0;

            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4};

            Parallel.For(0,20, // from & to
                options, // parallel options
                (i, state) => // (int, state) 
                {
                    totalSum += i;
                    Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {totalSum}" );
                }
            );
            
            Console.WriteLine($"The total of 20 numbers is {totalSum}" );
        }
        
        private static void RunThreadLocalVariableFor()
        {
            int totalSum = 0;

            Action<int> onSingleTaskFinished = (taskResult) => 
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult}" );
                totalSum += taskResult;
            };

            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4};

            Parallel.For(0,20, // from & to,
                options, // parallel options
                () => 0, // method to initialize the local variable
                (i, state, subtotal) => // TLocal (int, state, TLocal) 
                {
                    subtotal += i;
                    return subtotal;
                },
                onSingleTaskFinished // executed at the end
            );
            
            Console.WriteLine($"The total of 20 numbers is {totalSum}" );
        }
        
        private static void RunThreadLocalVariableForEach()
        {
            var numbers = Enumerable.Range(1, 60).ToArray();
            int totalSum = 0;

            Action<int> onSingleTaskFinished = (taskResult) => 
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult}" );
                totalSum += taskResult;
            };

            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4};

            Parallel.ForEach(numbers, // partitioner of array of numbers
                options, // parallel options
                () => 0, // method to initialize the local variable
                (i, state, subtotal) => // TLocal (int, state, TLocal) 
                {
                    subtotal += i;
                    return subtotal;
                },
                onSingleTaskFinished // executed at the end
            );
            
            Console.WriteLine($"The total of 20 numbers is {totalSum}" );
        }
        
        private static void RunThreadLocalVariableForEach_WithPartitioner()
        {
            var numbers = Enumerable.Range(1, 60).ToArray();
            int totalSum = 0;

            Action<int> onSingleTaskFinished = (taskResult) => 
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult}" );
                totalSum += taskResult;
            };

            var options = new ParallelOptions() { MaxDegreeOfParallelism = 4};
            var rangePartitioner = Partitioner.Create(0, numbers.Length, 15);

            Parallel.ForEach(rangePartitioner, // partitioner of array of numbers
                options, // parallel options
                () => 0, // method to initialize the local variable
                (partitionerChunk, state, subtotal) => // TLocal (int, state, TLocal) 
                {
                    for (int i = partitionerChunk.Item1; i < partitionerChunk.Item2; ++i)
                    {
                        subtotal += i;
                    }
                    return subtotal;
                },
                onSingleTaskFinished // executed at the end
            );
            
            Console.WriteLine($"The total of 20 numbers is {totalSum}" );
        }
    }
}