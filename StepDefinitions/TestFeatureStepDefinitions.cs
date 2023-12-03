using CapaFramework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TestProject.Pages;

namespace TestProject.StepDefinitions
{
    [Binding]
    public class TestFeatureStepDefinitions
    {
        private IWebDriver driver;
        SearchPage searchPage;
        ResultPage resultPage;
        ChannelPage channelPage;

        private TestFeatureStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [When(@"Search gavs tech in youtube")]
        public void SearchInYoutube()
        {
            searchPage = new SearchPage(driver);
            Loggers.Write("Step def file");
            resultPage = searchPage.searchText("Gavs technologies");
            Thread.Sleep(4000);
        }

        [When(@"Navigate to channel")]
        public void NavigateToChannel()
        {
            channelPage = resultPage.clickOnChannel();
            Loggers.Write("Clicked Channel ");
            Thread.Sleep(4000);
        }


        [Then(@"Verify title of the page ""([^""]*)""")]
        public void ThenVerifyTitleOfThePage(string ExpectedTitle)
        {
            Assert.AreEqual(ExpectedTitle, channelPage.getTitle());
            Loggers.Write("Retrived Title ");

        }

    }
}
