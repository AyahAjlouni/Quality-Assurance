using AventStack.ExtentReports;
using Bytescout.Spreadsheet;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    [TestClass]
    public class Category
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

        //	PBI: Create new bill on a specific category. 

        //check that admin able to add new bills on categories randomly
        [TestMethod]
        public void AddBillCategory_Valid()
        {
            Worksheet sheet = Setup.ReadExcel("TC8");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("AddBillCategory_Valid()",
                    "check that admin able to add new bills on categories randomly");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                CategoryPage categoryPage = new (data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.BillName= (string)sheet.Cell(i, 2).Value;
                    data.Email= (string)sheet.Cell(i, 3).Value;
                    data.Location= (string)sheet.Cell(i, 4).Value;

                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements
                    IWebElement categoryTable = Setup.driver.FindElement(By.XPath("//div/div/table"));
                    IList<IWebElement> categoryRows = categoryTable.FindElements(By.TagName("tbody"));
                    
                    //random number to get random category
                    Random random = new Random();
                    int randCategoryIndex = random.Next(1, categoryRows.Count);

                    categoryPage.LocateAndcreateButtonT(randCategoryIndex);
                    //create the bill category
                    categoryPage.LocateAndEnterBillname();
                    categoryPage.LocateAndEnterEmail();
                    categoryPage.LocateAndEnterlocation();
                    categoryPage.LocateAndClickCreate();
                    test.Pass("add bill category test case successfuly");

                    categoryPage.LocateAndShowButtonT(randCategoryIndex);

                    Thread.Sleep(3000);
                   
                    IWebElement billertable = Setup.driver.FindElement(By.XPath("//td/table"));
                    IList<IWebElement> billerRows = billertable.FindElements(By.XPath("//tbody/tr"));
                    bool Found = false;

                    foreach (IWebElement row in billerRows)
                    {
                        IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                        if (cells.Any(cell => cell.Text.Contains(data.BillName, StringComparison.OrdinalIgnoreCase)))   //enumeration is used when comparing strings in a case-insensitive manner
                        {
                            Found = true;
                            break;
                        }

                        if (cells.Any(cell => cell.Text.Contains(data.Email, StringComparison.OrdinalIgnoreCase)))   //enumeration is used when comparing strings in a case-insensitive manner
                        {
                            Found = true;
                            break;
                        }

                        if (cells.Any(cell => cell.Text.Contains(data.Location, StringComparison.OrdinalIgnoreCase)))   //enumeration is used when comparing strings in a case-insensitive manner
                        {
                            Found = true;
                            break;
                        }
                    }
                    Assert.IsTrue(Found, $"Bill name '{data.BillName}' or Bill email '{data.Email}' " +
                        $"or Bill location '{data.Location}' not found in the table.");


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

        [TestMethod]
        public void AddBillCategory_Invalid()
        {
            Worksheet sheet = Setup.ReadExcel("TC9");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("AddBillCategory_Invalid",
                    "check that admin can't add new bills on categories with invalid data  ");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                CategoryPage categoryPage = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url
                    Setup.StartSignIn();
                    //give the user data from excel
                   user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.BillName = (string)sheet.Cell(i, 2).Value;
                    data.Email = (string)sheet.Cell(i, 3).Value;
                    data.Location = (string)sheet.Cell(i, 4).Value;

                   // Method for logging in system

                   signin.SignInData(user1);
                   test.Log(Status.Info, "user login successfuly");

                    //locate elements
                    IWebElement categoryTable = Setup.driver.FindElement(By.XPath("//div/div/table"));
                    IList<IWebElement> categoryRows = categoryTable.FindElements(By.TagName("tbody"));

                    //random number to get random category
                    Random random = new Random();
                    int randCategoryIndex = random.Next(1, categoryRows.Count);

                    categoryPage.LocateAndcreateButtonT(randCategoryIndex);

                    //create the bill category
                    categoryPage.LocateAndEnterBillname();
                    categoryPage.LocateAndEnterEmail();
                    categoryPage.LocateAndEnterlocation();

                    IWebElement errormessage = Setup.driver.FindElement(By.XPath("//div/mat-error"));
                    string errormessageText = errormessage.GetAttribute("innertext");
                    string expected = "*required";
                    Assert.AreEqual(expected, errormessageText, "the account con't created");

                    //categoryPage.LocateAndClickCreate();
                    test.Pass("add bill category with invalid data test case successfuly");
                    
                    

                    Setup.driver.Navigate().GoToUrl("http://localhost:4200/admin/home");

                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, "location cant be null ");
                    //add the screenshot to report
                    Thread.Sleep(2000);
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.UserName} , {user1.Password} ");
                    Thread.Sleep(3000);
                    Setup.driver.Navigate().GoToUrl("http://localhost:4200/admin/home");
                    signin.LocateAndClickLogout();
                }
            }
        }

      
    }
}
