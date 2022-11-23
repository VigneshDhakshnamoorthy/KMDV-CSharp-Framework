using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using KMDVFramework.Config;
using KMDVFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using AventStack.ExtentReports.Reporter;

namespace KMDVFramework.TestCases.Base
{
    [TestFixture]
    public class TestBase : Constants
    {
        [ThreadStatic] public static IWebDriver driver;
        [ThreadStatic] public static ExtentTest log;

        [OneTimeSetUp]
        public static void ExtentStart()
        {
            if (!Directory.Exists(ReportFolder))
            {
                Directory.CreateDirectory(ReportFolder);
            }
            ExtentHtmlReporter Html = new ExtentHtmlReporter(ReportFolder);
            Extent.AttachReporter(Html);
        }

        [OneTimeTearDown]
        public static void ExtentClose()
        {

            Extent.Flush();
            while (!File.Exists(Path.Combine(ReportFolder, "index.html")))
            {

                Thread.Sleep(100);
            }
            if (File.Exists(ExtentReportPath))
            {
                File.Delete(ExtentReportPath);

            }

            File.Move(Path.Combine(ReportFolder, "index.html"), ExtentReportPath);
        }




        [SetUp]
        public void Setup()
        {
            if (TestContext.CurrentContext.Test.Arguments.Length > 0)
            {
                string arg = TestContext.CurrentContext.Test.Arguments[0]?.ToString();
                driver = new BrowserUtils().GetInstance(browsername: arg);
            }
            else
            {
                driver = new BrowserUtils().GetInstance();

            }

            string fullName = TestContext.CurrentContext.Test.FullName;
            string[] listdir = fullName.Split(".");
            string Catagory = listdir[2];
            string TestName = listdir.Last();
            for (int i = 3; i < listdir.Length; i++)
            {
                if (i < listdir.Length - 2)
                {
                    Catagory = Catagory + "." + listdir[i];
                }
            }

            for (int i = listdir.Length-2; i < 4; i++)
            {
                if (i < listdir.Length - 1)
                {
                    TestName = TestName + "." + listdir[i];
                }
            }

            string author = TestContext.CurrentContext.Test.Properties.Get("Author")?.ToString();
            if (TestName.Contains('('))
            {
                log = Extent.CreateTest(TestName.Split("(")[0]).AssignCategory(Catagory).CreateNode(TestName.Split("\"")[1]).AssignAuthor(author);
            }
            else
            {
                log = Extent.CreateTest(TestName).AssignCategory(Catagory).AssignAuthor(author);
            }


        }

        [TearDown]
        public void Teardown()
        {
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            string stackTrace = TestContext.CurrentContext.Result.StackTrace;
            string errorMessage = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                log.Fail(errorMessage);
                log.Fail(stackTrace);
                log.Info("For ScreenShot Click \"base64-img\"");
                log.AddScreenCaptureFromBase64String(driver.TakeScreenshot().AsBase64EncodedString);
            }
            else if (status == TestStatus.Passed)
            {
                log.Info("For ScreenShot Click \"base64-img\"");
                log.AddScreenCaptureFromBase64String(driver.TakeScreenshot().AsBase64EncodedString);
            }
            driver.Quit();
        }


    }


}