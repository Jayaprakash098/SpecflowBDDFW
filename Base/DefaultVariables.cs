using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Core.WebUI.Params
{
    public class DefaultVariables 
    {
        public string Results
        {
            get
            {
                return System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory + "../../../").FullName + @"\Tests Reports\Reports"
                     + DateTime.Now.ToString("yyyyMMdd hhmmss");
            }
        }
        public string Log { get
            {
                return Results + "\\log.txt";
            } 
        }

        public string ExtentReport
        {
            get
            {
                return Results + "\\result.html";
            }
        }


    }
}
