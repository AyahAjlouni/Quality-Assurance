using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Bytescout.Spreadsheet;

namespace AutomationTestingTask
{
    //class contain all members needed in other classes
    public class Setup
    {
        public static ExtentReports extentReports = new ExtentReports(); //generate report
        public static ExtentHtmlReporter reporter = new ExtentHtmlReporter("C:\\Users\\user\\Desktop\\Automation reports\\Taskreport\\");

        public static IWebDriver driver = new ChromeDriver();
        public static string url = "https://courses.ultimateqa.com/";  

        //Method to open browser with Maximum size
        public static void OpenDriver()
        {
            driver.Manage().Window.Maximize();
        }

        //Method to visit the url
        public static void NavigateToUrl() { 

            driver.Navigate().GoToUrl(url);
        }
        //Method to Close browser 
        public static void CloseDriver()
        {
            driver.Close();
        }
        //Method to highlight the elemnts that Found it 
        public static void Highlight(IWebElement element)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver; //add js code on html elements
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style' , 'background:pink !important')", element);//Convert the background of elements to pink
            Thread.Sleep(1000);
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style' , 'background:none !important')", element); //Restore the background color
            Thread.Sleep(1000);
        }
        
        //Method to Scroll page to the element that i want 
        public static void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver;
            scriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }


        // Method to check if email matches the specified pattern using a regular expression.
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }


        //Method that test the validation of emails 
        public static void Testemailvalidation(User user)
        {

            //find the xpath of email element 
            string emailinput = user.Email;
            IWebElement email = Setup.driver.FindElement(By.XPath("//div/input[@id='user[email]']"));
            // Check if the user-entered email address is valid using regular expressions
            if (Setup.IsValidEmail(emailinput))
            {
                SignInPage signinPage = new(user);
                signinPage.LocateAndEnterEmail();
            }
            else
            {
                IWebElement emailerror = Setup.driver.FindElement(By.XPath("//div/p[@id='user[email]-error']"));
                string text = emailerror.GetAttribute("innerText");
                Assert.AreEqual(text, "Please enter a valid email address", "Please enter a valid email address");
            }
        }


        //Method to call the driver and Url methods
        public static void StartSignIn()
        {
            //OpenDriver();
            NavigateToUrl();
            Thread.Sleep(2000);
        }


        public static string TakeScreenShot()
        {
            //add screenshot to the file 
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            string path = "C:\\Users\\user\\Desktop\\Automation reports\\images\\";
            string imageName = Guid.NewGuid().ToString() + "_image.png";//C# method to initialize characters
            string fullPath = Path.Combine(path + $"\\{imageName}");
            screenshot.SaveAsFile(fullPath);
            return fullPath;
        }
        //Method to read excel sheet that have users data 

        public static Worksheet ReadExcel(string sheetName)
        {
            Spreadsheet Excel = new Spreadsheet();
            Excel.LoadFromFile("C:\\Users\\user\\Desktop\\tasktestcases.xlsx");
            Worksheet sheet = Excel.Workbook.Worksheets.ByName(sheetName);
            return sheet;
        }
    }
}
