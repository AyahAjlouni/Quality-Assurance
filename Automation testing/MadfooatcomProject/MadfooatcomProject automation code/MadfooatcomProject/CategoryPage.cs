using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    internal class CategoryPage
    {
        public User user { get; set; }
        public CategoryData categoryData { get; set; }

        public CategoryPage(CategoryData categoryData)
        {
            this.categoryData = categoryData;
        }

        // to access the data in User Class
        public CategoryPage(User user)
        {
            this.user = user;
        }
        //Method to locate and click on create button in random row
        public void LocateAndcreateButtonT(int randCategoryIndex)
        {
            IWebElement createButton = Setup.driver.FindElement(By.XPath($"//tbody[{randCategoryIndex}]/tr/td[6]/button[1]"));
            Setup.ScrollToElement(Setup.driver, createButton);
            Setup.Highlight(createButton);
            createButton.Click();
        }
       
        //Method to locate and enter Bill name from excel 
        public void LocateAndEnterBillname()
        {
            IWebElement Billname = Setup.driver.FindElement(By.XPath("//input[@ng-reflect-name='Billname']"));
            Setup.ScrollToElement(Setup.driver, Billname);
            Setup.Highlight(Billname);
            Billname.SendKeys(categoryData.BillName);
            Thread.Sleep(3000);
        }
       
        //Method to locate and enter Email from excel 
        public void LocateAndEnterEmail()
        {
            IWebElement email = Setup.driver.FindElement(By.XPath("//input[@ng-reflect-name='Email']"));
            Setup.ScrollToElement(Setup.driver, email);
            Setup.Highlight(email);
            email.SendKeys(categoryData.Email);
            Thread.Sleep(3000);
        }
        //Method to locate and enter Location from excel 
        public void LocateAndEnterlocation()
        {
            IWebElement location = Setup.driver.FindElement(By.XPath("//input[@ng-reflect-name='Location']"));
            Setup.ScrollToElement(Setup.driver, location);
            Setup.Highlight(location);
            location.SendKeys(categoryData.Location);
            Thread.Sleep(3000);
        }

        //Method to locate and click on create button to create the category 
        public void LocateAndClickCreate()
        {
            Thread.Sleep(2000);
            IWebElement createButton = Setup.driver.FindElement(By.XPath("//button[contains(text(),'create')]"));
            Setup.ScrollToElement(Setup.driver, createButton);
            Setup.Highlight(createButton);
            createButton.Click();
        }

        //Method to locate and click on show button to check the randomly category created
        public void LocateAndShowButtonT(int randCategoryIndex)
        {
            Thread.Sleep(3000);
            IWebElement showButton = Setup.driver.FindElement(By.XPath($"//tbody[{randCategoryIndex}]/tr/td[1]/button[1]"));
            Setup.ScrollToElement(Setup.driver, showButton);
            Setup.Highlight(showButton);
            showButton.Click();
        }


    }
}
