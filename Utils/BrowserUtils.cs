using KMDVFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace KMDVFramework.Utils
{
    public class BrowserUtils : Constants
    {
        public IWebDriver GetInstance()
        {
            
            IWebDriver dr = ChromeInstance();
            dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            dr.Manage().Window.Maximize();
            dr.Navigate().GoToUrl(DominosURL);
            return dr;
        }

        public IWebDriver GetInstance(string browsername)
        {
            IWebDriver dr;
            if (browsername.ToLower() == "chrome")
            {
                dr = ChromeInstance();
            }
            else if (browsername.ToLower() == "chromeheadless")
            {
                dr = ChromeHeadless();
            }
            else if (browsername.ToLower() == "edge")
            {
                dr = EdgeInstance();
            }
            else if (browsername.ToLower() == "edgeheadless")
            {
                dr = EdgeHeadless();
            }
            else if (browsername.ToLower() == "firefox")
            {
                dr = FirefoxInstance();
            }
            else
            {
                dr = ChromeInstance();
            }
            dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            dr.Manage().Window.Maximize();
            dr.Navigate().GoToUrl(DominosURL);
            return dr;
        }


        private ChromeDriver ChromeInstance()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }

        private ChromeDriver ChromeHeadless()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions Option = new ChromeOptions();
            Option.AddArgument("--headless");
            return new ChromeDriver(Option);
        }

        private EdgeDriver EdgeInstance()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            EdgeOptions Option = new EdgeOptions();
            Option.AddArgument("inprivate");
            return new EdgeDriver(Option);
        }

        private EdgeDriver EdgeHeadless()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            EdgeOptions Option = new EdgeOptions();
            Option.AddArgument("--headless");
            Option.AddArgument("inprivate");
            return new EdgeDriver(Option);
        }

        private FirefoxDriver FirefoxInstance()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver();
        }

    }
}
