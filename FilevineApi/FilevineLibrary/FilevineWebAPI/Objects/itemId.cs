using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class itemId
    {
        [JsonProperty("partner")]
        public string partner { get; set; }

        public itemId()
        {
            partner = "MichelTemp";
        }
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
