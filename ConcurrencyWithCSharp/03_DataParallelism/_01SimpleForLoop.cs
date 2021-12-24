using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp._03_DataParallelism
{
    public class _01SimpleForLoop
    {
        private static void Main()
        {
            SequentialLoop();
            ParallelLoop();
        }

        private class Book
        {
            public string Name;
            public int Pages;
            public string Author;
        }

        private static void Trade(Book book)
        {
            var task = Task.Run(() => { Task.Delay(100);});
            task.Wait();
        }

        private static void SequentialLoop()
        {
            int bookCount = 1000000;
            Book[] books = new Book[bookCount];

            var sw = new Stopwatch();
            sw.Start();
            
            foreach (var book in books)
            {
                Trade(book);
            }
            
            sw.Stop();
            Console.WriteLine($"[Sequential] Total time = {sw.Elapsed.TotalMilliseconds}");
        }
        
        private static void ParallelLoop()
        {
            int bookCount = 1000000;
            Book[] books = new Book[bookCount];

            var sw = new Stopwatch();
            sw.Start();
            
            Parallel.ForEach(books, book => Trade(book));
            
            sw.Stop();
            Console.WriteLine($"[Parallel] Total time = {sw.Elapsed.TotalMilliseconds}");
        }
    }
}