using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;
using Knyaz.Optimus.Dom.Elements;

namespace dotnetScrape.Examples.Yahoo.Tasks
{
    public class YahooTaskFactory : IScrapingTaskFactory<YahooStockTask, YahooStockResult>
    {
        public IEnumerable<YahooStockTask> CreateTasks(IScrapingBrowser browser)
        {
            var rows = browser.WaitElement(Selector.Css("tr.simpTblRow")).ToList();
            for (int i = 0; i < rows.Count; i++)
            {
                var e = rows[i];
                var link = rows[i].GetLinks().First();
                yield return new YahooStockTask(i, link);
            }
        }
    }
}