using CapaFramework.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace CapaFramework.Config
{
    public class ConfigReader
    {

        public static String Browser { get; set; }
        public static String URL { get; set; }
        public static String Username { get; set; }
        public static String Password { get; set; }
        public static String HeadlessBrowser { get; set; }

        
        public void SetFrameworkSettings()
        {


            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            var builder = new ConfigurationBuilder().SetBasePath(projectDirectory)
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configRoot = builder.Build();

            Browser = configRoot.GetSection("testSettings").Get<TestSettings>().Browser;
            URL = configRoot.GetSection("testSettings").Get<TestSettings>().URL;
            Username = configRoot.GetSection("testSettings").Get<TestSettings>().Username;
            Password = configRoot.GetSection("testSettings").Get<TestSettings>().Password;
            HeadlessBrowser = configRoot.GetSection("testSettings").Get<TestSettings>().HeadlessBrowser;

            Loggers.Write("Config file is loaded");

        }
    }
}
