using System.Collections.Generic;
using dotnetScrape.Core.Browser.Interfaces;

namespace dotnetScrape.Core.Tasks.Interfaces
{
    public interface IScrapingTaskFactory<O, T> where O : IScrapingTask<T>
    {
        IEnumerable<O> CreateTasks(IScrapingBrowser browser);
    }
}