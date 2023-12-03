using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaFramework.Config
{
    [JsonObject("testSettings")]
    class TestSettings
    {

        [JsonProperty("browser")]
        public string Browser { get; set; }

        [JsonProperty("URL")]
        public string URL { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("HeadlessBrowser")]
        public string HeadlessBrowser { get; set; }
    }
}
