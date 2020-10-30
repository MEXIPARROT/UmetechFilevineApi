using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects.emails
{
    public class emails
    {
        [JsonProperty("emailId")]
        public emailId emailId { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
