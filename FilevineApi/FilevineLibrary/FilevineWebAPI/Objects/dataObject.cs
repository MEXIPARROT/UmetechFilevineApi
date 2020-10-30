using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class dataObject
    {
        [JsonProperty("expense")]
        public Expense Expense { get; set; }

        [JsonProperty("fieldselector")]
        public string fieldSelector { get; set; } //may be string, object array, number, int, or bool
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
