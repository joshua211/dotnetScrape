using System.Runtime.Versioning;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetScrape.Core.Browser.Interfaces;
using Knyaz.Optimus.Dom.Elements;
using Knyaz.Optimus.Dom.Interfaces;

namespace dotnetScrape.Core.Browser
{
    public static class Extensions
    {
        public static IEnumerable<string> GetLinks(this HtmlElement element)
        {
            var tags = element.QuerySelectorAll("a");
            return tags.Select(t => ((HtmlElement)t).GetAttribute("href"));
        }

        public static decimal GetDecimal(this IScrapingBrowser browser, Selector selector)
        {
            var element = browser.FindElement(selector);

            return Convert.ToDecimal(element.TextContent, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<decimal> GetDecimals(this IScrapingBrowser browser, Selector selector)
        {
            var elements = browser.FindElements(selector);
            foreach (var e in elements)
                yield return Convert.ToDecimal(e.TextContent, CultureInfo.InvariantCulture);
        }

        public static int GetInteger(this IScrapingBrowser browser, Selector selector)
        {
            var element = browser.FindElement(selector);

            return Convert.ToInt32(element.TextContent, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<decimal> GetIntegers(this IScrapingBrowser browser, Selector selector)
        {
            var elements = browser.FindElements(selector);

            foreach (var e in elements)
                yield return Convert.ToInt32(e.TextContent, CultureInfo.InvariantCulture);
        }

        public static double GetDouble(this IScrapingBrowser browser, Selector selector)
        {
            var element = browser.FindElement(selector);

            return Convert.ToDouble(element.TextContent, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<double> GetDoubles(this IScrapingBrowser browser, Selector selector)
        {
            var elements = browser.FindElements(selector);

            foreach (var e in elements)
                yield return Convert.ToDouble(e.TextContent, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<HtmlElement> ToHtmlElements(this IEnumerable<IElement> elements)
        {
            foreach (var e in elements)
                yield return (HtmlElement)e;
        }

        public static IEnumerable<HtmlElement> ToHtmlElements(this NodeList list)
        {
            foreach (var l in list)
                yield return (HtmlElement)l;
        }
    }
}