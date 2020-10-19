using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetScrape.Core.Bots.Interfaces;
using dotnetScrape.Core.Bots.Internal;
using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;
using HtmlAgilityPack;
using Knyaz.Optimus.Dom.Elements;

namespace dotnetScrape.Core.Bots
{
    public class ScrapingBot : IScrapingBot
    {
        private List<IScrapingBrowser> Browsers;

        public ScrapingBot(BotConfig config)
        {
            Browsers = new List<IScrapingBrowser>();
            for (int i = 0; i < config.ConcurrencyLevel; i++)
                Browsers.Add(new OptimusBrowser(config.ExecuteJavascript));
        }

        public void Back()
        {
            Parallel.ForEach(Browsers, b => b.Back());
        }

        public Task BackAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Browsers.ForEach(b => b.Dispose());
        }

        public void Do(Action<IScrapingBrowser> action)
        {
            Parallel.ForEach(Browsers, b => action(b));
        }

        public async Task DoAsync(Action<IScrapingBrowser> action)
        {
            await Task.Run(() => Parallel.ForEach(Browsers, b => action(b)));
        }

        public HtmlElement FindElement(Selector selector)
        {
            return Browsers.First().FindElement(selector);
        }

        public IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            return Browsers.First().FindElements(selector);
        }

        public void Navigate(string url)
        {
            Parallel.ForEach(Browsers, b => b.Navigate(url));
        }

        public async Task NavigateAsync(string url)
        {
            await Task.Run(() => Parallel.ForEach(Browsers, b => b.Navigate(url)));
        }

        public T RunTask<T>(IScrapingTask<T> task)
        {
            return Browsers.First().ExecuteTask(task);
        }

        public T RunTask<T>(Func<IScrapingBrowser, T> task)
        {
            var taskHelper = new TaskHelper<T>(task);

            return Browsers.First().ExecuteTask(taskHelper);
        }

        public async Task<T> RunTaskAsync<T>(IScrapingTask<T> task)
        {
            return await Browsers.First().ExecuteTaskAsync(task);
        }

        public async Task<T> RunTaskAsync<T>(Func<IScrapingBrowser, T> task)
        {
            var taskHelper = new TaskHelper<T>(task);
            return await Browsers.First().ExecuteTaskAsync(taskHelper);
        }
        public IEnumerable<T> RunTasks<O, T>(IScrapingTaskFactory<O, T> factory) where O : IScrapingTask<T>
        {
            var tasks = factory.CreateTasks(Browsers.First());
            return RunTasks<O, T>(tasks: tasks);
        }

        public IEnumerable<T> RunTasks<O, T>(Func<IScrapingBrowser, IEnumerable<O>> factory) where O : IScrapingTask<T>
        {
            var tasks = factory(Browsers.First());

            return RunTasks<O, T>(tasks);
        }

        private IEnumerable<T> RunTasks<O, T>(IEnumerable<O> tasks) where O : IScrapingTask<T>
        {
            var results = new ConcurrentBag<T>();
            var queue = new ConcurrentQueue<O>(tasks);

            Parallel.ForEach(Browsers, b =>
            {
                while (queue.TryDequeue(out var task))
                    results.Add(b.ExecuteTask(task));
            });

            return results;
        }

        public async Task<IEnumerable<T>> RunTasksAsync<O, T>(IScrapingTaskFactory<O, T> factory) where O : IScrapingTask<T>
        {
            var tasks = factory.CreateTasks(Browsers.First());
            return await RunTasksAsync<O, T>(tasks);
        }

        public async Task<IEnumerable<T>> RunTasksAsync<O, T>(Func<IScrapingBrowser, IEnumerable<O>> factory) where O : IScrapingTask<T>
        {
            var tasks = factory(Browsers.First());
            return await RunTasksAsync<O, T>(tasks);
        }

        private async Task<IEnumerable<T>> RunTasksAsync<O, T>(IEnumerable<O> tasks) where O : IScrapingTask<T>
        {
            var results = new ConcurrentBag<T>();
            var queue = new ConcurrentQueue<O>(tasks);

            await Task.Run(() =>
            {
                Parallel.ForEach(Browsers, b =>
                {
                    while (queue.TryDequeue(out var task))
                        results.Add(b.ExecuteTask(task));
                });
            });

            return results;
        }

        public async Task Wait(TimeSpan waitTime)
        {
            await Task.Delay(waitTime.Milliseconds);
        }

        public async Task WaitElement(Selector selector)
        {
            await Task.Run(() =>
                Parallel.ForEach(Browsers, b => b.WaitElement(selector)));
        }
    }

}
