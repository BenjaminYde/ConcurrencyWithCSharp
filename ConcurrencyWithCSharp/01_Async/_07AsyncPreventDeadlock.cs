using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _07AsyncPreventDeadlock
    {
        private static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var task = WorkAsync();
            task.Wait();

            sw.Stop();
            
            Console.WriteLine($"Total time = {sw.Elapsed.TotalMilliseconds} ms");
            
        }
        
        private static async Task WorkAsync()
        {
            // get numbers
            Console.WriteLine("Getting numbers...");
            var taskLoadData = LoadDataAsync();
            
            // get html text
            Console.WriteLine("Getting html text...");
            var taskGetText = GetHTMLTextAsync();
            
            // wait
            // the program won't freeze
            await taskGetText;
            await taskLoadData;
        }

        private static async Task<string> GetHTMLTextAsync()
        {
            // create http client
            using (var client = new HttpClient())
            {
                // get string from url
                Task<string> getStringTask = client.GetStringAsync("https://example.com");
                
                // wait
                await Task.Delay(3000);
                
                return "<div>This is my cool html!</div>";
            }
        }
        
        private static async Task LoadDataAsync()
        {
            for (int i = 0; i < 5; ++i)
            {
                await Task.Delay(5);
                Console.WriteLine(i);
            }
        }
    }
}