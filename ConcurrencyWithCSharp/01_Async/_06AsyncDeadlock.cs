using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _06AsyncDeadlock
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
            // get html text
            Console.WriteLine("Getting html text...");
            var text = GetHTMLTextAsync().Result; // the program will freeze here until it got the result!!
            Console.WriteLine($"Got text! Text is {text}");

            // get numbers
            Console.WriteLine("Getting numbers...");
            await LoadDataAsync();
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
                await Task.Delay(500);
                Console.WriteLine(i);
            }
        }
    }
}