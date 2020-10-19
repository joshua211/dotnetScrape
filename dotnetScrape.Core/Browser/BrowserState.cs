using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace dotnetScrape.Core.Browser
{
    public class BrowserState
    {
        public CookieContainer Cookies { get; set; }
        public Stack<string> History { get; set; }
        public Uri CurrentUri { get; set; }


        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Cookies.GetHashCode() + History.GetHashCode() + CurrentUri.GetHashCode() + GetHashCode();
        }

        public override string ToString()
        {
            return $"{CurrentUri}[{History.Count}] | {Cookies.Count} cookies";
        }

        public BrowserState GetSnapshot()
        {
            var json = JsonConvert.SerializeObject(this);
            var snapshot = JsonConvert.DeserializeObject<BrowserState>(json);
            return snapshot;
        }
    }
}