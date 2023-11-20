using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace automation.Helpers;

public static class WaitHelpers
{
    public static void WaitForElementToBeVisible(IWebDriver driver, By by, int timeSpan = 5)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeSpan));
        wait.Until(ExpectedConditions.ElementIsVisible(by));
    }

    public static void WaitForElementToBeVisibleCustom(IWebDriver driver, By by)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(webDriver => webDriver.FindElement(by).Displayed && webDriver.FindElement(by).Enabled);
    }
}