using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Bytescout.Spreadsheet;
using Bytescout.Spreadsheet.COM;
using Microsoft.AspNetCore.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace MadfooatcomProject
{
    [TestClass]
    public class Report
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
        //PBL: Search by Category name

        //Enter a valid category name and verify that the search results are displayed to the same category name
        [TestMethod]
        public void SearchByName()
        {
            Worksheet sheet = Setup.ReadExcel("TC4");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByName()",
                    "verify that the search results are displayed to the same category name");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);
               
               // report = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.Categoryname=(string)sheet.Cell(i, 2).Value;

                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements in update page
                    report.LocateAndClickReport();
                    report.LocateAndChooseCategory();
                    
                    test.Pass("Choose category test case successfuly");
                    //compare between expected and actual
                    IWebElement selectedCategory = Setup.driver.FindElement(By.XPath("//select[@formcontrolname='Billercategoryname']"));
                    SelectElement selectElement = new SelectElement(selectedCategory);
                    string selectedOption = selectElement.SelectedOption.Text;
                    IWebElement Categoryresult = Setup.driver.FindElement(By.XPath($"//tr/td[contains(text(),' {data.Categoryname} ')]"));
                    string resulttext = Categoryresult.Text;
                    Assert.AreEqual(resulttext, selectedOption, $"Categoryresult {resulttext} and selectedOption {selectedOption} are the same");

                    
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


        //Check that the search results are displayed to the same category name that choosen randomly
        [TestMethod]
        public void SearchByNameRandomly()
        {
            Worksheet sheet = Setup.ReadExcel("TC12");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByNameRandomly()",
                    "Check that the search results are displayed to the same category name that choosen randomly");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);

                // report = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    //data.Categoryname = (string)sheet.Cell(i, 2).Value;

                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements in update page
                    report.LocateAndClickReport();
                    Thread.Sleep(2000);
                    report.LocateAndChooseCategoryRandom();
                    test.Pass("Choose category test case successfuly");
                    Thread.Sleep(3000);
                    ////compare between expected and actual
                    IWebElement selectedCategory = Setup.driver.FindElement(By.XPath("//select[@formcontrolname='Billercategoryname']"));
                    SelectElement selectElement = new SelectElement(selectedCategory);
                    string OptionText = selectElement.SelectedOption.Text;
                    Console.WriteLine("Selected option: " + OptionText);
                    Thread.Sleep(2000);
                    IWebElement Categoryresult = Setup.driver.FindElement(By.XPath("//tbody/tr[1]/td[1]"));
                    string resulttext = Categoryresult.Text;
                    Console.WriteLine("Actuel option: " + resulttext);
                    Assert.AreEqual(resulttext, OptionText, $"Categoryresult {resulttext} and selectedOption {OptionText} are not the same");
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

        //check the number of pay inside category same with count pay
        [TestMethod]
        public void SearchByName_withcountpay()
        {
            Worksheet sheet = Setup.ReadExcel("TC4");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByName_withcountpay",
                    "check the number of pay inside category same with count pay");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);
                //report = new(data);
                //CategoryPage categoryPage = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.Categoryname = (string)sheet.Cell(i, 2).Value;

                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements in update page
                    Thread.Sleep(3000);
                    report.LocateAndClickReport();
                    Thread.Sleep(3000);
                    report.LocateAndChooseCategory();
                    
                    test.Pass("Choose category test case successfuly");

                    // get the value of count payment from the table 
                    IWebElement table = Setup.driver.FindElement(By.XPath("//div/table"));
                    Thread.Sleep(3000);
                    IList<IWebElement> rows = table.FindElements(By.TagName("tr"));
                    Thread.Sleep(2000);
                    string secondColVal = ""; 
                    foreach (var row in rows)
                    {
                        IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                        Thread.Sleep(2000);
                        if (cells.Count >= 1)
                        {
                            secondColVal = cells[1].Text; 
                        }
                    }
                    int actualval = int.Parse(secondColVal);
                    report.LocateAndClickView();
                    //count the number of invoics in the table
                    Thread.Sleep(3000);
                    IList<IWebElement> ListOfElements = Setup.driver.FindElements(By.TagName("tr"));
                    int countlist = ListOfElements.Count - 1;
                    Console.WriteLine(countlist);

                    Thread.Sleep(5000);
                   
                    //compare between expected and actual
                    Assert.AreEqual(actualval, countlist, " the count pay is same of invoics number ");
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


        //PBL: Search by date
        //Verify that the search results aren’t displayed when no results are found.
        [TestMethod]
        public void SearchByDate()
        {
            Worksheet sheet = Setup.ReadExcel("TC5");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByDate_with days not have result",
                    "Verify that the search results aren’t displayed when no results are found.");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);
                //report = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.startDate = sheet.Cell(i, 2).Value.ToString();
                    data.EndDate = sheet.Cell(i, 3).Value.ToString();

                    Thread.Sleep(2000);
                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");

                    //locate elements in update page
                    report.LocateAndClickReport();

                    report.LocateAndEnterStartDate();
                    Thread.Sleep(2000);
                    report.LocateAndEnterEndDate();
                    test.Pass("Choose start date and end date successfuly");
                    Thread.Sleep(3000);

                    IList<IWebElement> ListOfElements = Setup.driver.FindElements(By.TagName("tr"));
                    int countlist = ListOfElements.Count - 1;
                    Console.WriteLine(countlist);
                    int expectedcountlist = 1;
                    Assert.AreEqual(expectedcountlist, countlist, " no invoics in these dayes ");
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

        //Verify that the search results are not displayed if the start date is after the end date
        [TestMethod]
        public void SearchByDate_invalid()
        {
            Worksheet sheet = Setup.ReadExcel("TC6");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByDate_invalid()",
                    "Verify that the search results are not displayed if the start date is after the end date");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);
               
               // report = new();
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.startDate = sheet.Cell(i, 2).Value.ToString();
                    data.EndDate = sheet.Cell(i, 3).Value.ToString();

                    Thread.Sleep(2000);
                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");

                    //locate elements in update page
                    report.LocateAndClickReport();
                    Thread.Sleep(2000);
                    report.LocateAndEnterStartDate();
                    Thread.Sleep(2000);
                    report.LocateAndEnterEndDate();
                    Thread.Sleep(2000);
                    test.Pass("Choose start date and end date successfuly");

                    //count the number of rows in table
                    IList<IWebElement> ListOfElements = Setup.driver.FindElements(By.TagName("tr"));
                    int countlist = ListOfElements.Count - 1;
                    Console.WriteLine(countlist);
                    int expectedcountlist = 1;
                    Assert.AreEqual(expectedcountlist, countlist, " no invoics appear , the dates not correct ");
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


        //Verify that the user enters a valid start date and end date and search results are displayed correctly.
        [TestMethod]
        public void SearchByDateValid()
        {
            Worksheet sheet = Setup.ReadExcel("TC7");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByDate_valid()",
                    "Verify that the user enters a valid start date and end date and search results are displayed correctly.");
                User user1 = new User();
                ProfilePage signin = new(user1);
                CategoryData data = new CategoryData();
                ReportPage report = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url
                    Setup.StartSignIn();
                    //give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.startDate = sheet.Cell(i, 2).Value.ToString();
                    data.EndDate = sheet.Cell(i, 3).Value.ToString();
                    Thread.Sleep(2000);
                    //Method for logging in system

                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //enter the data in screen and locate the elements
                    report.LocateAndClickReport();
                    report.LocateAndEnterStartDate();
                    Thread.Sleep(3000);
                    report.LocateAndEnterEndDate();
                    Thread.Sleep(3000);
                    test.Pass("Choose start date and end date successfuly");
                    Thread.Sleep(6000);
                    
                    //locate the rows of report table 
                    IWebElement reportsTable = Setup.driver.FindElement(By.XPath("//div/div/table"));
                    IList<IWebElement> reportsRows = reportsTable.FindElements(By.TagName("tbody"));

                    int rowsCount = reportsRows.Count;
                    // choose random number 
                    Random random = new Random();
                    int rand = random.Next(1, rowsCount);

                    report.LocateAndClickViewT(rand);

                    Thread.Sleep(3000);
                    IWebElement table = Setup.driver.FindElement(By.XPath("//div/div/table")); 
                    Setup.ScrollToElement(Setup.driver, table);
                    Setup.Highlight(table);
                    Thread.Sleep(3000);
                    // Initialize a list to store the converted dates
                    List<string> convertedDates = new List<string>();

                    // Iterate through the rows in the table
                    foreach (IWebElement row in table.FindElements(By.XPath("//tbody/tr")))
                    {
                        // Get the text from the fourth column
                        string dateText = row.FindElements(By.XPath("//td"))[3].Text;
                        Setup.ScrollToElement(Setup.driver, row);
                        Setup.Highlight(row);
                        Thread.Sleep(2000);

                        // Convert the date text to the desired format
                        DateTime date = DateTime.ParseExact(dateText, "MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        string formattedDate = date.ToString("MMddyyyy");

                        // Add the converted date to the list
                        convertedDates.Add(formattedDate);
                    }

                    // when the month from 1 to 9 add zero at the first
                    if (data.startDate?.Length == 7)
                    {
                        data.startDate = "0" + data.startDate;
                    }

                    if (data.EndDate?.Length == 7)
                    {

                        data.EndDate = "0" + data.EndDate;
                    }

                    DateTime startDate = DateTime.ParseExact(data.startDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(data.EndDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);

                    Thread.Sleep(2000);
                    // check the dates
                    foreach (string convertedDate in convertedDates)
                    {
                        DateTime currentDate = DateTime.ParseExact(convertedDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Assert.AreEqual(true, currentDate >= startDate && currentDate <= endDate, $"Date {convertedDate} is not within the specified range.");
                    }


                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
                catch (Exception ex)
                {
                    test.Fail(ex.Message);
                    test.Log(Status.Error, ex.Message);
                    //   add the screenshot to report
                    Thread.Sleep(2000);
                    string fullPath = Setup.TakeScreenShot();
                    test.AddScreenCaptureFromPath(fullPath);
                    test.Log(Status.Info, $"{user1.UserName} , {user1.Password} ");
                    Thread.Sleep(3000);
                    signin.LocateAndClickLogout();
                }
            }

        }

        //Verify that the user enters a valid category name , start date and end date then search results are displayed correctly.
        [TestMethod]
        public void SearchByNameAndDate()
        {

            Worksheet sheet = Setup.ReadExcel("TC10");
            for (int i = 1; i <= sheet.NotEmptyRowMax; i++)
            {
                var test = Setup.extentReports.CreateTest("SearchByName and date()",
                    "verify that the search results are displayed to the same category name");
                User user1 = new User();
                CategoryData data = new CategoryData();
                ProfilePage signin = new(user1);
                ReportPage report = new(data);

                // report = new(data);
                try
                {
                    //call StartSignIn Method to activate the driver and Url 
                    Setup.StartSignIn();
                    // give the user data from excel
                    user1.UserName = (string)sheet.Cell(i, 0).Value;
                    user1.Password = sheet.Cell(i, 1).Value.ToString();
                    data.Categoryname = (string)sheet.Cell(i, 2).Value;
                    data.startDate = sheet.Cell(i, 3).Value.ToString();
                    data.EndDate = sheet.Cell(i, 4).Value.ToString();
                    // Method for logging in system
                    signin.SignInData(user1);
                    test.Log(Status.Info, "user login successfuly");
                    //locate elements in update page
                    report.LocateAndClickReport();
                    report.LocateAndChooseCategoryRandom();
                    report.LocateAndEnterStartDate();
                    Thread.Sleep(2000);
                    report.LocateAndEnterEndDate();
                    test.Pass("Choose name ,start date and end date successfuly");
                    IWebElement reportsTable = Setup.driver.FindElement(By.XPath("//div/div/table"));
                    IList<IWebElement> reportsRows = reportsTable.FindElements(By.TagName("tbody"));
                    report.LocateAndClickView();

                    Thread.Sleep(3000);
                    IWebElement table = Setup.driver.FindElement(By.XPath("//div/div/table")); 
                    Setup.ScrollToElement(Setup.driver, table);
                    Setup.Highlight(table);
                    Thread.Sleep(3000);

                    // Initialize a list to store the converted dates
                    List<string> convertedDates = new List<string>();

                    // Iterate through the rows in the table
                    foreach (IWebElement row in table.FindElements(By.XPath("//tbody/tr")))
                    {
                        // Get the text from the fourth column
                        string dateText = row.FindElements(By.XPath("//td"))[3].Text;
                        Setup.ScrollToElement(Setup.driver, row);
                        Setup.Highlight(row);
                        Thread.Sleep(2000);
                        // Convert the date text to the desired format
                        DateTime date = DateTime.ParseExact(dateText, "MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        string formattedDate = date.ToString("MMddyyyy");
                        // Add the converted date to the list
                        convertedDates.Add(formattedDate);
                    }

                    // when the month from 1 to 9 add zero at the first
                    if (data.startDate?.Length == 7)
                    {
                        data.startDate = "0" + data.startDate;
                    }

                    if (data.EndDate?.Length == 7)
                    {
                        data.EndDate = "0" + data.EndDate;
                    }

                    DateTime startDate = DateTime.ParseExact(data.startDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(data.EndDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);

                    Thread.Sleep(2000);
                    // check the dates
                    foreach (string convertedDate in convertedDates)
                    {
                        DateTime currentDate = DateTime.ParseExact(convertedDate, "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Assert.AreEqual(true, currentDate >= startDate && currentDate <= endDate, $"Date {convertedDate} is not within the specified range.");
                    }



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



    }
}
