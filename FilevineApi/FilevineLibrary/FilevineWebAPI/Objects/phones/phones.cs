using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects.phones
{
    public class phones
    {
        [JsonProperty("phoneId")]
        public phoneId phoneId { get; set; }

        [JsonProperty("number")]
        public string number { get; set; }

        [JsonProperty("rawNumber")]
        public string rawNumber { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
