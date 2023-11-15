using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTestingTask
{
    internal class HomePage
    {
        public User user { get; set; }
        public HomePage(User user)
        {
            this.user = user;
        }

        //Method find the xpath of caret Down and Click on it
        public void ClickcaretDown()
        {
            IWebElement caretDown = Setup.driver.FindElement(By.XPath("//ul/li/button/i"));////ul/li/button/i[@aria-hidden='true']
            Setup.ScrollToElement(Setup.driver, caretDown);
            Setup.Highlight(caretDown);
            caretDown.Click();
            Thread.Sleep(2000);
        }

        public void ClicksignOut()
        {
            //check if Sign out Clickable
            IWebElement signOut = Setup.driver.FindElement(By.XPath("//li/a[@href='/users/sign_out']"));
            Setup.ScrollToElement(Setup.driver, signOut);
            Setup.Highlight(signOut);
            signOut.Click();
        }

        public void SignOut() {
            Thread.Sleep(2000);
            ClickcaretDown();
            Thread.Sleep(2000);
            ClicksignOut();
            Thread.Sleep(2000);
        }

        //Method find the xpath of My Account element and Click on it
        public void ClickMyAccount()
        {
            IWebElement myAccount = Setup.driver.FindElement(By.XPath("//ul/li/ul/li/a[contains(text(),'My Account')]"));
            Setup.ScrollToElement(Setup.driver, myAccount);
            Setup.Highlight(myAccount);
            myAccount.Click();
            Thread.Sleep(2000);
        }
      
    }
}
