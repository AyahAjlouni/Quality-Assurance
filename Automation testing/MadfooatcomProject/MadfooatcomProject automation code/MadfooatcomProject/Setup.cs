using AventStack.ExtentReports;
using Bytescout.Spreadsheet;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;


namespace MadfooatcomProject
{
    public class Setup
    {
        public static ExtentReports extentReports = new ExtentReports(); //generate report
        public static ExtentHtmlReporter reporter = new ExtentHtmlReporter("D:\\OneDrive-9-12-2023\\Automation\\Automation reports\\Report\\");


        public static IWebDriver driver = new ChromeDriver();
        public static string url = "http://localhost:4200/"; 

        //Method to open browser with Maximum size
        public static void OpenDriver()
        {
            driver.Manage().Window.Maximize();
        }

        //Method to visit the url
        public static void NavigateToUrl()
        {

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
            string path = "D:\\OneDrive-9-12-2023\\Automation\\Automation reports\\images\\";
            string imageName = Guid.NewGuid().ToString() + "_image.png";//C# method to initialize characters
            string fullPath = Path.Combine(path + $"\\{imageName}");
            screenshot.SaveAsFile(fullPath);
            return fullPath;
        }

        public static Worksheet ReadExcel(string sheetName)
        {
            Spreadsheet Excel = new Spreadsheet();
            Excel.LoadFromFile("D:\\OneDrive-9-12-2023\\Automation\\Madfooatcomtestcases.xlsx");
            Worksheet sheet = Excel.Workbook.Worksheets.ByName(sheetName);
            return sheet;
        }
    }
}
