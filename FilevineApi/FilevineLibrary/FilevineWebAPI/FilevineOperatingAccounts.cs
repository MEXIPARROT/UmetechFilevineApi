using Filevine.PublicApi.Models;
using FilevineLibrary.FilevineWebAPI.Request;
using FilevineLibrary.FilevineWebAPI.Response;
using PCLawData.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI
{
    public class FilevineOperatingAccounts
    {
        public FilevineWebClient webClient { get; set; }

        public FilevineOperatingAccounts(FilevineWebClient _webClient)
        {
            webClient = _webClient;
        }
        
        public OperatingAccountResponse GetOperatingAccountItemList()
        {
            var projectId = "7673878"; //7399078 used to be because of expenses
            var OperatingAccountResponse = new OperatingAccountResponse();// webClient);
            var sectionSelector = "operatingAccounts"; 
            var res = webClient.GetRequest($"core/projects/{projectId}/collections/{sectionSelector}");
            try
            {
                //Console.WriteLine("GET_Operating_AccountITEM_LIST");
                //Console.WriteLine(res);
                OperatingAccountResponse = OperatingAccountResponse.FromJSON(res);
            }
            catch (Exception ex)
            {
                Logging.Report(ex.ToString(), -1);
            }
            return OperatingAccountResponse;
        }

        public OperatingAccountResponse PostCollectionItem(long projectID, OperatingAccountRequest request, string collectionPartnerID)//Expense request)//string fName, mName, lName, ssn, birthDate, gender)
        {
            var operatingAccountResponse = new OperatingAccountResponse();
            var projectId = projectID;//proj num
            var sectionSelector = "operatingAccounts";
            request.itemId.partner = collectionPartnerID;

            var res = webClient.PostRequest($"core/projects/{projectId}/collections/{sectionSelector}/", request.ToJSON());//7229227 my project "@"post by partnerID
            try
            {
                operatingAccountResponse = OperatingAccountResponse.FromJSON(res);
            }
            catch (Exception ex)
            {
                Logging.Report(ex.ToString(), -1);
            }
            return operatingAccountResponse;
        }

        public OperatingAccountResponse PatchCollectionItem(long projectID, OperatingAccountRequest request, string collectionPartnerID)//Expense request)//string fName, mName, lName, ssn, birthDate, gender)
        {
            var operatingAccountResponse = new OperatingAccountResponse();
            var projectId = projectID;//proj num
            var sectionSelector = "operatingAccounts";
            request.itemId.partner = collectionPartnerID;

            var res = webClient.UpdateRequest($"core/projects/{projectId}/collections/{sectionSelector}/@{collectionPartnerID}", request.ToJSON());//7229227 my project "@"post by partnerID
            try
            {
               //Console.WriteLine("INPATCH");
               //Console.WriteLine($"core/projects/{projectId}/collections/{sectionSelector}/@{collectionPartnerID}", request.ToJSON());
                operatingAccountResponse = OperatingAccountResponse.FromJSON(res);
            }
            catch (Exception ex)
            {
                Logging.Report(ex.ToString(), -1);
            }
            return operatingAccountResponse;
        }
    }
}
