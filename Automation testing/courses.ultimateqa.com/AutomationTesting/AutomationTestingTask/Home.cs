using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytescout.Spreadsheet;

namespace AutomationTestingTask
{
    [TestClass]
    public class Home
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

        // Method Not Required in the task 
        //Method that test the Sign Out From Account
        [TestMethod]
        public void SignOut()
        {
            Worksheet sheet = Setup.ReadExcel("TC5");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SignOut()", "Method that test the Sign Out From Account");

                User user1 = new User();
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data 

                    SignInPage signinPage = new SignInPage(user1);
                    HomePage homePage = new HomePage(user1);
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = true;
                    test.Log(Status.Info, "sign in form reached ");
                    signinPage.SignInData(user1);
                    Thread.Sleep(10000);
                    homePage.SignOut();
                    test.Pass("SignOut test case successfuly");
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.FirstName} , {user1.LastName} ");
                }
                // Setup.CloseDriver();
            }
        }
    }
}
