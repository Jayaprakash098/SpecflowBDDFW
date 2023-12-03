using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Pages
{
    public class ResultPage
    {
        private IWebDriver driver;

        public ResultPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By channelName = By.LinkText("GAVS");

        public ChannelPage clickOnChannel()
        {
            driver.FindElement(channelName).Click();
            return new ChannelPage(driver);
        }
    }
}
