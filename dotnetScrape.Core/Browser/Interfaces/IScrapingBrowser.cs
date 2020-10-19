using System.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetScrape.Core.Tasks.Interfaces;
using HtmlAgilityPack;
using Knyaz.Optimus.Dom.Elements;
using Knyaz.Optimus.Dom.Interfaces;

namespace dotnetScrape.Core.Browser.Interfaces
{
    public interface IScrapingBrowser : IHtmlSearchable, IDisposable
    {
        void Navigate(string url);
        Task NavigateAsync(string url);
        void Back();
        Task BackAsync();
        HtmlElement DocumentRoot { get; }
        T ExecuteTask<T>(IScrapingTask<T> task);
        Task<T> ExecuteTaskAsync<T>(IScrapingTask<T> task);
        void ExecuteJavascript(string script);
        void DumpToFile(string path);
        string CurrentUrl { get; }
        string DocumentTitle { get; }
        IEnumerable<HtmlElement> WaitElement(Selector selector);
        BrowserState State { get; }
    }
}