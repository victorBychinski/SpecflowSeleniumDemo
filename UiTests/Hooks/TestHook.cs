using BoDi;
using BuisenessDomainInteractions.Interfaces;
using BuisenessDomainInteractions.PageObjects;
using Framework;
using Framework.WebBrowser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Serilog;
using System.ComponentModel;
using TechTalk.SpecFlow;

namespace UiTests.Hooks
{
    [Binding]
    public class TestHook
    {
        private ScenarioContext _contnext;

        public TestHook(ScenarioContext scenarioContext)
        {
            this._contnext = scenarioContext;
        }
        [BeforeScenario]
        public void BeforeUiScenario(IObjectContainer container)
        {
            var config = BuildConfiguration();
            var logger = GetLogger();
            var driverFactory = new BrowserFactory(config, logger);
            container.RegisterInstanceAs<IConfiguration>(config);
            container.RegisterInstanceAs<ILogger>(logger);
            container.RegisterInstanceAs<IWebDriver>(driverFactory.GetWebDriver());

            container.RegisterTypeAs<BasePage, IBasePage>();
            container.RegisterInstanceAs<ILogger>(logger);
            container.RegisterFactoryAs<IWebBrowser>(c => new WebBrowser(c.Resolve<IWebDriver>(), c.Resolve<IConfiguration>(), c.Resolve<ILogger>()));
            container.RegisterFactoryAs<HomePage>(c => new HomePage(c.Resolve<IWebBrowser>(), c.Resolve<IConfiguration>(), c.Resolve<ILogger>()));

        }

        [AfterScenario]
        public void AfterUiScenario(IObjectContainer container) 
        {
            var browser = container.Resolve<IWebBrowser>();
            TakeScreenShotIfFailed(browser);
            browser.Quit();
        }

        public void TakeScreenShotIfFailed(IWebBrowser browser)
        {
            if (_contnext.TestError != null)
            {
                string name = this._contnext.ScenarioInfo.Title;
                browser.TakeScreenShot(name);
            }
        }

        private IConfiguration BuildConfiguration()
        {
            var testEnv = GetTestEnv();
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
               //.AddJsonFile($"appsettings.{testEnv}.json", optional: true);
            return builder.Build();
        }

        private ILogger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }
        private string GetTestEnv()
        {
            var env = Environment.GetEnvironmentVariable("TEST_ENV");

            return env;
        }
    }
}
