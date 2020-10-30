using FilevineLibrary.FilevineWebAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Response
{
    public class ProjectTypeResponse
    {
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
        ////
        //do these get stored???
        [JsonProperty("expense")]
        public dynamic expense { get; set; } //originally expense class

        [JsonProperty("amountdue")]
        public double amountdue { get; set; }

        [JsonProperty("data")]
        public DateTime date { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("notes")]
        public string notes { get; set; }

        [JsonProperty("datepaid")]
        public DateTime datepaid { get; set; }

        [JsonProperty("re")]
        public string re { get; set; }

        [JsonProperty("payee")]
        public string payee { get; set; }
        ///
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("itemId")]
        public itemId itemId { get; set; }

        [JsonProperty("dataObject")]
        public Expense DataObject { get; set; } //call it dataobject but actually expense class




        public static ProjectTypeResponse FromJSON(string json)
        {
            ProjectTypeResponse temp = JsonConvert.DeserializeObject<ProjectTypeResponse>(json);
            //Console.WriteLine(temp.count);
            return temp;
        }

        public static List<ProjectTypeResponse> FromJSONArray(string json)
        {
            List<ProjectTypeResponse> temp = JsonConvert.DeserializeObject<List<ProjectTypeResponse>>(json);
            //Console.WriteLine("-----\n");
            //Console.WriteLine(temp[1].items.);
            //Console.WriteLine("-----\n");
            return temp;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
