namespace dotnetScrape.Core.Bots
{
    public class BotConfig
    {
        public int ConcurrencyLevel { get; private set; }
        public bool ExecuteJavascript { get; private set; }

        public BotConfig(int concurrencyLevel, bool executeJavascript = true)
        {
            ConcurrencyLevel = concurrencyLevel;
            ExecuteJavascript = executeJavascript;
        }
    }
}