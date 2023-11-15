using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    internal class ProfilePage
    {
        public User user { get; set; }
        // to access the data in User Class
        public ProfilePage(User user)
        {
            this.user = user;
        }
        public void LocateAndClickLogin()
        {
            Thread.Sleep(2000);
            IWebElement loginButton = Setup.driver.FindElement(By.XPath("//div//a[@href='auth/lohin']"));
            Setup.ScrollToElement(Setup.driver, loginButton);
            Setup.Highlight(loginButton);
            loginButton.Click();
        }
        public void LocateAndEnterUsername()
        {
            IWebElement username = Setup.driver.FindElement(By.XPath("//input[@formcontrolname='emailControl']"));
            Setup.ScrollToElement(Setup.driver, username);
            Setup.Highlight(username);
            username.SendKeys(user.UserName);
            Thread.Sleep(3000);
        }
        public void LocateAndEnterPassword()
        {
            IWebElement password = Setup.driver.FindElement(By.XPath("//input[@type='password']"));
            Setup.ScrollToElement(Setup.driver, password);
            Setup.Highlight(password);
            password.SendKeys(user.Password);
        }
        public void LocateAndClickSignIn()
        {
            Thread.Sleep(2000);
            IWebElement signInButton = Setup.driver.FindElement(By.XPath("//form/button[contains(text(),'SIGN IN')]"));
            Setup.ScrollToElement(Setup.driver, signInButton);
            Setup.Highlight(signInButton);
            signInButton.Click();
        }

        public void SignInData(User user)
        {
            // call all methods that locate elements and give it the data that need it 
            ProfilePage signin = new(user);
            signin.LocateAndClickLogin();
            signin.LocateAndEnterUsername();
            signin.LocateAndEnterPassword();
            signin.LocateAndClickSignIn();
            Thread.Sleep(5000);

        }

        public void LocateAndClickProfile()
        {
            Thread.Sleep(2000);
            IWebElement profileButton = Setup.driver.FindElement(By.XPath("//a[contains(text(),' Profile ')]"));
            Setup.ScrollToElement(Setup.driver, profileButton);
            Setup.Highlight(profileButton);
            profileButton.Click();
        }

        public void LocateAndEnterFullname()
        {
            IWebElement fullname = Setup.driver.FindElement(By.XPath("//input[@formcontrolname='FullName']"));
            Setup.ScrollToElement(Setup.driver, fullname);
            Setup.Highlight(fullname);
            fullname.Clear();
            fullname.SendKeys(user.Fullname);
            Thread.Sleep(3000);
        }
        public void LocateAndEnterCurrentPassword()
        {
            IWebElement password = Setup.driver.FindElement(By.XPath("//input[@formcontrolname='password']"));
            Setup.ScrollToElement(Setup.driver, password);
            Setup.Highlight(password);
            password.SendKeys(user.Password);
        }
        public void LocateAndClickSubmit()
        {
            Thread.Sleep(2000);
            IWebElement submitButton = Setup.driver.FindElement(By.XPath("//button[contains(text(),'Update')]"));
            Setup.ScrollToElement(Setup.driver, submitButton);
            Setup.Highlight(submitButton);
            submitButton.Click();
        }
        public void LocateAndClickLogout()
        {
            Thread.Sleep(3000);
            IWebElement logoutButton = Setup.driver.FindElement(By.XPath("//li/a[contains(text(),'Logout')]"));
            Thread.Sleep(3000);
            //li/a/i[@class='bi bi-box-arrow-left']"));
            ////a[contains(text(),' Logout ')]"));
            Setup.ScrollToElement(Setup.driver, logoutButton);
            Setup.Highlight(logoutButton);
            Thread.Sleep(3000);
            logoutButton.Click();
            Thread.Sleep(3000);
        }
    }
}
