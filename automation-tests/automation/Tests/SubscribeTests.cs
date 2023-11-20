using automation.Helpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace automation.Tests;

[TestClass]
public class SubscribeTests
{
    private IWebDriver _driver;

    [TestInitialize]
    public void Init()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("http://localhost:4200");
    }
    
    [TestMethod]
    public void Check_Name_ErrorMessage()
    {
       // Arrange
       var id = "name";
       var message = "1234A";
       CompleteInput(id,message);
  
       
        // Act
        ClickSubscribe();
        WaitHelpers.WaitForElementToBeVisible(_driver,  By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[1]/p"));
        
        var errorMsg =
            _driver.FindElement(
                By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[1]/p"));
        
        // Assert
        errorMsg.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void Check_Email_ErrorMessage()
    {
        // Arrange
        var id = "email";
        var message = "1234A";
        CompleteInput(id,message);

        // Act
        ClickSubscribe();
        WaitHelpers.WaitForElementToBeVisible(_driver,  By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[2]/p"));
        
        var errorMsg =
            _driver.FindElement(
                By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[2]/p"));
        
        // Assert
        errorMsg.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void Check_PhoneNumber_ErrorMessage()
    {
        // Arrange
        var id = "phonenumber";
        var message = "1234A";
        CompleteInput(id,message);
        
        // Act
        ClickSubscribe();
        WaitHelpers.WaitForElementToBeVisible(_driver,  By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[3]/p"));
        
        var errorMsg =
            _driver.FindElement(
                By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/div[3]/p"));
        
        // Assert
        errorMsg.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void Subscribe_Made_Successfully()
    {
        // Arrange
        var ids = new[] { "name", "email", "phonenumber" };
        var messages = new[] { "Vladutz", "vlad_Boss@yahoo.com","0707070707" };
        for (int i = 0; i < ids.Length; i++)
        {
            CompleteInput(ids[i],messages[i]);
        }
        
        // Act
        ClickSubscribe();
        WaitHelpers.WaitForElementToBeVisible(_driver,By.XPath("/html/body/app-root/app-subscriber-page/div/app-confirm-subscription/h3"));
        var markedResultMsg =
            _driver.FindElement(By.XPath("/html/body/app-root/app-subscriber-page/div/app-confirm-subscription/h3"));
        
        // Assert
        markedResultMsg.Text.Should().NotBeNull();
    }

    [TestMethod]
    public void Check_Error_Email_Already_Subscribed()
    {
        // Arrange
        var ids = new[] { "name", "email", "phonenumber" };
        var messages = new[] { "Vladutz", "razvan-andrei.canuci@student.tuiasi.ro","0707070707" };
        for (int i = 0; i < ids.Length; i++)
        {
            CompleteInput(ids[i],messages[i]);
        }
        
        // Act
        ClickSubscribe();
        var errorXPath = By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/p");
        WaitHelpers.WaitForElementToBeVisible(_driver,errorXPath);
        
        var errorMsg =
            _driver.FindElement(errorXPath);
        
        // Assert
        errorMsg.Text.Should().NotBeNull();
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

    private void ClickSubscribe()
    {
        var subscribeBtn = _driver.FindElement(By.XPath("/html/body/app-root/app-subscriber-page/div/app-subscriber-form/form/button"));
        subscribeBtn.Click();
    }
}