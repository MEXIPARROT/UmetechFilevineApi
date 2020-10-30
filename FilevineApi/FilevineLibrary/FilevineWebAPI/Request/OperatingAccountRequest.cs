using FilevineLibrary.FilevineWebAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Request
{
    public class OperatingAccountRequest
    {
        [JsonProperty("itemId")]
        public itemId itemId { get; set; }

        [JsonProperty("dataObject")]
        public OperatingAccount dataObject { get; set; } //used to be expense object

        public OperatingAccountRequest()
        {
            //expense.amountdue = 0.00;
            //expense.date =
            //basically define expense how it is to be expected? seems it prefers to not be defined
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
