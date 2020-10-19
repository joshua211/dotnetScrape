using System;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;

namespace dotnetScrape.Core.Bots.Internal
{
    internal class TaskHelper<T> : IScrapingTask<T>
    {
        private Func<IScrapingBrowser, T> taskFunction;

        internal TaskHelper(Func<IScrapingBrowser, T> taskFunction)
        {
            this.taskFunction = taskFunction;
        }

        public T Run(IScrapingBrowser browser)
        {
            return taskFunction(browser);
        }
    }
}