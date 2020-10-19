namespace dotnetScrape.Core.Browser
{
    public class Selector
    {
        public SelectorType Type { get; private set; }
        public string Query { get; private set; }

        private Selector(SelectorType type, string query)
        {
            Type = type;
            Query = query;
        }

        public static Selector Css(string css)
        {
            return new Selector(SelectorType.Css, css);
        }

        public static Selector Id(string id)
        {
            return new Selector(SelectorType.Id, id);
        }
    }

    public enum SelectorType
    {
        Id,
        Css,
    }
}