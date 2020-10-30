using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class link
    {
        [JsonProperty("self")]
        public string self { get; set; }

        [JsonProperty("prev")]
        public string prev { get; set; }

        [JsonProperty("next")]
        public string next { get; set; }

        public link()
        {
            self = "";
            prev = "";
            next = "";
        }
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
