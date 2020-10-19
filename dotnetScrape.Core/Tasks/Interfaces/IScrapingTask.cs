using dotnetScrape.Core.Browser.Interfaces;

namespace dotnetScrape.Core.Tasks.Interfaces
{
    public interface IScrapingTask<T>
    {
        T Run(IScrapingBrowser browser);
    }
}