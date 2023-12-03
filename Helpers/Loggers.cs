using Automation.Framework.Core.WebUI.Params;
using CapaFramework.Config;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CapaFramework.Helpers
{
    public static class Loggers
    {

        public static StreamWriter streamw = null;
        private static string logFileName = $"CapaNextGen_{DateTime.Now:yyyyMMdd_HHmmss}";

        public static void CreateLogFile()
        {
            //DefaultVariables defaultVariables = new DefaultVariables();
            //string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            //string logFileName = $"CapaNextGen_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            //string logFilePath = Path.Combine(dir, logFileName);
            

            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string dir = projectDirectory +"\\Log\\";

            if (Directory.Exists(dir))
            {

                streamw = File.AppendText(dir + logFileName+".log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                streamw = File.AppendText(dir + logFileName + ".log");
                
            }


        }

        public static void Write(string logMessage)
        {
            CreateLogFile();
            
            streamw.Write($"{ DateTime.Now} :   " + logMessage + Environment.NewLine);
            streamw.Flush();
            streamw.Close();
        }

       
    }
}
