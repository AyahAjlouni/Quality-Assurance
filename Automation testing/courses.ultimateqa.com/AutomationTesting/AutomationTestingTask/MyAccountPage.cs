using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTestingTask
{
    internal class MyAccountPage
    {
        public User user { get; set; }
        public MyAccountPage(User user)
        {
            this.user = user;
        }
        public void LocateImage()
        {
            IWebElement profileImage = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][avatar]' ]"));
            Setup.ScrollToElement(Setup.driver, profileImage);
            Setup.Highlight(profileImage);
            profileImage.SendKeys(user.Image);
        }

        //Method find the xpath of email and Clear the data
        public void ClearEmail()
        {
            IWebElement email = Setup.driver.FindElement(By.XPath("//div/input[@id='user[email]']"));
            email.Clear();
        }
        //Method find the xpath of First Name and Clear the data
        public void ClearFname()
        {
            IWebElement firstname = Setup.driver.FindElement(By.XPath("//input[@id='user[first_name]']"));
            firstname.Clear();
        }
        //Method find the xpath of Last Name and Clear the data
        public void ClearLname()
        {
            IWebElement lastname = Setup.driver.FindElement(By.XPath("//div/input[@id='user[last_name]']"));
            lastname.Clear();
        }

        //Method find the xpath of Company Name and Clear the data
        public void ClearCompany()
        {
            IWebElement company = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][company]']"));
            company.Clear();
        }
        //Method find the xpath of professional Title and Clear the data
        public void ClearprofessionalTitle()
        {
            IWebElement professionalTitle = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][headline]']"));
            professionalTitle.Clear();
        }
        //Method find the xpath of First Name element and send data for it
        public void LocateAndEnterFname()
        {
            IWebElement firstname = Setup.driver.FindElement(By.XPath("//input[@id='user[first_name]']"));
            Setup.ScrollToElement(Setup.driver, firstname);
            Setup.Highlight(firstname);
            firstname.SendKeys(user.FirstName);
        }
        //Method find the xpath of LAst Name element and send data for it
        public void LocateAndEnterLname()
        {
            IWebElement lastname = Setup.driver.FindElement(By.XPath("//div/input[@id='user[last_name]']"));
            Setup.ScrollToElement(Setup.driver, lastname);
            Setup.Highlight(lastname);
            lastname.SendKeys(user.LastName);
        }

        //Method find the xpath of TimeZone and select option
        public void ChooseTimeZone()
        {
            IWebElement TimeZone = Setup.driver.FindElement(By.XPath("//div/select[@id='user[profile_attributes][timezone]']"));
            Setup.ScrollToElement(Setup.driver, TimeZone);
            Setup.Highlight(TimeZone);
            // Initialize the SelectElement with the drop-down element
            SelectElement select = new SelectElement(TimeZone);
            // Select by visible text
            select.SelectByText(user.TimeZone);
        }
        //Method find the xpath of Company name element and send data
        public void CompanyData()
        {
            IWebElement company = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][company]']"));
            Setup.ScrollToElement(Setup.driver, company);
            Setup.Highlight(company);
            company.SendKeys(user.Company);
        }
        //Method find the xpath of professional Title element and send data
        public void professionalTitleData()
        {
            IWebElement professionalTitle = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][headline]']"));
            Setup.ScrollToElement(Setup.driver, professionalTitle);
            Setup.Highlight(professionalTitle);
            professionalTitle.SendKeys(user.Professional_Title);
        }


        //Method find the xpath of Save Changes Button and Click on it
        public void ClickSaveChanges()
        {
            IWebElement SaveChanges = Setup.driver.FindElement(By.XPath("//div/input[@value='Save Changes']"));
            Setup.ScrollToElement(Setup.driver, SaveChanges);
            Setup.Highlight(SaveChanges);
            SaveChanges.Click();
            Thread.Sleep(2000);
        }


        public void PassBtn()
        {
            IWebElement PasswordBtn = Setup.driver.FindElement(By.XPath("//ul/li/a[@href='/account/password']"));
            Setup.ScrollToElement(Setup.driver, PasswordBtn);
            Setup.Highlight(PasswordBtn);
            PasswordBtn.Click();
            Thread.Sleep(2000);
        }

        public void changePass(String newpass)
        {
            IWebElement changePassword = Setup.driver.FindElement(By.XPath("//div/input[@id='user[password]']"));
            Setup.ScrollToElement(Setup.driver, changePassword);
            Setup.Highlight(changePassword);
            changePassword.SendKeys(newpass);
            Thread.Sleep(2000);
        }
        public void RetypePass(String newpass)
        {
            IWebElement RetypePassword = Setup.driver.FindElement(By.XPath("//div/input[@id='user[password_confirmation]']"));
            Setup.ScrollToElement(Setup.driver, RetypePassword);
            Setup.Highlight(RetypePassword);
            RetypePassword.SendKeys(newpass);
            Thread.Sleep(2000);
        }

        public void CurrentPass()
        {
            IWebElement currentPassword = Setup.driver.FindElement(By.XPath("//div/input[@id='user[current_password]' ]"));
            Setup.ScrollToElement(Setup.driver, currentPassword);
            Setup.Highlight(currentPassword);
            currentPassword.SendKeys(user.Password);
            Thread.Sleep(2000);
        }
        public void ClickUpdate()
        {
            IWebElement Update = Setup.driver.FindElement(By.XPath("//div/input[@value='Update']"));
            Setup.ScrollToElement(Setup.driver, Update);
            Setup.Highlight(Update);
            Update.Click();
            Thread.Sleep(5000);

        }

    }
}
