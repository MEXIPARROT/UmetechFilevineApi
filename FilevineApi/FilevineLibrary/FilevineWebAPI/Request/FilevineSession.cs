using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Request
{
    public class FilevineSession
    {
        public string mode { get; set; }
        public string apiKey { get; set; }
        public string apiHash { get; set; }
        public string apiTimestamp { get; set; }
        public string userId { get; set; }
        public string orgId { get; set; }


        public FilevineSession(FilevineSetting settings)
        {
            mode = "key";
            apiKey = settings.apiKey;

            apiTimestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ");
            string hashInput = $"{settings.apiKey}/{apiTimestamp}/{settings.apiSecret}";
            var hash = CreateMD5(hashInput);
            apiHash = hash;

            userId = settings.userId;
            orgId = settings.orgId;
        }

        public FilevineSession(string _mode, string _apiKey, string _apiSecret, string _userId, string _orgId)
        {
            mode = _mode;
            apiKey = _apiKey;

            apiTimestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ");
            string hashInput = $"{_apiKey}/{apiTimestamp}/{_apiSecret}";
            var hash = CreateMD5(hashInput);
            apiHash = hash;

            userId = _userId;
            orgId = _orgId;
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
