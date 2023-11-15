using com.sun.org.apache.bcel.@internal.generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTestingTask
{
    internal class SignInPage
    {

        public User user { get; set; }
        // to access the data in User Class
        public SignInPage(User user)
        {
            this.user = user;
        }
        //Method to call 
        public void SignInData(User user)
        {
            // call all methods that locate elements and give it the data that need it 
            SignInPage signinPage = new(user);
            signinPage.LocateAndClicksignInLink();
            Setup.Testemailvalidation(user);
            signinPage.LocateAndEnterPassword();
            signinPage.LocateAndCheckRememberMe();
            signinPage.ClicksignIn();
            Thread.Sleep(5000);

        }
        //Method find the xpath of pass element and Click on it
        public void LocateAndClicksignInLink()
        {
            Thread.Sleep(2000);
            IWebElement signInLink = Setup.driver.FindElement(By.XPath("//ul/li/a[contains(text(),'Sign In')]"));
            Setup.ScrollToElement(Setup.driver, signInLink);
            Setup.Highlight(signInLink);
            signInLink.Click();

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
        //Method find the xpath of Email element and send data for it
        public void LocateAndEnterEmail()
        {
            IWebElement email = Setup.driver.FindElement(By.XPath("//div/input[@id='user[email]']"));
            Setup.ScrollToElement(Setup.driver, email);
            Setup.Highlight(email);
            email.SendKeys(user.Email);
        }
       
        //Method find the xpath of pass element and send data for it
        public void LocateAndEnterPassword()
        {
            IWebElement password = Setup.driver.FindElement(By.XPath("//div/input[@id='user[password]']"));
            Setup.ScrollToElement(Setup.driver, password);
            Setup.Highlight(password);
            password.SendKeys(user.Password);
            
        }
        //Method find the xpath of Remember Me element and Check it
        public void LocateAndCheckRememberMe()
        {
            IWebElement checkRememberMe = Setup.driver.FindElement(By.XPath("//div/input[@id='user[remember_me]']"));
            Setup.ScrollToElement(Setup.driver, checkRememberMe);
            Setup.Highlight(checkRememberMe);
            if (user.RememberMe == true)
                checkRememberMe.Click();
        }

        //Method find the xpath of signIn Button and Click on it
        public void ClicksignIn()
        {
            IWebElement signIn = Setup.driver.FindElement(By.XPath("//div/button[contains(text(),'Sign in')]"));
            Setup.ScrollToElement(Setup.driver, signIn);
            Setup.Highlight(signIn);
            signIn.Click();
            Thread.Sleep(2000);
        }

        //Method to find the path of forget password link and click on it 
        public void ForgetPasswordLink()
        {
            IWebElement forgetPass = Setup.driver.FindElement(By.XPath("//div/a[contains(text(),'Forgot Password?')]"));
            Setup.Highlight(forgetPass);
            forgetPass.Click();
        }
        //Method to find the path of submit button and click on it 
        public void SubmitBtn()
        {
            IWebElement submit = Setup.driver.FindElement(By.XPath("//div/input[@type='submit']"));
            Setup.Highlight(submit);
            submit.Click();
        }
        // Method to Find the path of sign up link and click on it 
        public void signUpLink()
        {
            IWebElement signUpLink = Setup.driver.FindElement(By.XPath("//div/aside/a[@href='/users/sign_up']"));
            Setup.ScrollToElement(Setup.driver, signUpLink);
            Setup.Highlight(signUpLink);
            signUpLink.Click();
        }
        // Method to Find the path of already sign in link and click on it 
        public void signinalreadyLink()
        {
            IWebElement signinalreadyLink = Setup.driver.FindElement(By.XPath("//aside/a[contains(text(),'I already have an account!')]"));
            Setup.ScrollToElement(Setup.driver, signinalreadyLink);
            Setup.Highlight(signinalreadyLink);
            signinalreadyLink.Click();
        }

        //Method to get all methods that user need to sign in
        public void haveAccount(User user)
        {
            LocateAndClicksignInLink();
            signUpLink();
            signinalreadyLink();
            Setup.Testemailvalidation(user);
            LocateAndEnterPassword();
            LocateAndCheckRememberMe();
            ClicksignIn();
        }

    }
}
