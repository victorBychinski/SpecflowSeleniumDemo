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
    public class InputPage : BasePage
    {
        private By _fullName => By.XPath(@"//input[@id='fullName']");
        private By _appendText => By.XPath(@"//input[@id='join']");
        private By _getValue => By.XPath(@"//input[@id='getMe']");
        private By _clearMe => By.XPath("//input[@id='clearMe']");
        private By _disabledField => By.XPath(@"//input[@id='noEdit']");
        private By _isReadOnly => By.XPath(@"//input[@id='dontwrite']");

        public InputPage(IWebBrowser browser, IConfiguration configuration, ILogger logger) : base(browser, configuration, logger)
        {
        }

        public void InputName(string name)
        {
            browser.InputText(_fullName, name);
        }

        public void AppendTextToAppropriateFieldAndPressTab(string text)
        {
            browser.Click(_appendText);
            browser.PressKeyboardKey(Keys.End);
            browser.InputText(_appendText, text);
            browser.PressKeyboardKey(Keys.Tab);
        }
    }
}
