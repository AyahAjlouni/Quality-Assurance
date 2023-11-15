using AventStack.ExtentReports;
using Bytescout.Spreadsheet;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    [TestClass]
    public class Profile
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

        //Method that test the admin can change their name to a valid name.
        [TestMethod]
        public void Changename_Valid()
        {
            Worksheet sheet = Setup.ReadExcel("TC1");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("Changename_Valid()", "Verify that an admin can change their name to a valid name.");
                User user1 = new User();
                ProfilePage signin = new(user1);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.Fullname = (string)sheet.Cell(i, 2).Value;

                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements in update page
                    signin.LocateAndClickProfile();
                    signin.LocateAndEnterFullname();
                    signin.LocateAndEnterCurrentPassword();
                    signin.LocateAndClickSubmit();
                    Thread.Sleep(3000);
                    test.Pass("Change name test case successfuly");
                    //compare between expected and actual
                    string expectedresult = user1.Fullname;
                    IWebElement fullname = Setup.driver.FindElement(By.XPath("//input[@formcontrolname='FullName']"));
                    string Fullnametext = fullname.GetAttribute("ng-reflect-model");
                    Assert.AreEqual(Fullnametext, expectedresult, $"Name hasn't been updated successfully {expectedresult} != {Fullnametext}");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    Thread.Sleep(2000);
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.UserName} , {user1.Password} ");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
            }
        }

        //Method that test the admin cannot change their name if they enter an incorrect password.
        [TestMethod]
        public void Changename_incorrectpass()
        {
            Worksheet sheet = Setup.ReadExcel("TC2");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("Changename_incorrectpass()",
                    "Verify that an admin cannot change their name if they enter an incorrect password");
                User user1 = new User();
                ProfilePage signin = new(user1);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();

                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.Fullname = (string)sheet.Cell(i, 2).Value;
                    //Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");

                    //locate elements in update page
                    signin.LocateAndClickProfile();
                    signin.LocateAndEnterFullname();
                    user1.Password = sheet.Cell(i, 3).Value.ToString();
                    signin.LocateAndEnterCurrentPassword();
                    signin.LocateAndClickSubmit();
                    Thread.Sleep(2000);
                    test.Pass("Change name with incorrect password test case successfuly");

                    string expectedresult = user1.Fullname;
                    IWebElement fullname = Setup.driver.FindElement(By.XPath("//input[@formcontrolname='FullName']"));
                    string Fullnametext = fullname.GetAttribute("ng-reflect-model");
                    Assert.AreEqual(Fullnametext, expectedresult, $"Name still the same {Fullnametext} and the password ({user1.Password}) is not correct");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();


                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    Thread.Sleep(2000);
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.UserName} , {user1.Password} ");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
                
                   
               
            }
        }

        //Method that test the admin cannot change their name if they enter an incorrect password.
        [TestMethod]
        public void Changename_Invalid()
        {
            Worksheet sheet = Setup.ReadExcel("TC3");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("Changename_Invalid ()",
                    "Verify that an admin cannot copmlete update name process if the full name field is empty");
                User user1 = new User();
                ProfilePage signin = new(user1);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();

                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.Fullname = (string)sheet.Cell(i, 2).Value;
                    //Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");

                    //locate elements in update page
                    signin.LocateAndClickProfile();
                    signin.LocateAndEnterFullname();
                    user1.Password = sheet.Cell(i, 3).Value.ToString();
                    signin.LocateAndEnterCurrentPassword();
                    signin.LocateAndClickSubmit();
                    Thread.Sleep(2000);
                    test.Pass("Change name with incorrect password test case successfuly");

                    string expectedresult ="*required";
                    IWebElement errormessage = Setup.driver.FindElement(By.XPath("//div/mat-error[.='*required']"));
                    string Fullnametext = errormessage.GetAttribute("innertext");
                    Assert.AreEqual(Fullnametext, expectedresult, $"Name is empty {Fullnametext}.");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, "full name field is empty");
                    //add the screenshot to report
                    Thread.Sleep(2000);
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.UserName} , {user1.Password} "); 
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }



            }
        }

     
    }
}
