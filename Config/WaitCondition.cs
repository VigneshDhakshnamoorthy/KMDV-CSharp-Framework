using OpenQA.Selenium;
using System;

namespace KMDVFramework.Config
{
    public class WaitCondition
    {
        public static Func<IWebDriver, IWebElement> ElementIsDisplayedIsEnabled(By locator)
        {
            return (driver) =>
            {
                IWebElement element = driver.FindElement(locator);
                if (element.Displayed
                && element.Enabled)
                {
                    return element;
                }

                return null;
            };
        }
    }
}
