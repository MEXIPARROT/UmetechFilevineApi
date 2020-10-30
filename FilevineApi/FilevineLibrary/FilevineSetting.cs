using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary
{
    public class FilevineSetting
    {
        public string apiURL { get; set; }
        public string apiKey { get; set; }
        public string apiSecret { get; set; }
        public string userId { get; set; }
        public string orgId { get; set; }

        public FilevineSetting(string _apiURL, string _apiKey, string _apiSecret)
        {
            apiURL = _apiURL;
            apiKey = _apiKey;
            apiSecret = _apiSecret;
            userId = "0";
            orgId = "0";
        }
    }
}
