using Framework.WebBrowser;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessDomainInteractions.PageObjects
{
    public class HomePage : BasePage
    {
        private By _pageHeader => By.XPath(@"//h1[contains(text(),'LetCode with Koushik')]");
        public HomePage(IWebBrowser browser, IConfiguration configuration, ILogger logger) : base(browser, configuration, logger)
        {
        }

        public bool IsUserOnTheHomePage()
        {
            return browser.IsDisplayed(_pageHeader);
        }
    }
}
