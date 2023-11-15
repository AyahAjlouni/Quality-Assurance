using MadfooatcomProject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    internal class ReportPage
    {
        public User user { get; set; }
        public CategoryData categoryData { get; set; }
        // to access the data in User Class
        public ReportPage(User user)
        {
           
            this.user = user;
        }
        public ReportPage(CategoryData categoryData)
        {
            this.categoryData = categoryData;
        }
       
        //Method to locate and click on report button
        public void LocateAndClickReport()
        {
            Thread.Sleep(2000);
            IWebElement ReportButton = Setup.driver.FindElement(By.XPath("//li/a[contains(text(),'Report')]"));
            Setup.ScrollToElement(Setup.driver, ReportButton);
            Setup.Highlight(ReportButton);
            ReportButton.Click();
        }

        //Method to locate and select option from category drop down list by value that from excel
        public void LocateAndChooseCategory()
        {
            IWebElement selectCategory = Setup.driver.FindElement(By.XPath("//select[@formcontrolname='Billercategoryname']"));
            Setup.ScrollToElement(Setup.driver, selectCategory);
            Setup.Highlight(selectCategory);
            // Initialize the SelectElement with the drop down element
            SelectElement select = new SelectElement(selectCategory);
            // Select by value
            select.SelectByValue(categoryData.Categoryname);
        }

        //Method to locate and select option randomly from category drop down list
        public void LocateAndChooseCategoryRandom()
        {
            IWebElement selectCategory = Setup.driver.FindElement(By.XPath("//select[@formcontrolname='Billercategoryname']"));
            Setup.ScrollToElement(Setup.driver, selectCategory);
            Setup.Highlight(selectCategory);
            // Initialize the SelectElement with the drop down element
            SelectElement select = new SelectElement(selectCategory);
            //cont the number of options 
            int optionsCount = select.Options.Count;
            //choose random number
            Random random = new Random();
            int randomIndex = random.Next(1, optionsCount);
            // Select by index
            select.SelectByIndex(randomIndex);
        }

        //Method to locate and click on view button
        public void LocateAndClickView()
        {
            Thread.Sleep(2000);
            IWebElement viewButton = Setup.driver.FindElement(By.XPath("//tr[1]/td[4]/button"));
            Setup.ScrollToElement(Setup.driver, viewButton);
            Setup.Highlight(viewButton);
            viewButton.Click();
        }

        //Method to locate and click on view button randomly from list of categories
        public void LocateAndClickViewT(int random)
        {
            Thread.Sleep(2000);
            IWebElement viewButton = Setup.driver.FindElement(By.XPath($"//tr[{random}]/td[4]/button"));
            Setup.ScrollToElement(Setup.driver, viewButton);
            Setup.Highlight(viewButton);
            viewButton.Click();
        }

        //Method to locate and enter start date from excel 
        public void LocateAndEnterStartDate()
        {
            IWebElement startDate = Setup.driver.FindElement(By.XPath("//div/input[@formcontrolname='DateFrom']"));
            Setup.ScrollToElement(Setup.driver, startDate);
            Setup.Highlight(startDate);
            Thread.Sleep(1000);
            startDate.SendKeys(categoryData.startDate);
            Thread.Sleep(3000);
        }

        //Method to locate and enter end date from excel 
        public void LocateAndEnterEndDate()
        {
            IWebElement endDate = Setup.driver.FindElement(By.XPath("//div/input[@formcontrolname='DateTo']"));
            Setup.ScrollToElement(Setup.driver, endDate);
            Setup.Highlight(endDate);
            Thread.Sleep(1000);
            endDate.SendKeys(categoryData.EndDate);
            endDate.SendKeys(Keys.ArrowUp);
            endDate.SendKeys(Keys.ArrowDown);
            Thread.Sleep(3000);
        }
        
    }
}




