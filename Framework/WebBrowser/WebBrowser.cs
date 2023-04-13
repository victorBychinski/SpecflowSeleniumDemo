using ConfigurationStructure;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using Utils;

namespace Framework.WebBrowser
{
    /// <summary>
    /// A wrapper arround the Selenuim webdriver. 
    /// For a demo purpose partially implemented but could be extended in the future. 
    /// Selenium could be easily replaced with some other tool
    /// </summary>
    public class WebBrowser : IWebBrowser
    {
        private IWebDriver _driver;
        private ILogger _logger;
        private IConfiguration _configuration;
        private long _defaultTimeout;
        private string _screenShotsPath;
        public WebBrowser(IWebDriver driver, IConfiguration configuration, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
            _configuration = configuration;
            _defaultTimeout = GetTimeOutFromConfiguration();
            _screenShotsPath = CurrentRunPathrovider.GetScreenShotOutputFolderPath(configuration["ScreenShotsFolder"]);
        }

        public void GoTo(string url)
        {
            _logger.Information($"Trying to navigate to: {url}");
            _driver.Navigate().GoToUrl(url);
        }

        public void Refresh()
        {
            _logger.Information($"Trying to refresh the page");
            _driver.Navigate ().Refresh();
        }

        public bool IsDisplayed(By locator)
        {
            var element = LocateElements(locator).FirstOrDefault();
            return element != null && element.Displayed;
        }
        public IWebElement Findelement(By locator)
        {
            _logger.Information($"Trying to find element on the page");

            var wait = GetDefaultWait();
            return wait.Until(d => d.FindElement(locator));
        }
        public void Click(By locator)
        {
            _logger.Information($"Trying to click element on the page");

            var element = LocateElements(locator).FirstOrDefault();
            element?.Click();
        }

        public void InputText(By locator, string text)
        {
            var element = LocateElements(locator).FirstOrDefault();
            element?.SendKeys(text);
        }

        public void PressKeyboardKey(string key)
        {
            new Actions(_driver)
                .KeyDown(key)
                .Perform();
        }

        public void PressKeyboardKey(IEnumerable<string> keys)
        {
            var action = new Actions(_driver);
            foreach (var key in keys)
            {

                action.KeyDown(key);
            }
            action.Perform();
        }

        public void GetAttribute(By locator, string attribute)
        {
            var element = LocateElements(locator).FirstOrDefault();
            element?.GetAttribute(attribute);
        }
        public string GetInnerText(By locator)
        {
            var element = LocateElements(locator).FirstOrDefault();
            return element?.Text;
        }
        public IEnumerable<IWebElement> Findelements(By locator)
        {
            return LocateElements(locator);
        }
        private IEnumerable<IWebElement> LocateElements(By locator, Type[] expeptionTypes = null)
        {
            _logger.Information($"Trying to find elements on the page");

            var wait = GetDefaultWait(expeptionTypes);
            return wait.Until(d => d.FindElements(locator));
        }

        private WebDriverWait GetDefaultWait(Type[] expeptionTypes = null)
        {
            expeptionTypes ??= new Type[] { typeof(NoSuchElementException), typeof(ElementNotVisibleException) };
            WebDriverWait wait = new (_driver, timeout: TimeSpan.FromSeconds(_defaultTimeout))
            {
                PollingInterval = TimeSpan.FromSeconds(2),
            };
            wait.IgnoreExceptionTypes(expeptionTypes);

            return wait;
        }

        private long GetTimeOutFromConfiguration()
        {
            _logger.Debug("Trying to parse timeOut from config");
            var settings = _configuration.GetSection(nameof(BrowserSettings)).Get<BrowserSettings>();

            return settings.TimeOut;
        }

        public void Quit()
        {
            if (_driver == null)
            {
                _logger.Information("Driver Instance already Killed");
                return;
            }
            try
            {
                _driver.Quit();
                _logger.Information("Quit WebDriver successfully");
            }
            catch (Exception e)
            {
                _logger.Error($"Unable to Quit  WebDriver due to {e.Message}");
            }
        }

        public void TakeScreenShot(string imageName)
        {
            
            var finalPath = _screenShotsPath + imageName.Replace(' ', '_') + ".png";
            try
            {
                Screenshot TakeScreenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                TakeScreenshot.SaveAsFile(finalPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
