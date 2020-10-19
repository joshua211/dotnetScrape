using System.Net;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetScrape.Core.Browser.Interfaces;
using dotnetScrape.Core.Tasks.Interfaces;
using HtmlAgilityPack;
using Knyaz.Optimus;
using Knyaz.Optimus.Dom.Elements;
using Knyaz.Optimus.TestingTools;
using Knyaz.Optimus.ResourceProviders;
using Knyaz.Optimus.ScriptExecuting.Jint;

namespace dotnetScrape.Core.Browser
{
    public class OptimusBrowser : IScrapingBrowser
    {
        private readonly Engine engine;

        public BrowserState State { get; private set; }

        public OptimusBrowser(bool executeJs)
        {
            engine = executeJs ? EngineBuilder.New().UseJint().Build()
                                : EngineBuilder.New().Build();

            engine.UseKnownUserAgent(KnownUserAgents.Windows_Firefox);
            State = new BrowserState()
            {
                History = new Stack<string>(),
                Cookies = new CookieContainer(),
                CurrentUri = engine.Uri
            };
            engine.OnUriChanged += () => State.CurrentUri = engine.Uri;
        }

        public string CurrentUrl => State.CurrentUri.AbsolutePath;

        public HtmlElement DocumentRoot { get => engine.Document.DocumentElement as HtmlElement; }

        public string DocumentTitle => engine.Document.GetElementsByTagName("title")[0].TextContent;

        public void Back()
        {
            BackAsync().Wait();
        }

        public void ExecuteJavascript(string script)
        {
            engine.ScriptExecutor.Execute("text/javascript", script);
        }

        public T ExecuteTask<T>(IScrapingTask<T> task)
        {
            var currentState = State.CurrentUri;
            var result = task.Run(this);

            if (currentState != State.CurrentUri)
                engine.OpenUrl(currentState.AbsolutePath);

            return result;
        }

        public HtmlElement FindElement(Selector selector)
        {
            var x = engine.Document.QuerySelector(selector.Query) as HtmlElement;
            switch (selector.Type)
            {
                case SelectorType.Css:
                    return engine.Document.QuerySelector(selector.Query) as HtmlElement;
                case SelectorType.Id:
                    return engine.Document.GetElementById(selector.Query) as HtmlElement;
                default:
                    return null;
            }
        }

        public IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            switch (selector.Type)
            {
                case SelectorType.Css:
                    return engine.Document.QuerySelectorAll(selector.Query) as IEnumerable<HtmlElement>;
                case SelectorType.Id:
                    return new List<HtmlElement>() { engine.Document.GetElementById(selector.Query) as HtmlElement };
                default:
                    return null;
            }
        }

        public void Navigate(string url)
        {
            NavigateAsync(url).Wait();
        }

        public void Dispose()
        {
            engine.Dispose();
        }

        public async Task NavigateAsync(string url)
        {
            State.History.Push(url);
            await engine.OpenUrl(url);
        }

        public async Task BackAsync()
        {
            if (State.History.Count > 0)
            {
                await NavigateAsync(State.History.Pop());
            }
        }

        public async Task<T> ExecuteTaskAsync<T>(IScrapingTask<T> task)
        {
            var result = await Task.Run(() => task.Run(this));

            return result;
        }

        public void DumpToFile(string path) => engine.DumpToFile(path);


        public IEnumerable<HtmlElement> WaitElement(Selector selector)
        {
            switch (selector.Type)
            {
                case SelectorType.Css:
                    return engine.WaitSelector(selector.Query).ToHtmlElements();
                case SelectorType.Id:
                    return new List<HtmlElement>() { engine.WaitId(selector.Query) as HtmlElement };
            }

            throw new Exception("Not supported");
        }

        private void UseState(BrowserState state)
        {
            if (CurrentUrl != state.CurrentUri.AbsolutePath)
                Navigate(state.CurrentUri.AbsolutePath);
            State = state;
            //TODO handle cookies
        }
    }
}