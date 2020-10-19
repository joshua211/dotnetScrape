using System;
using System.Globalization;
using System.IO;
using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;

namespace dotnetScrape.Examples.Yahoo.Tasks
{
    public class YahooStockTask : IScrapingTask<YahooStockResult>
    {
        private readonly int index;
        private readonly string link;

        public YahooStockTask(int index, string link)
        {
            this.index = index;
            this.link = link;
        }

        public YahooStockResult Run(IScrapingBrowser browser)
        {
            browser.Navigate(link);
            var price = browser.GetDecimal(Selector.Css(@"#marketsummary-itm-1 > h3 > span"));
            var name = browser.FindElement(Selector.Css(@"h1.D\(ib\)")).TextContent;
            var change = browser.FindElement(Selector.Css(@"span.Trsdu\(0\.3s\):nth-child(2)")).TextContent;

            return new YahooStockResult(name, price, change, index);
        }
    }

    public class YahooStockResult
    {
        public string FullName { get; private set; }
        public decimal Price { get; private set; }
        public string Change { get; private set; }
        public int Index { get; private set; }

        public YahooStockResult(string fullName, decimal price, string change, int taskIndex)
        {
            FullName = fullName;
            Price = price;
            Change = change;
            Index = taskIndex;
        }
    }
}