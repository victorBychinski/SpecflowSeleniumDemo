using BuisenessDomainInteractions.Interfaces;
using Framework.WebBrowser;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Serilog;


namespace BuisenessDomainInteractions.PageObjects
{
    public class BasePage : IBasePage
    {
        protected IWebBrowser browser;
        protected ILogger logger;
        private IConfiguration _configuration;

        private By _pageLogo => By.XPath(@"//app-header/nav[@role='navigation']//img[contains(@src, 'logo.png')]");
        private By _workspace => By.XPath(@"//app-header//a[@id='testing']");
        private By _courses => By.XPath(@"//app-header//div//a[text()='Courses']");
        private By _grooming => By.XPath(@"//app-header//div//a[text()='Grooming']");
        private By _product => By.XPath(@"//app-header//div//a[text()='Product']");
        private By _home => By.XPath(@"//app-header//a[@routerlink='/']//span");
        private By GetDropDownItemLocator(string item) => By.XPath($"//app-header//div//a[text()='{item}']x");
        public BasePage(IWebBrowser browser, IConfiguration configuration, ILogger logger)
        {
            this.browser = browser;
            this.logger = logger;
            _configuration = configuration;

        }
        public void Navigate()
        {
            logger.Information("Navigating to the base url");
            browser.GoTo(GetBaseUrl);
        }
        public void GoToWorkspaces()
        {
            logger.Information("Navigating to the base url");
            browser.GoTo(GetBaseUrl);
        }
        public void GoToCourse(string course)
        {
            browser.Click(_workspace);
            browser.Click(GetDropDownItemLocator(course));
        }

        public void GoHome()
        {
            browser.Click(_home);
        }
        public void Refresh()
        {
            browser.Refresh();
        }
        protected string GetBaseUrl => _configuration["BaseUrl"];

    }
}
