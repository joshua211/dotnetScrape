using dotnetScrape.Core.Tasks.Interfaces;

namespace dotnetScrape.Core.Tasks
{
    public class ScrapingTaskFactoryBuilder<O, T> where T : IScrapingTask<O>
    {
        public static ScrapingTaskFactoryBuilder<O, T> Create()
                            => new ScrapingTaskFactoryBuilder<O, T>();


    }
}