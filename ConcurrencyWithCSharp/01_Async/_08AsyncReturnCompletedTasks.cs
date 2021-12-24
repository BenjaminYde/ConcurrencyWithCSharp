using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _08AsyncReturnCompletedTasks
    {
        private static void Main()
        {
            var myClass = new MySynchronousImplementation();
            myClass.DoSomethingAsync1();
            int i = myClass.DoSomethingAsync2().Result;
        }

        private interface IMyAsyncInterface
        {
            Task DoSomethingAsync1();
            Task<int> DoSomethingAsync2();
            Task DoSomethingAsyncFailed();
            Task<int> DoSomethingAsyncFailed(CancellationToken cancellationToken);
        }
        
        private class MySynchronousImplementation : IMyAsyncInterface
        {
            public Task DoSomethingAsync1()
            {
                Console.WriteLine("Executed DoSomethingAsync1");
                return Task.CompletedTask; // set completed
            }

            public Task<int> DoSomethingAsync2()
            {
                Console.WriteLine("Executed DoSomethingAsync2");
                return Task.FromResult(253); // returns T Value
            }

            public Task DoSomethingAsyncFailed()
            {
                return Task.FromException(new NotImplementedException());
            }

            public Task<int> DoSomethingAsyncFailed(CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                    return Task.FromCanceled<int>(cancellationToken);
                return Task.FromResult(-1);
            }
        }
    }
}