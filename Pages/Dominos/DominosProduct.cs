using AventStack.ExtentReports;
using NUnit.Framework;
using KMDVFramework.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace KMDVFramework.Pages.Dominos
{
    public class DominosProduct : DominosOrderOnline
    {
        public DominosProduct(IWebDriver driver, ExtentTest log) : base(driver, log)
        {
        }

        private readonly By ProductPageList = By.XPath("//span[@class='itm-dsc__nm']");
        private readonly By AddExtraNO = By.XPath("//button/span[text()='NO THANKS']");
        private readonly By EachProductPrice = By.XPath("//span[@class='crt-cnt-qty-prc-txt']/span[@class='rupee']");
        private readonly By SubTotalPrice = By.XPath("//span[@class='rupee sb-ttl']");


        public void IsProductPageLoaded()
        {
            WaitForPageLoad();
            Assert.That(GetElement(ProductPageList).Displayed, Is.True, "Product Page Not Loaded");
            log.Pass("Product Page Loaded Succesfully");


        }

        public void AddCartProduct(string ProductName, int Qty)
        {
            WaitForPageLoad();
            WaitForDisplay(ProductPageList);
            By ProductBy = By.XPath($"(//div[@data-label='{ProductName}']//child::button[@data-label='addTocart'])[1]");
            JSClick(ProductBy);
            try
            {
                Click(AddExtraNO, 1);
            }
            catch (Exception) { }
            By QtyBy = By.XPath($"//div[@class='crt-cnt-descrptn']/span[text()='{ProductName}']/../../..//child::span[@class='cntr-val']");
            By PriceBy = By.XPath($"//div[@class='crt-cnt-descrptn']/span[text()='{ProductName}']/../../..//child::span[@class='rupee']");
            By IncreaseQty = By.XPath($"//div[@class='crt-cnt-descrptn']/span[text()='{ProductName}']/../../..//child::div/div[@data-label='increase']");
            int PendingQty = Qty - int.Parse(GetText(QtyBy));
            for (int i = 0; i < PendingQty; i++)
            {
                JSClick(IncreaseQty);
            }
            WaitForDisplay(PriceBy);
            log.Pass($"Added to Cart - {ProductName} | Qty - {GetText(QtyBy)} | Price - {GetText(PriceBy)}");

        }

        public void AddCartList(string ListName)
        {
            log.Pass($"Product List : {ListName}");
            foreach (var (key, value) in new ExcelUtils().GetProducts(ListName))
            {
                AddCartProduct(key, value);
            }
        }

        public void RemoveProduct(string ProductName, int Qty)
        {
            By DecreaseQty = By.XPath($"//div[@class='crt-cnt-descrptn']/span[text()='{ProductName}']/../../..//child::div/div[@data-label='decrease']");
            By QtyBy = By.XPath($"//div[@class='crt-cnt-descrptn']/span[text()='{ProductName}']/../../..//child::span[@class='cntr-val']");
            string PreviousQty = GetText(QtyBy);
            for (int i = 0; i < Qty; i++)
            {
                JSClick(DecreaseQty);
            }

            log.Pass($"From Product {ProductName} - {Qty} Qty Removed | Previous Qty - {PreviousQty} | Current Qty - {GetText(QtyBy)}");

        }

        public void RemoveList(string ListName)
        {
            log.Pass($"Product List : {ListName}");
            foreach (var (key, value) in new ExcelUtils().GetProducts(ListName))
            {
                RemoveProduct(key, value);
            }
        }

        public void VerifyCartValue()
        {
            List<string> EachProduct = GetTextFromList(EachProductPrice);
            float ProductTotal = 0;
            foreach (var item in EachProduct)
            {
                ProductTotal += float.Parse(item);
            }

            if (ProductTotal == float.Parse(GetText(SubTotalPrice)))
            {
                log.Pass($"Each Product Total Value ( {ProductTotal} ) == Sub Total Value ( {GetText(SubTotalPrice)} )");
            }
            else
            {
                Assert.That(ProductTotal, Is.EqualTo(float.Parse(GetText(SubTotalPrice))), $"Each Product Total Value ( {ProductTotal} ) != Sub Total Value ( {GetText(SubTotalPrice)} )");
            }
        }
    }
}
