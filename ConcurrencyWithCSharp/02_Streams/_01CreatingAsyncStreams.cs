using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._02_Streams
{
    public class _01CreatingAsyncStreams
    {
        private static void Main()
        {
            /*var task1 = CreateTask1();
            task1.Wait();*/
            
            var task2 = CreateTask2();
            task2.Wait();
        }

        private static async Task CreateTask1()
        {
            var values = GetValuesAsync();
            await foreach (var value in values)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine("Completed!");
        }
        
        private static async Task CreateTask2()
        {
            var enumerator = GetValuesAsync().GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                var value = enumerator.Current;
                Console.WriteLine(value);
            }
            Console.WriteLine("Completed!");
        }

        private static async IAsyncEnumerable<int> GetValuesAsync()
        {
            await Task.Delay(500);
            yield return 1;
            await Task.Delay(500);
            yield return 2;
        }
    }
}