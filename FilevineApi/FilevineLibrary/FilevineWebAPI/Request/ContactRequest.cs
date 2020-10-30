using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using Filevine.PublicApi.Models;
using Newtonsoft.Json;

namespace FilevineLibrary.FilevineWebAPI.Request
{
    public class ContactRequest
    {
        [JsonProperty("requestedFields")]
        public string requestedFields { get; set; }

        [JsonProperty("offset")]
        public int offset { get; set; }

        [JsonProperty("limit")]
        public int limit { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("fullName")]
        public string fullName { get; set; }

        [JsonProperty("nickName")]
        public string nickName { get; set; }

        [JsonProperty("personType")]
        public string personType { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        public ContactRequest(string _requestedFields, int _offset, int _limit, string _firstName, string _lastName, string _fullName, string _nickName, string _personType, string _phone, string _email)
        {
            requestedFields = _requestedFields;
            offset = _offset;
            limit = _limit;
            firstName = _firstName;
            lastName = _lastName;
            fullName = _fullName;
            nickName = _nickName;
            personType = _personType;
            phone = _phone;
            email = _email;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
