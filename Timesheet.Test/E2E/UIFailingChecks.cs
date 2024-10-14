namespace Timesheet.Test.E2E;

using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

// Start of the class definition for UIFailingChecks.
public class UIFailingChecks {

    // Declaration of private variables for WebDriverWait and WebDriver.
    private WebDriverWait wait;
    private IWebDriver _webDriver;

    // Method called before each test method is executed.
    [SetUp]
    public void beforeEach() {
        // Set up the ChromeDriver using WebDriverManager.
        new DriverManager().SetUpDriver(new ChromeConfig());

        // Initialize a new ChromeDriver instance.
        _webDriver = new ChromeDriver();

        // Initialize a WebDriverWait instance with a timeout of 5 seconds.
        wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
    }

    // Method called after each test method is executed.
    [TearDown]
    public void afterEach() {
        // Close the current browser window.
        _webDriver.Close();

        // Quit the WebDriver and close all associated windows.
        _webDriver.Quit();
    }

    // Test method to verify if projects are shown after creation.
    [Test]
    public void testProjectsAreShownAfterCreation() {
        // Open the specified URL in the Chrome browser.
        _webDriver.Navigate().GoToUrl("http://localhost:8080/");

        // Wait until the element with the name "email" is visible.
        wait.Until(drv => drv.FindElement(By.Name("email")));

        // Find the elements necessary to complete the login form
        _webDriver.FindElement(By.Name("email")).SendKeys("admin@test.com");
        _webDriver.FindElement(By.Name("password")).SendKeys("password123");
        _webDriver.FindElement(By.CssSelector("button")).Click();

        // Wait until the element with class name "card-title" is visible.
        wait.Until(drv => drv.FindElement(By.ClassName("card-title")));

        // Navigate to the specified URL.
        _webDriver.Navigate().GoToUrl("http://localhost:8080/#/manage/projects");

        // Find the elements necessary to create a new project
        wait.Until(drv => drv.FindElement(By.CssSelector("#name")));

        _webDriver.FindElement(By.CssSelector("""[data-testid="project-name"]""")).SendKeys("Project 2");
        // FAILS because there's an error beneath; a window appears saying a 400 "Unable to add project"
        _webDriver.FindElement(By.CssSelector("""[data-testid="project-description"]""")).SendKeys("Project 2 description");
        _webDriver.FindElement(By.CssSelector(".btn-primary")).Click();

        // Navigate to the specified URL.
        _webDriver.Navigate().GoToUrl("http://localhost:8080/#/projects");

        // Find all elements with CSS selector ".col-8 .list-group-item".
        var waitForProjects = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(1));
        waitForProjects.Until(d => d.FindElement(By.CssSelector(".col-8 .list-group-item")).Displayed);
        // FAILS because it didn't wait for any elements to be "displayed"
        ReadOnlyCollection<IWebElement> projects = _webDriver.FindElements(By.CssSelector(".col-8 .list-group-item"));

        // Verify if the number of projects is equal to 2.
        Assert.That(projects.Count, Is.EqualTo(2));
    }

    // Test method to verify page updates to the project page after login.
    [Test]
    public void testPageUpdatesToProjectPageAfterLogin() {
        // Open the specified URL in the Chrome browser.
        _webDriver.Navigate().GoToUrl("http://localhost:8080/");

        // Wait until the element with the name "email" is visible.
        wait.Until(drv => drv.FindElement(By.Name("email")));

        // Find the elements necessary to complete the login form
        _webDriver.FindElement(By.Name("email")).SendKeys("admin@test.com");
        _webDriver.FindElement(By.Name("password")).SendKeys("password123");
        _webDriver.FindElement(By.CssSelector("button")).Click();

        // Wait until the element with class name "card-title" is visible.
        wait.Until(drv => drv.FindElement(By.ClassName("card-title")));

        // Find the element with CSS selector ".card-title" and get its text.
        string title = _webDriver.FindElement(By.CssSelector(".card-title")).Text;

        // Verify if the obtained title matches the expected value "Projects".
        Assert.That(title, Is.EqualTo("Projects"));
    }

}

