using Automation.Framework.Core.WebUI.Params;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using CapaFramework.Config;
using LivingDoc.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Base;

namespace CapaFramework.Helpers
{
    public class ExtentReportsHelper 
    {

        public ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        DefaultVariables _defaultVariables;
        public ExtentTest test;
        
       /* private readonly TestSetUp _testSetUp;
        public ExtentReportsHelper(TestSetUp testSetUp)
        {
            _testSetUp = testSetUp;
        }*/
        public void ExtentReportInIt() {

            extent = new ExtentReports();
            _defaultVariables = new DefaultVariables();
            Loggers.Write("Initializing Extent Reports");
            /*String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "\\index.html";*/
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory + "../../../").FullName;
            String reportPath = projectDirectory + @"\Reports\report" + DateTime.Now + ".html";
            htmlReporter = new ExtentHtmlReporter(_defaultVariables.ExtentReport);
            Loggers.Write("Reports File Path - " + reportPath);

            htmlReporter.Config.ReportName = "Automaton Test Results";
            htmlReporter.Config.DocumentTitle = "Automation Testing";
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Start();
            Loggers.Write("Reports File Path - " + reportPath);

            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Browser", ConfigReader.Browser);
            extent.AddSystemInfo("OS", "Windows");
            Loggers.Write("Reports File Initilized");
        }

        public void ExtentReportsTearDown()
        {
            Loggers.Write("Closing Extent Report");
            extent.Flush();
        }



    }
}
