using ConfigurationStructure;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Serilog;


namespace Framework
{
    public class BrowserFactory
    {
        private IConfiguration _configuration;
        private ILogger _logger;

        public BrowserFactory(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IWebDriver GetWebDriver()
        {
            var driver = GetBrowserType() switch
            {
                BrowserType.Chrome => GetChrome(),
                BrowserType.FireFox => GetFirefox(),
                BrowserType.Edge => GetEdge(),
                _ => throw new NotSupportedException("Browser type is not supported.")
            };
            return driver;
        }

        private IWebDriver GetEdge()
        {
            var edgeOptions = new EdgeOptions
            {
                AcceptInsecureCertificates = true,
                PageLoadStrategy = PageLoadStrategy.Eager
            };
            edgeOptions.AddArguments( "--disable-gpu", "--no-sandbox", "start-maximized");
            edgeOptions.AddExcludedArgument("enable-automation");
            edgeOptions.AddUserProfilePreference("credentials_enable_service", false);
            edgeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            return new EdgeDriver(edgeOptions);
        }

        private IWebDriver GetFirefox()
        {
            var fireFoxOptions = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            fireFoxOptions.SetPreference("permissions.default.image", 2);
            fireFoxOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            return new FirefoxDriver(fireFoxOptions);
        }

        private IWebDriver GetChrome()
        {
            var chromeOption = new ChromeOptions();
            chromeOption.AddArguments("--disable-gpu", "--no-sandbox", "start-maximized");
            chromeOption.AddExcludedArgument("enable-automation");
            //chromeOption.AddAdditionalCapability("useAutomationExtension", false);
            chromeOption.AddUserProfilePreference("credentials_enable_service", false);
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
            return new ChromeDriver(chromeOption);
        }

        private BrowserType GetBrowserType()
        {
            var browserSettings = _configuration.GetSection(nameof(BrowserSettings)).Get<BrowserSettings>();
            var browserType = browserSettings.BrowserType;

            return browserType;
        }
    }
}
