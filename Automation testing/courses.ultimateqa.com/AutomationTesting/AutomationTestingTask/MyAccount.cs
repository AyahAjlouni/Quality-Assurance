using AventStack.ExtentReports;
using Bytescout.Spreadsheet;
using Mono.Unix.Native;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using sun.security.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutomationTestingTask
{
    [TestClass]
    public class MyAccount
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


        // test case three that verify updating profile information
        [TestMethod]
        public void UpdateUserData()
        {
            Worksheet sheet = Setup.ReadExcel("TC3");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("UpdateUserData()", "test case three that verify updating profile information");
                User user1 = new User();
                HomePage homePage = new HomePage(user1);
                //call StartSignIn Method to activate the driver and Url 
                try
                {
                    Setup.StartSignIn();
                    // give the user data 
                    SignInPage signinPage = new(user1);
                    MyAccountPage myAccountPage = new MyAccountPage(user1);
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = (bool)sheet.Cell(i, 2).Value;
                    test.Log(Status.Info, "sign in form reached ");
                    // call all methods that locate elements and give it the data that need it 
                    signinPage.SignInData(user1);
                    Thread.Sleep(20000);
                    homePage.ClickcaretDown();
                    homePage.ClickMyAccount();
                    // Update profile information
                    //find the path of profile photo , and update it.
                    user1.Image = (string)sheet.Cell(i, 3).Value;
                    myAccountPage.LocateImage();
                    //find the path of email , clear data then update it.
                    myAccountPage.ClearEmail();
                    user1.Email = (string)sheet.Cell(i, 4).Value;
                    Setup.Testemailvalidation(user1);
                    //find the path of first name , clear data then update it.
                    myAccountPage.ClearFname();
                    user1.FirstName = (string)sheet.Cell(i, 5).Value;
                    myAccountPage.LocateAndEnterFname();
                    //find the path of last name , clear data then update it.
                    myAccountPage.ClearLname();
                    user1.LastName = (string)sheet.Cell(i, 6).Value;
                    myAccountPage.LocateAndEnterLname();
                    //find the path of company name , clear data then update it.
                    myAccountPage.ClearCompany();
                    user1.Company = (string)sheet.Cell(i, 7).Value;
                    myAccountPage.CompanyData();
                    //find the path of professional Title , clear data then update it.
                    myAccountPage.ClearprofessionalTitle();
                    user1.Professional_Title = (string)sheet.Cell(i, 8).Value;
                    myAccountPage.professionalTitleData();
                    //select time zone from  drop down list
                    user1.TimeZone = (string)sheet.Cell(i, 9).Value;
                    myAccountPage.ChooseTimeZone();
                    myAccountPage.ClickSaveChanges();
                    Thread.Sleep(2000);

                    // Check if the expected resulte is the Same of Actual . 
                    IWebElement checkSubmit = Setup.driver.FindElement(By.XPath("//div/p[.='Your profile was successfully updated.']"));
                    string actualResult = checkSubmit.GetAttribute("innerText");
                    string expectedResult = "Your profile was successfully updated.";
                    Assert.AreEqual(expectedResult, actualResult, "Profile data has not been updated successfully");


                    // Check if the expected Image is the Same of Actual . 
                    IWebElement img = Setup.driver.FindElement(By.XPath("//div/img[@class='img-responsive']"));
                    bool expectedimg = true;// img.GetAttribute("src"); "Your profile was successfully updated.";
                    Assert.AreEqual(img.Displayed, expectedimg, "Image has not been updated successfully");

                    // Check if the expected email is the Same of Actual . 
                    IWebElement email = Setup.driver.FindElement(By.XPath("//div/input[@id='user[email]']"));
                    string actualResultemail = email.GetAttribute("value");
                    string expectedResultemail = user1.Email;
                    Assert.AreEqual(actualResultemail, expectedResultemail, "Email has not been updated successfully");


                    // Check if the expected Firstname is the Same of Actual . 
                    IWebElement Fname = Setup.driver.FindElement(By.XPath("//input[@id='user[first_name]']"));
                    string Fnametext = Fname.GetAttribute("value");
                    string expectedResultfname = user1.FirstName;
                    Assert.AreEqual(Fnametext, expectedResultfname, "First Name has not been updated successfully");


                    // Check if the expected Lastname is the Same of Actual . 
                    IWebElement Lname = Setup.driver.FindElement(By.XPath("//div/input[@id='user[last_name]']"));
                    string Lnametext = Lname.GetAttribute("value");
                    string expectedResultlname = user1.LastName;
                    Assert.AreEqual(Lnametext, expectedResultlname, "Last Name has not been updated successfully");

                    // Check if the expected Company Name is the Same of Actual . 
                    IWebElement company = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][company]']"));
                    string companytext = company.GetAttribute("value");
                    string expectedResultcomp = user1.Company;
                    Assert.AreEqual(companytext, expectedResultcomp, "company has not been updated successfully");

                    // Check if the expected Professional Title is the Same of Actual . 
                    IWebElement Pro_Title = Setup.driver.FindElement(By.XPath("//div/input[@id='user[profile_attributes][headline]']"));
                    string Pro_Titletext = Pro_Title.GetAttribute("value");
                    string Pro_Titleexpected = user1.Professional_Title;
                    Assert.AreEqual(Pro_Titletext, Pro_Titleexpected, "Professional Title has not been updated successfully");


                    IWebElement selectedtimezone = Setup.driver.FindElement(By.Id("user[profile_attributes][timezone]"));
                    SelectElement selectElement = new SelectElement(selectedtimezone);
                    string actualSelectedOption = selectElement.SelectedOption.Text;
                    Assert.AreEqual(user1.TimeZone, actualSelectedOption, "TimeZone has not been selected successfully");
                    

                    Thread.Sleep(6000);
                    //Setup.CloseDriver();
                    homePage.SignOut();

                    test.Pass("Update User Data test case successfuly");
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.TimeZone}");
                    homePage.SignOut();
                }

            }
        }


       // Method To Test if the password can be change
        [TestMethod]
        public void ChangePassword()
        {
            Worksheet sheet = Setup.ReadExcel("TC7");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("ChangePassword()", "Method To Test if the password can be change");
                User user1 = new User();
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data 
                    SignInPage signinPage = new(user1);
                    HomePage homePage = new(user1);
                    MyAccountPage myAccountPage = new MyAccountPage(user1);
                    user1.Email = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    user1.RememberMe = (bool)sheet.Cell(i, 2).Value;
                    test.Log(Status.Info, "sign in form reached ");
                    // call all methods that locate elements and give it the data that need it 
                    signinPage.SignInData(user1);
                    Thread.Sleep(20000);
                    homePage.ClickcaretDown();
                    homePage.ClickMyAccount();
                    myAccountPage.PassBtn();

                    string newPassword =(string)sheet.Cell(i, 3).Value;
                    myAccountPage.changePass(newPassword);
                    myAccountPage.RetypePass(newPassword);
                    myAccountPage.CurrentPass();
                    myAccountPage.ClickUpdate();
                    IWebElement UpdateResult = Setup.driver.FindElement(By.XPath("//div/p[.='Successfully updated your password.']"));
                    string text = UpdateResult.GetAttribute("innerText");
                    string expected = "Successfully updated your password.";
                    Assert.AreEqual(text, expected, "Password has not been updated successfully");
                    //  Setup.CloseDriver();
                    homePage.SignOut();
                    test.Pass("Change Password test case successfuly");
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //add the screenshot to report
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.Email} , {user1.Password} ");
                }
            }

        }


    }
}
