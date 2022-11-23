using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace KMDVFramework.Pages.Dominos
{
    public class DominosOrderOnline : DominosHomePage
    {
        public DominosOrderOnline(IWebDriver driver, ExtentTest log) : base(driver, log)
        {
        }

        private readonly By LocateMeBtn = By.XPath("//div[@class='srch-cnt-srch']/div/button");
        private readonly By LocationInputBox = By.XPath("//div[@class='sc-jhAzac boPDyK']/div/input");
        private readonly By LocationSearchBtn = By.XPath("//div[@class='sc-jhAzac boPDyK']/div/button");
        private readonly By ResultLocationOption = By.XPath("(//div[@class='lst-wrpr'])[1]");

        private readonly By PopupAdCloseBtn = By.XPath("//button[@data-id='CLOSE']");
        private readonly string PopupFrame = "moe-onsite-campaign-633e64135e6bba5b2249ca22";

        public void IsOrderOnlinePageLoaded()
        {
            Assert.That(GetElement(LocateMeBtn).Displayed, Is.True, "OrderOnline Page Not Loaded");
            log.Pass("OrderOnline Page Loaded Succesfully");
        }

        public void ClosePopup()
        {
            try
            {
                SwitchFrame(PopupFrame);
                Click(PopupAdCloseBtn, 3);
                log.Pass("Popup Closed Succesfully");
                DefaultFrame();

            }
            catch (Exception)
            {
                log.Info("No Popup Available");
                DefaultFrame();
            }

        }

        public void ClickLocateMeBtn()
        {
            Click(LocateMeBtn);
            WaitForDisplay(LocationSearchBtn);
            log.Pass("Order Online Button Clicked Succesfully");
        }

        public void EnterLocation(string Pincode)
        {
            Type(LocationInputBox, Pincode);
            log.Pass($"Location ({Pincode}) Entered Succesfully");
            Sleep(1);
            JSClick(ResultLocationOption, 5);
            

        }
    }
}
