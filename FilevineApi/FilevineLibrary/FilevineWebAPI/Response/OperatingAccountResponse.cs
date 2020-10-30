using FilevineLibrary.FilevineWebAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Profile;

namespace FilevineLibrary.FilevineWebAPI.Response
{
    public class OperatingAccountResponse
    {
        [JsonProperty("date")]
        public DateTime date { get; set; }

        [JsonProperty("entry")] //originally "amount"
        public int entry { get; set; } //amount

        [JsonProperty("checkNumber")]
        public string checkNumber { get; set; }

        [JsonProperty("invoice")]
        public int invoice { get; set; }//might be long

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("ExplCode")]
        public string ExplCode { get; set; }

        [JsonProperty("explanation")]
        public string explanation { get; set; }

        [JsonProperty("rcptsAmount")]
        public double rcptsAmount { get; set; }

        [JsonProperty("disbsAmount")]
        public double disbsAmount { get; set; }

        ///FILEVINE'S GET PROJECT COLLECTION ITEM LIST RESPONSE!!! NEEDED FOR ALL RESPONSES LIKE THIS https://developer.filevine.io/v2/collection-sections/get-project-collection-item-list
        [JsonProperty("count")]
        public int count { get; set; }

        [JsonProperty("offset")]
        public int offset { get; set; }

        [JsonProperty("limit")]
        public int limit { get; set; }

        [JsonProperty("hasMore")]
        public bool hasMore { get; set; }

        [JsonProperty("requestedFields")]
        public string requestedFields { get; set; }

        [JsonProperty("links")]
        public link links { get; set; }

        [JsonProperty("items")]
        public List<items> items { get; set; }

        //FILEVINE FOR CHECKING IF POSTED
        [JsonProperty("itemId")]
        public itemId itemId { get; set; }

        [JsonProperty("dataObject")]
        public dataObject dataObject { get; set; }

        public static OperatingAccountResponse FromJSON(string json)
        {
            OperatingAccountResponse temp = JsonConvert.DeserializeObject<OperatingAccountResponse>(json);
            //Console.WriteLine(temp.count);
            return temp;
        }
        public static List<OperatingAccountResponse> FromJSONArray(string json)
        {
            List<OperatingAccountResponse> temp = JsonConvert.DeserializeObject<List<OperatingAccountResponse>>(json);
            //Console.WriteLine("-----\n");
            //Console.WriteLine(temp[1].entry);
            //Console.WriteLine("-----\n");
            return temp;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
