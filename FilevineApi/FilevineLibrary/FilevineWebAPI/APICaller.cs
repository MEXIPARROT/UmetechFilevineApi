using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI
{
    public class APICaller
    {

        public static string GetRequest(string url, Dictionary<string, string> headers = null)
        {
            return EmptyPayloadRequest("GET", url, headers);
        }

        public static string DeleteRequest(string url, Dictionary<string, string> headers = null)
        {
            return EmptyPayloadRequest("DELETE", url, headers);
        }

        public static string PostRequest(string url, string json, Dictionary<string, string> headers = null)
        {
            return JSONRequest("POST", url, json, headers);
        }

        public static string PatchRequest(string url, string json, Dictionary<string, string> headers = null)
        {
            return JSONRequest("PATCH", url, json, headers);
        }

        public static string PutRequest(string url, string json, Dictionary<string, string> headers = null)
        {
            return JSONRequest("PUT", url, json, headers);
        }

        private static string EmptyPayloadRequest(string method, string url, Dictionary<string, string> headers = null)
        {
            string res = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                res = reader.ReadToEnd();
            }

            return res;
        }

        private static string JSONRequest(string method, string url, string json, Dictionary<string, string> headers = null)
        {
            string result = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = method;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    if (httpWebRequest.HaveResponse && response != null)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            result = error;
                        }
                    }

                }
            }

            return result;
        }
    }
}
