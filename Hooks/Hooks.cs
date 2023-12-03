using Automation.Framework.Core.WebUI.Params;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using TechTalk.SpecFlow;
using TestProject.Base;
using CapaFramework.Helpers;
using CapaFramework.Config;

namespace TestProject.Hooks
{
    
    [Binding]
    public sealed class Hooks  : BaseTests
    {
        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentReportsHelper _extentReports;
        private readonly IObjectContainer _container;
        private static readonly object _driverLock = new object();

        public Hooks(IObjectContainer container)
        {
            
            _container = container;            

        }


        [BeforeTestRun]
        public static void BeforeTestRun(ConfigReader configReader)
        {
            
            configReader.SetFrameworkSettings();
            DefaultVariables d = new DefaultVariables();
            Loggers.Write(d.Results);
            //_ilogging.SetLogLevel("Debug");
            Loggers.Write("Inside Before Test Run"+ConfigReader.URL);
            
            //_ilogging.Debug("Running before test run exec...");
            /*var htmlRep = new ExtentHtmlReporter("C:\\Users\\JP\\source\\repos\\TestProject\\Reports\\index.html");
            htmlRep.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlRep);*/
            _extentReports =  new ExtentReportsHelper();
            _extentReports.ExtentReportInIt();

        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
           
            Loggers.Write("Running after test run...");
            
            _extentReports.ExtentReportsTearDown();
            
        }
        
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Loggers.Write("Running before feature...");
            //_ilogging.Debug("Running before test run debug...");
            
            _feature = _extentReports.extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            Loggers.Write("Before Feature executed");
            //_extentReports.CreateTest(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {

            Loggers.Write("Running after feature...");
        }

        [BeforeScenario(Order =1)]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Loggers.Write("Into Before Scenario");
            lock (_driverLock)
            {
                if (Driver == null)
                {
                    InitializeBrowser(ConfigReader.Browser);
                    _container.RegisterInstanceAs<IWebDriver>(Driver);
                }
            }
            
            LaunchURL(ConfigReader.URL);
            Thread.Sleep(10);
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            Loggers.Write("Running before Scenario..."+ scenarioContext.ScenarioInfo.Title);

        }

        [AfterScenario]
        public void AfterScenario()
        {
            var Driver = _container.Resolve<IWebDriver>();
            lock (_driverLock)
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Loggers.Write("Driver closed");
                }
            }

        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            string stepName = ScenarioStepContext.Current.StepInfo.Text;
            var mediaEntity = CaptureScreenshotAndReturnModel(scenarioContext.ScenarioInfo.Title.Trim());
            Loggers.Write("Running after test step..."+stepName);
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    //_extentReports.SetStepStatusPass("Given - " + stepName);
                    _scenario.CreateNode<Given>(stepName).Pass("", mediaEntity);
                }
                else if (stepType == "When")
                {
                    //_extentReports.SetStepStatusPass("When - " + stepName);
                    _scenario.CreateNode<When>(stepName).Pass("", mediaEntity);
                }
                else if (stepType == "Then")
                {
                    //_extentReports.SetStepStatusPass("Then - " + stepName);
                    _scenario.CreateNode<Then>(stepName).Pass("", mediaEntity);
                }
                else if (stepType == "And")
                {
                    //_extentReports.SetStepStatusPass("And - " + stepName);
                    _scenario.CreateNode<And>(stepName).Pass("", mediaEntity);
                }
            }
            else if (scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    //_extentReports.SetStepStatusFail("Given - " + stepName);
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "When")
                {
                    //_extentReports.SetStepStatusFail("When - " + stepName);
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "Then")
                {
                    //_extentReports.SetStepStatusFail("Then - " + stepName);
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "And")
                {
                    //_extentReports.SetStepStatusFail("And - " + stepName);
                    _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message, mediaEntity);
                }
            }


        }
    }
}