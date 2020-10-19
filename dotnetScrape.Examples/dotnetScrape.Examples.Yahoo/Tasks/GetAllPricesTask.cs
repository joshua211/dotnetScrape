using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;
using Knyaz.Optimus.Dom.Elements;

namespace dotnetScrape.Examples.Yahoo.Tasks
{
    public class GetAllPricesTask : IScrapingTask<IEnumerable<StockPriceResult>>
    {
        public IEnumerable<StockPriceResult> Run(IScrapingBrowser browser)
        {
            var watch = new Stopwatch();
            watch.Start();
            var results = new List<StockPriceResult>();
            var rows = browser.FindElements(Selector.Css("tr.simpTblRow"));
            System.Console.WriteLine($"-{watch.ElapsedMilliseconds}ms to get all rows");
            foreach (var row in rows)
            {
                //TODO make an extension to use FindElement on HtmlElements
                var name = ((HtmlElement)row.ChildNodes.ToList()[0]).TextContent;
                var price = Convert.ToDecimal(((HtmlElement)row.ChildNodes.ToList()[2]).TextContent, CultureInfo.InvariantCulture);

                results.Add(new StockPriceResult(name, price));
            }

            return results;
        }
    }

    public class StockPriceResult
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public StockPriceResult(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}