using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._02_Streams
{
    public class _02Cancellation
    {
        private static void Main()
        {
            //var task1 = ExitWithSimpleBreak();
            //task1.Wait();
            
            var task1 = ExitWithToken();
            task1.Wait();
        }
        
        // example 1
        private static async Task ExitWithSimpleBreak()
        {
            var values = GetValuesAsync();
            await foreach (var value in values)
            {
                if (value == 4)
                    break;
                Console.WriteLine(value);
            }
            Console.WriteLine("Completed!");
        }
        
        private static async IAsyncEnumerable<int> GetValuesAsync()
        {
            for (int i = 1; i < 6; ++i)
            {
                yield return i;
                await Task.Delay(250);
            }
        }
        
        // example 2
        
        private static async Task ExitWithToken()
        {
            // create token
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            
            // loop over values
            var values = GetValuesAsync(token);
            try
            {
                await foreach (var value in values)
                {
                    if (value == 4)
                    {
                        cancellationTokenSource.Cancel();
                        break;
                    }
                    Console.WriteLine(value);
                }
            }
            catch
            {
            }
            
            Console.WriteLine("Completed!");
        }
        
        private static async IAsyncEnumerable<int> GetValuesAsync(CancellationToken cancellationToken = default)
        {
            for (int i = 1; i < 6; ++i)
            {
                yield return i;
                await Task.Delay(250, cancellationToken);
            }
        }
    }
}