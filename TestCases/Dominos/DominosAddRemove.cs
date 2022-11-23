using KMDVFramework.TestCases.Base;
using NUnit.Framework;
using KMDVFramework.Pages.Dominos;

namespace KMDVFramework.TestCases.Dominos
{
    public class DominosAddRemove : TestBase
    {
      
        
        [Author("VigneshDhakshnamoorthy")]
        public void AddRemoveE2E()
        {
            DominosProduct Page = new DominosProduct(driver, log);
            Page.IsHomePageLoaded();
            Page.ClickOrderOnline();
            Page.IsOrderOnlinePageLoaded();
            Page.ClosePopup();
            Page.ClickLocateMeBtn();
            Page.EnterLocation("600100");
            Page.IsProductPageLoaded();
            Page.AddCartList("ADD CART LIST - VEG PIZZA");
            Page.VerifyCartValue();
            Page.AddCartList("ADD CART LIST - NON-VEG PIZZA");
            Page.VerifyCartValue();
            Page.AddCartList("ADD CART LIST - SIDES");
            Page.VerifyCartValue();
            Page.RemoveList("REMOVE CART LIST");
            Page.VerifyCartValue();


        }
    }
}
