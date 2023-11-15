using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using javax.swing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Bytescout.Spreadsheet;

namespace AutomationTestingTask
{
    [TestClass]
    public class SignIn
    {

        [ClassInitialize]
        public static void InitiateClass(TestContext testContext)
        {
            Setup.extentReports.AttachReporter(Setup.reporter);
            Setup.OpenDriver();
        }

        [ClassCleanup]
        public static void CleanUpClass()
        {
            Setup.extentReports.Flush();
            //Method as distractor
            Setup.CloseDriver();

        }

        // test case one that verify entering invalid username and password on the login page
        [TestMethod]
        public void TestSignIn_withInvalidData()
        {
            Worksheet sheet = Setup.ReadExcel("TC1");

            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("TestSignIn_withInvalidData()", "test case one that verify entering invalid username and password on the login page");
                //call StartSignIn Method to activate the driver and Url 
                User user1 = new User();
                try
                {
                    Setup.StartSignIn();
                    // give the user data 
                    SignInPage signinPage = new SignInPage(user1);
                    HomePage homePage = new HomePage(user1);
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = (bool)sheet.Cell(i, 2).Value;
                    // call all methods that locate elements and give it the data that need it 
                    signinPage.SignInData(user1);
                    test.Log(Status.Info, "sign in form reached ");
                    Thread.Sleep(10000);
                    // Check if the expected Result is the Same of Actual .
                    IWebElement message_Invalid = Setup.driver.FindElement(By.XPath("//div/p[.='Invalid email or password.']"));
                    string actualResult = message_Invalid.GetAttribute("innerText");
                    string expectedResult = "Invalid email or password.";
                    Assert.AreEqual(expectedResult, actualResult, "actualResult isn't the same of expectedResult");
                    test.Pass("TestSignIn_withInvalidData test case successfuly");
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.Email} , {user1.Password}");
                }
            }
            Thread.Sleep(2000);
            //Setup.CloseDriver();
        }

        // test case two that verify entering valid username and password on the login page
        [TestMethod]
        public void TestSignIn_withValidData()
        {
            Worksheet sheet = Setup.ReadExcel("TC2");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("TestSignIn_withValidData()", "test case two that verify entering valid username and password on the login page");
                User user1 = new User();
                //call StartSignIn Method to activate the driver and Url 
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data 
                    SignInPage signinPage = new SignInPage(user1);
                    HomePage homePage = new HomePage(user1);
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = (bool)sheet.Cell(i, 2).Value;

                    signinPage.SignInData(user1);
                    Thread.Sleep(2000);
                    test.Log(Status.Info, "sign in form reached ");
                    // Check if the expected Result is the Same of Actual .
                    string expectedResult = "https://courses.ultimateqa.com/collections";
                    string actualResult = Setup.driver.Url;
                    Assert.AreEqual(expectedResult, actualResult, "actualResult isn't the same of expectedResult");
                    // Console.WriteLine("successfuly");
                    test.Pass("TestSignIn_withValidData test case successfuly");
                    homePage.SignOut();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.Email} , {user1.Password}");
                }

            }
            Thread.Sleep(2000);
            //  Setup.CloseDriver();
        }

        // Method Not Required in the task 
        //Method to Check if the proccess of Forget password is working or not 
        [TestMethod]
        public void TestForgetPassword()
        {
            Worksheet sheet = Setup.ReadExcel("TC4");

            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("TestForgetPassword()", "Method to Check if the proccess of Forget password is working or not ");
                User user1 = new User();
                //call StartSignIn Method to activate the driver and Url 
                try
                {
                    Setup.StartSignIn();
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    HomePage homePage = new HomePage(user1);
                    SignInPage signinPage = new(user1);
                    signinPage.LocateAndClicksignInLink();
                    signinPage.ForgetPasswordLink();
                    Setup.Testemailvalidation(user1);
                    signinPage.SubmitBtn();
                    test.Log(Status.Info, "sign in form reached ");
                    // Check if the expected Result is the Same of Actual . 
                    IWebElement checkSubmit = Setup.driver.FindElement(By.XPath("//article//h2[.='Help is on the way!']"));
                    string text = checkSubmit.GetAttribute("innerText");
                    string actualResult = "Help is on the way!";
                    Assert.AreEqual(text, actualResult, "Submit button doesn't worke successfully");
                    test.Pass("TestForgetPassword test case successfuly");
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.Email}");
                }
                Thread.Sleep(2000);
                //Setup.CloseDriver();
            }
        }

        // Method Not Required in the task 
        //Method that check if the element 'already i have account' is working or not 
        [TestMethod]
        public void Testalreadyhaveaccount()
        {
            Worksheet sheet = Setup.ReadExcel("TC6");

            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("Testalreadyhaveaccount()", "Method that check if the element 'already i have account' is working or not ");
                User user1 = new User();
                //call StartSignIn Method to activate the driver and Url 
                try
                {
                    Setup.StartSignIn();
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = true;
                    HomePage homePage = new HomePage(user1);
                    SignInPage signinPage = new(user1);
                    signinPage.haveAccount(user1);
                    Thread.Sleep(6000);
                    test.Log(Status.Info, "sign in form reached ");
                    // Check if the expected Result is the Same of Actual .
                    IWebElement Dashboard = Setup.driver.FindElement(By.XPath("//nav/ul/li/a[@href='/enrollments']"));
                    string expectedResult = Dashboard.GetAttribute("innerText");
                    string actualResult = "My Dashboard";
                    Assert.AreEqual(expectedResult, actualResult, "actualResult isn't the same of expectedResult");
                    test.Pass("Testalreadyhaveaccount test case successfuly");
                    homePage.SignOut();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.Email} , {user1.Password}");
                }
                Thread.Sleep(3000);
                //  Setup.CloseDriver();
            }
        }


    }
}