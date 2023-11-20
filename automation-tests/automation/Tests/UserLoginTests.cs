using automation.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace automation.Tests;

[TestClass]
public class UserLoginTests
{
    private IWebDriver _driver;
    
    [TestInitialize]
    public void Init()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("http://localhost:4200/user");
    }

    [TestMethod]
    public void Check_Error_Message_Username_Or_Password_Wrong()
    {
        // Arrange
        var username = "traianelemer";
        var password = "aaa";

        // Act
        CompleteInput("username", username);
        CompleteInput("password", password);
        ClickLoginButton();

        var errorMsgXpath = By.XPath("/html/body/app-root/app-login-form/div/form/p");
        WaitHelpers.WaitForElementToBeVisible(_driver,  errorMsgXpath);
        
        var errorMsg =
            _driver.FindElement(
                errorMsgXpath);
        
        // Assert
        errorMsg.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void Login_Successfully_User_Flow()
    {
        // Arrange
        var username = "traianelemer";
        var password = "America4$";

        // Act
        CompleteInput("username", username);
        CompleteInput("password", password);
        ClickLoginButton();

        var usernameXpath = By.XPath("/html/body/app-root/app-user-page/nav/div/div[1]/p[1]");
        
        WaitHelpers.WaitForElementToBeVisible(_driver,  usernameXpath);
        
        var usernameMsg =
            _driver.FindElement(
                usernameXpath);

        // Assert
        usernameMsg.Text.Should().Be(username);
    }

    [TestMethod]
    public void Login_Successfully_To2FA_Admin()
    {
        // Arrange
        var username = "vasiletraian";
        var password = "Miketyson123$";
        var twoFactorMessage = "2FA:";

        // Act
        CompleteInput("username", username);
        CompleteInput("password", password);
        ClickLoginButton();

        var twoFactorXPath = By.XPath("/html/body/app-root/app-factor-page/div/h3");
        
        WaitHelpers.WaitForElementToBeVisible(_driver,  twoFactorXPath);
        
        var twoFactorMsg =
            _driver.FindElement(
                twoFactorXPath);

        // Assert
        twoFactorMsg.Text.Should().Be(twoFactorMessage);
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _driver.Quit();
    }
    
    private void CompleteInput(string id, string message)
    {
        WaitHelpers.WaitForElementToBeVisible(_driver, By.Id(id));
        
        var input = _driver.FindElement(By.Id(id));
        
        input.SendKeys(message);
    }

    private void ClickLoginButton()
    {
        var loginBtn = _driver.FindElement(By.XPath("/html/body/app-root/app-login-form/div/form/button"));
        loginBtn.Click();
    }
    
}