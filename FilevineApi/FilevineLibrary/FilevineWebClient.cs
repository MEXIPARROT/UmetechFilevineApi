//using Database.Contracts;
using FilevineLibrary.FilevineWebAPI.Request;
using FilevineLibrary.FilevineWebAPI.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TestingConsole.FilevineWebAPI.Response;

namespace FilevineLibrary
{
    public class FilevineWebClient
    {
        public string baseURL { get; set; }
        public FilevineSetting settings { get; set; }
        public FilevineSession session { get; set; }
        public FilevineSessionResponse token { get; set; }

        public FilevineWebClient(FilevineSetting _settings)
        {
            baseURL = "https://api.filevine.io/";
            settings = _settings;
            RefreshToken();
        }

        public void RefreshToken()
        {
            try
            {
                session = new FilevineSession(settings);
                var res = FilevineLibrary.FilevineWebAPI.APICaller.PostRequest(baseURL + "session", session.ToJson());
                token = FilevineSessionResponse.FromString(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string PostRequest(string url, string json, Dictionary<string, string> addHeaders = null)
        {
            RefreshToken();

            var headers = new Dictionary<string, string>();
            if (addHeaders != null)
            {
                foreach (var header in addHeaders)
                {
                    headers.Add(header.Key, header.Value);
                }
            }

            headers.Add("Authorization", $"Bearer {token.accessToken}");
            headers.Add("x-fv-sessionid", token.refreshToken);

            var res = FilevineLibrary.FilevineWebAPI.APICaller.PostRequest(baseURL + url, json, headers);
            return res;
        }

        public string GetRequest(string url, Dictionary<string, string> addHeaders = null)
        {
            RefreshToken();

            var headers = new Dictionary<string, string>();
            if (addHeaders != null)
            {
                foreach (var header in addHeaders)
                {
                    headers.Add(header.Key, header.Value);
                }
            }

            headers.Add("Authorization", $"Bearer {token.accessToken}");
            headers.Add("x-fv-sessionid", token.refreshToken);

            var res = FilevineLibrary.FilevineWebAPI.APICaller.GetRequest(baseURL + url, headers);
            return res;
        }

        public string DeleteRequest(string url, Dictionary<string, string> addHeaders = null)
        {
            RefreshToken();

            var headers = new Dictionary<string, string>();
            if (addHeaders != null)
            {
                foreach (var header in addHeaders)
                {
                    headers.Add(header.Key, header.Value);
                }
            }

            headers.Add("Authorization", $"Bearer {token.accessToken}");
            headers.Add("x-fv-sessionid", token.refreshToken);

            var res = FilevineLibrary.FilevineWebAPI.APICaller.DeleteRequest(baseURL + url, headers);
            return res;
        }

        //mine since there is no update
        public string UpdateRequest(string url, string json, Dictionary<string, string> addHeaders = null)
        {
            RefreshToken();

            var headers = new Dictionary<string, string>();
            if (addHeaders != null)
            {
                foreach (var header in addHeaders)
                {
                    headers.Add(header.Key, header.Value);
                }
            }

            headers.Add("Authorization", $"Bearer {token.accessToken}");
            headers.Add("x-fv-sessionid", token.refreshToken);

            var res = FilevineLibrary.FilevineWebAPI.APICaller.PatchRequest(baseURL + url, json, headers);
            return res;
        }

        public string PutRequest(string url, string json, Dictionary<string, string> addHeaders = null)
        {
            RefreshToken();

            var headers = new Dictionary<string, string>();
            if (addHeaders != null)
            {
                foreach (var header in addHeaders)
                {
                    headers.Add(header.Key, header.Value);
                }
            }

            headers.Add("Authorization", $"Bearer {token.accessToken}");
            headers.Add("x-fv-sessionid", token.refreshToken);

            var res = FilevineLibrary.FilevineWebAPI.APICaller.PutRequest(baseURL + url, json, headers);
            return res;
        }
    }
}
