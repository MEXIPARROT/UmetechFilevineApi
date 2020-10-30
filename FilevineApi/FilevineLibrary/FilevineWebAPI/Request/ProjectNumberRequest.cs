using FilevineLibrary.FilevineWebAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FilevineLibrary.FilevineWebAPI.Response
{

    public class ProjectNumberRequest
    {
        [JsonProperty("number")]
        public string number { get; set; }
    }
}
