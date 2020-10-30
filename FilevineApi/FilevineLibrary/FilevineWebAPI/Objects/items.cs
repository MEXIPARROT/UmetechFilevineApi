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
    public class items
    {
        [JsonProperty("sectionSelector")]
        public string sectionSelector { get; set; }

        [JsonProperty("personId")]
        public personId personId { get; set; }
        
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("middleName")]
        public string middleName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("isSingleName")]
        public bool isSingleName { get; set; }

        [JsonProperty("fullName")]
        public string fullName { get; set; }

        [JsonProperty("ssn")]
        public string ssn { get; set; }

        [JsonProperty("birthDate")]
        public string birthDate { get; set; }

        [JsonProperty("notes")]
        public string notes { get; set; }

        [JsonProperty("fromCompany")]
        public string fromCompany { get; set; }

        [JsonProperty("specialty")]
        public string specialty { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("maritalStatus")]
        public string maritalStatus { get; set; }

        [JsonProperty("isTextingPermitted")]
        public bool isTextingPermitted { get; set; }

        [JsonProperty("remarket")]
        public bool remarket { get; set; }

        [JsonProperty("abbreviatedName")]
        public string abbreviatedName { get; set; }

        [JsonProperty("driverLicenceNumber")]
        public string driverLicenceNumber { get; set; }

        [JsonProperty("personTypes")]
        public List<string> personTypes { get; set; } //previously string[] but filesync example states such like vector: List<string>

        [JsonProperty("salutation")]
        public string salutation { get; set; }

        [JsonProperty("barNumber")]
        public string barNumber { get; set; }

        [JsonProperty("fiduciary")]
        public string fiduciary { get; set; }

        [JsonProperty("isMinor")]
        public bool isMinor { get; set; }

        [JsonProperty("phones")]
        public List<phones.phones> phones { get; set; }

        [JsonProperty("emails")]
        public List<emails.emails> emails  { get; set; }

        [JsonProperty("address")]
        public List<addresses.addresses> address { get; set; }

        [JsonProperty("primaryEmail")]
        public string primaryEmail { get; set; }

        [JsonProperty("pictureUrl")]
        public string pictureUrl { get; set; }

        [JsonProperty("pictureKey")]
        public string pictureKey { get; set; }

        [JsonProperty("uniqueID")]
        public string uniqueID { get; set; }
                
        [JsonProperty("searchNames")]
        public List<string> searchNames { get; set; }

        [JsonProperty("itemId")]
        public itemId itemId { get; set; }

        [JsonProperty("dataObject")]
        public dataObject dataObject { get; set; }

        [JsonProperty("links")]
        public link links { get; set; }

        public items()
        {
            //personId
            ///firstName = "Temporary";
            //middleName = "Not Real";
            //lastName = "Rodriguez";
            //isSingleName = true;
            //fullName = "Temporary ProbablyReal Rodriguez";
            //ssn = "000 01 2345";
            //birthDate = "September 14, 2015";
        }
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
