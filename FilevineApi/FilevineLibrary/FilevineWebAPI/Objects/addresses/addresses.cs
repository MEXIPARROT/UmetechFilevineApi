using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects.addresses
{
    public class addresses
    {
        [JsonProperty("adressesId")]
        public addressesId adressesId { set; get; }

        [JsonProperty("line1")]
        public string line1 { get; set; }

        [JsonProperty("line2")]
        public string line2 { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("postalCode")]
        public string postalCode { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

        [JsonProperty("fullAddress")]
        public string fullAddress { get; set; }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
