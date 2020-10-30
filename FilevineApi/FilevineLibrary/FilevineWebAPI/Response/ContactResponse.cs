using Filevine.PublicApi.Models;
using FilevineLibrary.FilevineWebAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Response
{
    public class ContactResponse
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

        public ContactResponse()
        {
            count = 0;
            offset = 0;
            limit = 0;
            hasMore = false;
            requestedFields = "";
        }

        public static ContactResponse FromJSON(string json)
        {
            ContactResponse temp = JsonConvert.DeserializeObject<ContactResponse>(json);
            Console.WriteLine(temp.count);
            return temp;
        }

        public static List<ContactResponse> FromJSONArray(string json)
        {
            List<ContactResponse> temp = JsonConvert.DeserializeObject<List<ContactResponse>>(json);
            Console.WriteLine("-----\n");
            //Console.WriteLine(temp[1].items.);
            Console.WriteLine("-----\n");
            return temp;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
