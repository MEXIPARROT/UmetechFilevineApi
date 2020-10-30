using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Response
{
    public class FilevineSessionResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string refreshTokenExpiry { get; set; }
        public string refreshTokenTzl { get; set; }
        public string userId { get; set; }
        public string orgId { get; set; }

        public FilevineSessionResponse(string _accessToken, string _refreshToken, string _refreshTokenExpiry, string _refreshTokenTzl, string _userId, string _orgId)
        {
            accessToken = _accessToken;
            refreshToken = _refreshToken;
            refreshTokenExpiry = _refreshTokenExpiry;
            refreshTokenTzl = _refreshTokenTzl;
            userId = _userId;
            orgId = _orgId;
        }

        public static FilevineSessionResponse FromString(string json)
        {
            return JsonConvert.DeserializeObject<FilevineSessionResponse>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
