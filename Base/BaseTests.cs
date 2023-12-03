using AventStack.ExtentReports;
using CapaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject.Base
{
    public class BaseTests
    {
        public IWebDriver Driver;
        
        bool IsHeadlessBrowser = Convert.ToBoolean(ConfigReader.HeadlessBrowser);

        public void InitializeBrowser(string browserName)
        {

            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AcceptInsecureCertificates = true;
                    if (IsHeadlessBrowser)
                    {
                        chromeOptions.AddArguments("headless");
                    }
                    Driver = new ChromeDriver(chromeOptions);
                    break;

                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AcceptInsecureCertificates = true;
                    if (IsHeadlessBrowser)
                    {
                        firefoxOptions.AddArguments("headless");
                    }
                    Driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AcceptInsecureCertificates = true;
                    if (IsHeadlessBrowser)
                    {
                        edgeOptions.AddArguments("headless");
                    }
                    Driver = new EdgeDriver(edgeOptions);
                    break;
            }
            Driver.Manage().Window.Maximize();
            /*driver.Manage().Timeouts().ImplicitWait;*/
        }

        public void LaunchURL(string url)
        {
            Driver.Url = url;
        }

        public MediaEntityModelProvider CaptureScreenshotAndReturnModel(string Name)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
            }
            catch
            {

                return null;
            }
        }

    }
}
