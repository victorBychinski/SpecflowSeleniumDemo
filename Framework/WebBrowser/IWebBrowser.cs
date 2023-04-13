using OpenQA.Selenium;
using System.Security.Cryptography;

namespace Framework.WebBrowser
{
    public interface IWebBrowser
    {
        void GoTo(string url);
        void Refresh();
        bool IsDisplayed(By locator);
        void TakeScreenShot(string imageName);
        void InputText(By locator, string text);
        void PressKeyboardKey(string key);
        void PressKeyboardKey(IEnumerable<string> keys);
        string GetInnerText(By locator);
        IWebElement Findelement(By locator);
        IEnumerable<IWebElement> Findelements(By locator);
        void Click(By locator);
        void Quit();
    }
}
