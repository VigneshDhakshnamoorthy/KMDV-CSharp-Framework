using NPOI.XSSF.UserModel;
using NUnit.Framework;
using KMDVFramework.Config;
using System;
using System.Collections.Generic;
using System.IO;

namespace KMDVFramework.Utils
{
    public class ExcelUtils : Constants
    {


        public static IEnumerable<TestCaseData> GetTestCase(string tcname)
        {
            XSSFWorkbook wb = new XSSFWorkbook(File.Open(ExcelData, FileMode.Open));
            var Sheet = wb.GetSheet(TestCaseSheetName);
            int Rowcount = Sheet.LastRowNum;
            var testCases = new List<TestCaseData>();

            for (int i = 0; i <= Rowcount; i++)
            {
                try
                {
                    string cellvalue = Sheet.GetRow(i).GetCell(0).StringCellValue;
                    if (cellvalue.ToLower() == tcname.ToLower())
                    {
                        for (int j = i + 2; j <= Rowcount; j++)
                        {
                            if (Sheet.GetRow(j).GetCell(0).StringCellValue.ToLower() == "y")
                            {
                                testCases.Add(new TestCaseData(Sheet.GetRow(j).GetCell(5).StringCellValue));
                            }
                        }
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            if (testCases != null)
            {
                foreach (TestCaseData testCaseData in testCases)
                {
                    yield return testCaseData;
                }
            }
        }

        public Dictionary<string, int> GetProducts(string ProductName)
        {
            XSSFWorkbook wb = new XSSFWorkbook(File.Open(ExcelData, FileMode.Open));
            var Sheet = wb.GetSheet(ProductSheetName);
            int Rowcount = Sheet.LastRowNum;
            var products = new Dictionary<string, int>();

            for (int i = 0; i <= Rowcount; i++)
            {
                try
                {
                    string cellvalue = Sheet.GetRow(i).GetCell(0).StringCellValue;

                    if (cellvalue.ToLower() == ProductName.ToLower())
                    {
                        for (int j = i + 1; j <= Rowcount; j++)
                        {
                            products.Add(Sheet.GetRow(j).GetCell(0).StringCellValue, (int)Sheet.GetRow(j).GetCell(1).NumericCellValue);
                        }
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return products;

        }

    }

}
