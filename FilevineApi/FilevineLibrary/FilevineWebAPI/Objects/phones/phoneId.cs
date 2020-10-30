using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects.phones
{
    public class phoneId
    {
        [JsonProperty("native")]
        public int native { get; set; }

        [JsonProperty("partner")]
        public string partner { get; set; }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
