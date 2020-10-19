using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using dotnetScrape.Core.Bots;
using dotnetScrape.Core.Browser;
using dotnetScrape.Examples.Yahoo.Tasks;

namespace dotnetScrape.Examples.Yahoo
{
    class Program
    {
        private const string URL = "https://finance.yahoo.com/screener/predefined/day_gainers?offset=0&count=10";
        private const int CONCURRENCY = 1;

        static async Task Main(string[] args)
        {
            using (var bot = new ScrapingBot(new BotConfig(CONCURRENCY)))
            {
                var watch = new Stopwatch();
                watch.Start();
                await bot.NavigateAsync(URL);
                bot.Do(b => System.Console.WriteLine("hello"));
                System.Console.WriteLine($"{watch.ElapsedMilliseconds}ms to navigate");
                watch.Restart();

                var results = bot.RunTasks(new YahooTaskFactory());
                System.Console.WriteLine($"{watch.ElapsedMilliseconds}ms to run all tasks");

                foreach (var r in results)
                    await File.AppendAllTextAsync("results.txt", $"{r.FullName}: {r.Price}$ [{r.Change}]\n");

                System.Console.WriteLine("Done");
            }
        }
    }
}
