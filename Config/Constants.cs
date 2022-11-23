using AventStack.ExtentReports;
using System;
using System.IO;

namespace KMDVFramework.Config
{
    public class Constants
    {
        public static string ProjectFolder = Environment.CurrentDirectory.Split("bin")[0];
        public static string ReportFolder = Path.Combine(ProjectFolder, @"Report\");
        public static string RepositoryFolder = Path.Combine(ProjectFolder, @"Repository\");
        public static string DataRepositoryFolder = Path.Combine(RepositoryFolder, @"DataRepository\");
        public static string ObjectRepositoryFolder = Path.Combine(RepositoryFolder, @"ObjectRepository\");
        public static string ExcelData = Path.Combine(DataRepositoryFolder, "Dominos.xlsx");
        public static string PropertyData = Path.Combine(ObjectRepositoryFolder, "Object.properties");

        public readonly int WaitTime = 15;

        public readonly static string DominosURL = "https://www.dominos.co.in/";
        public readonly static string OneTrackerURL = "https://onetrackerqa.azurewebsites.net/";

        public readonly static string TestCaseSheetName = "Web";
        public readonly static string ProductSheetName = "Products";

        public readonly static string MailHost = "outlook.office365.com";
        public readonly static int MailPort = 587;

        public readonly static string UserNameCES = "vignesh.dhakshnamoorthy@cesltd.com";
        public readonly static string UserPasswordCES = "Kmdv@1990";
        public readonly static string SenderName = "Vignesh Dhakshnamoorthy";
        public readonly static string UserNameOAK = "vdhakshnamoorthy@oaktreecapital.com";
        public readonly static string UserPasswordOAK = "KMD.Vicky@060490";

        public readonly static string RecipientMail = "eswararao.kanakala@cesltd.com";

        public static ExtentReports Extent = new ExtentReports();
        public static string CurrentTime = DateTime.Now.ToString("dd-MMM-yy__tt_hh-mm");
        public static string ExtentReportPath = Path.Combine(ReportFolder, CurrentTime + ".html");

        public readonly static string OakTreeMail = "vdhakshnamoorthy@oaktreecapital.com";

    }
}
