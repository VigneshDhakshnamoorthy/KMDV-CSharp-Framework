using AventStack.ExtentReports;
using NUnit.Framework;
using KMDVFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KMDVFramework.Pages.Base
{
    public class PageBase : Constants
    {
        protected IWebDriver driver;
        protected ExtentTest log;

        public PageBase(IWebDriver driver, ExtentTest log)
        {
            this.driver = driver;
            this.log = log;

        }

        public void Quit() => driver.Quit();
        public void LogPass(string Message) => log.Pass(Message);
        public void LogInfo(string Message) => log.Info(Message);
        public void LogFail(string Message) => log.Fail(Message);
        public void LogError(string Message) => log.Error(Message);
        public void Sleep(int seconds) => SleepMilli(seconds * 1000);
        public void SleepMilli(int milliseconds) => Thread.Sleep(milliseconds);

        public void Click(By by) => GetElement(by).Click();
        public void Click(IWebElement ele) => WaitForDisplay(ele).Click();
        public void Click(By by, int WaitingTime) => GetElement(by, WaitingTime).Click();

        public void Type(By by, string value) => GetElement(by).SendKeys(value);
        public void Type(IWebElement ele, string value) => WaitForDisplay(ele).SendKeys(value);

        public void ScrollInto(IWebElement ele) => ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", ele);
        public void ScrollInto(By by) => ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", GetElement(by));

        public void JSClick(IWebElement ele) => ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", ele);
        public void JSClick(By by) => ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", GetElement(by));
        public void JSClick(By by, int WaitingTime) => ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", GetElement(by, WaitingTime));

        public void SwitchFrame(string Frame) => driver.SwitchTo().Frame(Frame);
        public void SwitchFrame(int Frame) => driver.SwitchTo().Frame(Frame);
        public void SwitchFrame(IWebElement ele) => driver.SwitchTo().Frame(ele);

        public void DefaultFrame() => driver.SwitchTo().DefaultContent();

       

        public IWebElement GetElement(By by)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(WaitTime),
                PollingInterval = TimeSpan.FromMilliseconds(3)
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            return fluentWait.Until(x => x.FindElement(by));
        }

        public IWebElement GetElement(By by, int WaitingTime)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(WaitingTime),
                PollingInterval = TimeSpan.FromMilliseconds(3)
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
            return fluentWait.Until(x => x.FindElement(by));
        }

        public List<IWebElement> GetElements(By by)
        {
            List<IWebElement> elements = new List<IWebElement>();
            for (int i = 0; i < WaitTime * 1000; i++)
            {
                try
                {
                    elements.AddRange(driver.FindElements(by).ToList());
                    break;
                }
                catch (Exception) { continue; }
            }
            return elements;
        }

        public List<string> GetTextFromList(By by)
        {
            List<string> list = new List<string>();
            foreach (var element in GetElements(by))
            {
                list.Add(element.Text);
            }

            return list;
        }

        public void ClickFromList(By by, string value)
        {
            foreach (var element in GetElements(by))
            {
                if (element.Text.ToLower() == value.ToLower())
                {
                    element.Click();
                }
            }

        }

        public bool IsPageLoaded()
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete") | ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("active");

        }

        public void WaitForPageLoad()
        {
            for (int i = 0; i < WaitTime * 1000; i++)
            {
                if (IsPageLoaded())
                {
                    break;
                }
                else { continue; }
            }

        }

        public string GetText(By by)
        {
            return GetElement(by).Text;
        }

        public string GetText(IWebElement ele)
        {
            return ele.Text;
        }

        public void VerifyTitle(string title)
        {
            string actual = driver.Title;
            if (actual.ToLower() == title.ToLower())
            {
                log.Pass("Title Verified");
                Assert.That(actual, Is.EqualTo(title).IgnoreCase);
            }
            else
            {
                log.Fail("Title Not Verified");
                Assert.That(actual, Is.EqualTo(title).IgnoreCase);
            }
        }
        public IWebDriver GetDriver()
        {
            return driver;
        }

        public IWebElement WaitForDisplay(IWebElement ele)
        {
            for (int i = 0; i < WaitTime * 1000; i++)
            {
                if (ele.Displayed)
                {
                    break;
                }
                else
                {
                    SleepMilli(1);
                }

            }
            return ele;
        }

        public IWebElement WaitForDisplay(By by)
        {
            IWebElement ele = GetElement(by);
            for (int i = 0; i < WaitTime * 1000; i++)
            {
                if (ele.Displayed)
                {
                    break;
                }
                else
                {
                    SleepMilli(1);
                }

            }
            return ele;
        }

        public string GetTitle()
        {
            log.Info(driver.Title);
            return driver.Title;
        }


        public SelectElement SelectDropDown(By by)
        {
            return new SelectElement(GetElement(by));
        }

        public SelectElement SelectDropDown(IWebElement ele)
        {
            return new SelectElement(ele);
        }
    }
}