using Framework.WebBrowser;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Serilog;
using System.Xml.Linq;


namespace BuisenessDomainInteractions.PageObjects
{
    public class WorkSpace : BasePage
    {
        private By _pageHeader => By.XPath(@"//app-test-site//section[1]//h1");
        private By _pageMenuItem => By.XPath(@"//app-test-site//section//app-menu");
        private By GetEditButtonLocatorForMenuItem(string menuItemTitle) => By.XPath($"//app-test-site//section//app-menu//header//p[contains(text(), '{menuItemTitle}')]/ancestor::app-menu[1]//footer/a[text()='Edit']");
        private By GetEditButtonLocatorByMenuItemDescription(string description) => By.XPath($"//app-test-site//section//app-menu/div/div//span[contains(text(), \"{description}\")]/ancestor::app-menu[1]//footer/a[text()='Edit']");
        public WorkSpace(IWebBrowser browser, IConfiguration configuration, ILogger logger) : base(browser, configuration, logger)
        {
        }

        public string GetPageHeader()
        {
            logger.Information("Trying to get Workspace page header text.");
            return browser.GetInnerText(_pageHeader);
        }

        public void SelectSectionToPracticeBySectionName(string name)
        {
            logger.Information($"Trying to go to practice for {name} section.");
            browser.Click(GetEditButtonLocatorForMenuItem(name));
        }

        public void SelectSectionToPracticeBySectionDescription(string descr)
        {
            logger.Information($"Trying to go to practice for section with description {descr} section.");
            browser.Click(GetEditButtonLocatorByMenuItemDescription(descr));
        }
    }
}
