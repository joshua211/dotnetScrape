using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;
using HtmlAgilityPack;
using Knyaz.Optimus.Dom.Elements;

namespace dotnetScrape.Core.Bots.Interfaces
{
    public interface IScrapingBot : IHtmlSearchable, IDisposable
    {
        void Do(Action<IScrapingBrowser> action);
        Task DoAsync(Action<IScrapingBrowser> action);
        void Back();
        Task Wait(TimeSpan waitTime);
        Task WaitElement(Selector selector);
        Task BackAsync();
        void Navigate(string url);
        Task NavigateAsync(string url);
        T RunTask<T>(IScrapingTask<T> task);
        T RunTask<T>(Func<IScrapingBrowser, T> task);
        Task<T> RunTaskAsync<T>(IScrapingTask<T> task);
        Task<T> RunTaskAsync<T>(Func<IScrapingBrowser, T> task);
        IEnumerable<T> RunTasks<O, T>(IScrapingTaskFactory<O, T> factory) where O : IScrapingTask<T>;
        IEnumerable<T> RunTasks<O, T>(Func<IScrapingBrowser, IEnumerable<O>> factory) where O : IScrapingTask<T>;
        Task<IEnumerable<T>> RunTasksAsync<O, T>(IScrapingTaskFactory<O, T> factory) where O : IScrapingTask<T>;
        Task<IEnumerable<T>> RunTasksAsync<O, T>(Func<IScrapingBrowser, IEnumerable<O>> factory) where O : IScrapingTask<T>;



    }
}