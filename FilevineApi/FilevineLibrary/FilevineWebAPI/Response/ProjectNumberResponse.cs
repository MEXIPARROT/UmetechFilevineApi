using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilevineLibrary.FilevineWebAPI.Objects;

namespace FilevineLibrary.FilevineWebAPI.Response
{
    public class ProjectNumberResponse
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
        public List<projects> items { get; set; }

        public ProjectNumberResponse()
        {
            count = 0;
            offset = 0;
            limit = 0;
            hasMore = false;
            requestedFields = "";
            items = new List<projects>();
        }

        public static ProjectNumberResponse FromJSON(string json)
        {
            var response = JsonConvert.DeserializeObject<ProjectNumberResponse>(json);
            return response;
        }

        public static List<ProjectNumberResponse> FromJSONArray(string json)
        {
            List<ProjectNumberResponse> response = JsonConvert.DeserializeObject<List<ProjectNumberResponse>>(json);
            return response;
        }
    }
}