using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class projects
    {
        [JsonProperty("clientId")]
        public clientId clientId { get; set; }
        [JsonProperty("projectTypeId")]
        public projectTypeId projectTypeId { get; set; }
        [JsonProperty("projectId")]
        public projectId projectId { get; set; }

        public projects()
        {

        }
    }
}