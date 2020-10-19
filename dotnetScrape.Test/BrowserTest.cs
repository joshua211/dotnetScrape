using dotnetScrape.Core.Browser;
using dotnetScrape.Core.Browser.Interfaces;
using MinimalProfiler.Core.Attributes;
using MinimalProfiler.Core.Profiling;
using Xunit;

namespace dotnetScrape.Test
{
    public class BrowserTest
    {
        private readonly IScrapingBrowser browser;

        public BrowserTest()
        {
            browser = new OptimusBrowser(true);
            var profiler = Profiler.Create()
                                    .UseAssemblies(typeof(BrowserTest).Assembly)
                                    .Build();
        }

        [Fact]
        [ProfileMe]
        public void NavigationShouldWork()
        {
            string excpectedTitle = "Google";

            browser.Navigate("https://www.google.de/");

            Assert.Equal(excpectedTitle, browser.DocumentTitle);
        }
    }
}
