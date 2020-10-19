using System.Collections.Generic;
using dotnetScrape.Core.Browser;
using Knyaz.Optimus.Dom.Elements;

namespace dotnetScrape.Core
{
    public interface IHtmlSearchable
    {
        HtmlElement FindElement(Selector selector);
        IEnumerable<HtmlElement> FindElements(Selector selector);
    }
}