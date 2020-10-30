using System;
using FilevineSDK;

namespace FilevineLibrary
{
    public class Connection : IDisposable
    {
        public FilevineSDK.Session Session { get; private set; }
        public FilevineSDK.SDKObjects.Token Token { get; private set; }

        public Connection(FilevineSetting settings)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            Session = new Session(settings.apiURL);
            Session.CreateApiSession(settings.apiKey, settings.apiSecret, settings.userId, settings.orgId);
            Token = Session.ApiToken;
        }

        public void Dispose()
        {

        }
    }
}
