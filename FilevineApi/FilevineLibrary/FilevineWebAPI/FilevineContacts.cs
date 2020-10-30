using Filevine.PublicApi.Models;
using FilevineLibrary.FilevineWebAPI.Request;
using FilevineLibrary.FilevineWebAPI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace FilevineLibrary.FilevineWebAPI
{
    public class FilevineContacts
    {
        public FilevineWebClient webClient { get; set; }
        public FilevineContacts(FilevineWebClient _webClient)
        {
            webClient = _webClient;
        }

        public ContactResponse GetContactList()
        {
            var contactResponse = new ContactResponse();

            var res = webClient.GetRequest("core/contacts");// 21666108");//"subscriptions" //ken's account number???
            try
            {
                //Console.WriteLine("In_GetContactList");
                //Console.WriteLine(res);
                contactResponse = ContactResponse.FromJSON(res);
                //Console.WriteLine("DONE");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return contactResponse;
        }
        public ContactResponse GetContactList(string native)
        {
            var contactResponse = new ContactResponse();

            var res = webClient.GetRequest($"core/contacts/{native}");
            try
            {
                Console.WriteLine(res);
                contactResponse = ContactResponse.FromJSON(res);
                Console.WriteLine("===========================");
                //Console.WriteLine(temp.items.personId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return contactResponse;
        }

        public ContactResponse PostContact(ContactRequest request)//string fName, mName, lName, ssn, birthDate, gender)
        {
            var contactResponse = new ContactResponse();



            var res = webClient.PostRequest($"core/contacts", request.ToJSON());// post to directory with contact defined in the main.cs
            try
            {
                Console.WriteLine("In_PostContact");
                Console.WriteLine(res);
                contactResponse = ContactResponse.FromJSON(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return contactResponse;
        }
    }
}
