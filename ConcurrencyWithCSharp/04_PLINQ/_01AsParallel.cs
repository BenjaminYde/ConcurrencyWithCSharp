using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ConcurrencyWithCSharp._04_PLINQ
{
    public class _01AsParallel
    {
        public static void Main()
        {
            BasicWork_Sequential();
            BasicWork_AsParallel();

            HardWork_Sequential();
            HardWork_Parallel();
        }

        private static void BasicWork_Sequential()
        {
            var sw = new Stopwatch();
            sw.Start();

            var items = Enumerable.Range(1, 100000);
            
            var evenItems = items
                .Where(i => i % 2 == 0)
                .ToList();
            
            sw.Stop();

            Console.WriteLine($"[BasicWork_Sequential] EvenItemCount is {evenItems.Count},  Total time = {sw.Elapsed.TotalMilliseconds}");
        }

        private static void BasicWork_AsParallel()
        {
            var sw = new Stopwatch();
            sw.Start();

            var items = Enumerable.Range(1, 100000);

            var evenItems = items
                .AsParallel()
                .Where(i => i % 2 == 0)
                .ToList();
            
            sw.Stop();

            Console.WriteLine($"[BasicWork_AsParallel] EvenItemCount is {evenItems.Count},  Total time = {sw.Elapsed.TotalMilliseconds}");
        }

        private static void HardWork_Sequential()
        {
            var sw = new Stopwatch();
            sw.Start();

            var items = Enumerable.Range(1, 100);

            var rnd = new Random();
            var evenItems = items
                .Where(i =>
                {
                    int timeToSleep = rnd.Next(25, 50);
                    Thread.Sleep(timeToSleep);

                    return i % 2 == 0;
                })
                .ToList();

            sw.Stop();

            Console.WriteLine($"[HardWork_Sequential] EvenItemCount is {evenItems.Count},  Total time = {sw.Elapsed.TotalMilliseconds}"); 
        }
        
        private static void HardWork_Parallel()
        {
            var sw = new Stopwatch();
            sw.Start();

            var items = Enumerable.Range(1, 100);

            var rnd = new Random();
            var evenItems = items
                .AsParallel()
                .Where(i =>
                {
                    int timeToSleep = rnd.Next(25, 50);
                    Thread.Sleep(timeToSleep);

                    return i % 2 == 0;
                })
                .ToList();

            sw.Stop();

            Console.WriteLine($"[HardWork_AsParallel] EvenItemCount is {evenItems.Count}, Total time = {sw.Elapsed.TotalMilliseconds}"); 
        }
    }
}