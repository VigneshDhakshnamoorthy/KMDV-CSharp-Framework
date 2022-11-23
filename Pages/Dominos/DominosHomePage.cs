using AventStack.ExtentReports;
using NUnit.Framework;
using KMDVFramework.Pages.Base;
using OpenQA.Selenium;

namespace KMDVFramework.Pages.Dominos
{
    public class DominosHomePage : PageBase
    {
        public DominosHomePage(IWebDriver driver, ExtentTest log) : base(driver, log)
        {
        }

        private readonly By OrderOnlineBtn = By.XPath("//button[text()='ORDER ONLINE NOW']");

        public void IsHomePageLoaded()
        {
            Assert.That(GetElement(OrderOnlineBtn).Displayed, Is.True, "HomePage Not Loaded");
            log.Pass("HomePage Loaded Succesfully");

        }

        public void ClickOrderOnline()
        {
            Click(OrderOnlineBtn);
            log.Pass("Order Online Button Clicked Succesfully");
        }
    }

}
