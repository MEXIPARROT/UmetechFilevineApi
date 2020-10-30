using FilevineLibrary;
using FilevineLibrary.FilevineWebAPI;
using FilevineLibrary.FilevineWebAPI.Objects;
using FilevineLibrary.FilevineWebAPI.Request;
using FilevineLibrary.FilevineWebAPI.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PCLawData;
using PCLawData.Contracts;
using PCLawData.Operations;
using System.Configuration;
using PCLawData.Logging;
using System.Reflection.Emit;
using System.Data.Odbc;
using System.Data;
using System.Threading;

namespace FilveineApiConnection
{

    class TestConsole
    {
        //DO KNOW THAT THIS PROGRAM WORKS ONLY IN THE REMOTE YARBROUGH COMPUTER, SO IF IT'S RAN FROM YOUR PERSONAL PC IT WON'T WORK!!!!
        //MOST COMMENTS LEFT IN THIS PROGRAM ARE LEFT BECAUSE I WOULD RECOMMEND PRINTING DATA TO UNDERSTAND WHERE IT'S GOING
        static void Main(string[] args){
            var task = Task.Run(() => MainProgram());
            //Console.WriteLine(MainProgram());
            //if (MainProgram().Wait(0))//TimeSpan.FromSeconds(0)))//
            if(task.Wait(TimeSpan.FromSeconds(60)))
            //return task.Result;
                Console.WriteLine("WORK COMPLETE");
            else
            {
                //throw new TimeoutException();
                Console.WriteLine("TIMED OUT");
                return;
            }
            return;
        }

        //private static void MainProgram()
        static async Task MainProgram()
        {
            FilevineOperatingAccounts OpAcc = new FilevineOperatingAccounts(CreateConnectionToFilevine());// _client);   
            FilevineProjects projects = new FilevineProjects(CreateConnectionToFilevine());
            //List<ChangeTracker> trackers = ChangeTrackingOperations.SelectActiveTrackers(1);//ManualTrackingOperations.SelectActiveTrackers(1);//ChangeTrackingOperations.SelectActiveTrackers(1); //appID = 1        
            var ManualTrackRow = ManualTrackingOperations.SelectActiveTrackers();// 1);

            //Console.WriteLine(ManualTrackRow.CheckID);
            //Console.WriteLine(ManualTrackRow.SudoDate);
            //Console.WriteLine(ManualTrackRow.Date);

            var CTree = ManualTrackingOperations.CheckDBChanges(ManualTrackRow.CheckID);//check <= statement in checkdbchanges method
            Console.WriteLine("->  changes:{0}", CTree.Count);
            foreach (var row in CTree)//changes is list of changetracker objects as well, but this time it's comparing query results from PCLaw Database
            {
                //Console.WriteLine("currently in ManualTrackingTable:{0}", ManualTrackRow.CheckID);
                //Console.WriteLine("Will be updated as new ID to ManualTrackingTable:{0}", row.CheckID);

                if (CTree.Count > 0)//CREATE   //iF INSERT
                {
                    //Console.WriteLine(ManualTrackRow.CheckID);//, 
                    //Console.WriteLine(row.CheckID);
                    if (ManualTrackRow.CheckID < row.CheckID)//update ManualTracking Table, this is the only time this table is manipulated
                    {
                        //Console.WriteLine("UPDATE ManualTracking Row...\n");
                        ManualTrackingOperations.UpdateActiveTrackers(row.CheckID, row.SudoDate);
                    }
                    try
                    {
                        //Console.WriteLine("TRYING");
                        var grabAffectedRow = OperatingCostsOperations.SelectCostODBC(row.CheckID);//OperatingCostsOperations.SelectCost(change.Identity); //get the row
                        //Console.WriteLine(grabAffectedRow.CheckID);
                        //Console.WriteLine(grabAffectedRow.Date);
                        var projectID = projects.GetProjectByNumber(grabAffectedRow.MatterID.ToString());//, Reports);
                        if (projectID.projectId != null)
                        {
                            if (true == InsertCostInFilevine(OpAcc, grabAffectedRow, projectID.projectId.native, row.CheckID.ToString()))//, change);//change.Identity.ToString());//pk_id //delete is not actual delete, status changes
                            {
                                ManualTrackingOperations.UploadEntryCostTracking(row.CheckID, MD5HashGenerator.GenerateKey(grabAffectedRow));
                            }
                        }
                        else
                            Logging.Report($"Invalid Matter or Non-Mapped Project Number. OpExpenseID: {row.CheckID}", -1);
                    }
                    catch (Exception ex)
                    {
                        Logging.Report(ex.ToString(), -1);
                    }
                }
            }
            //select (5000) rows -> hash them and compare
            //UPDATE //always runs guaranteed
            {
                try
                {
                    //Console.WriteLine(ManualTrackRow.CheckID);
                    var Top5000CTree = ManualTrackingOperations.Top5000(ManualTrackRow.CheckID);
                    foreach (var cost in Top5000CTree) //get ID and 
                    {
                        //Console.WriteLine(cost.CheckID);
                        var CostObj = OperatingCostsOperations.SelectCostODBCCheckId(cost.CheckID);//tbl_PK_id == cost.CheckID so basically query again? but alright GetObj();
                        string CostOdjhashed = MD5HashGenerator.GenerateKey(CostObj);
                        var costTrack = ManualTrackingOperations.SelectFromOurDB(cost.CheckID);

                        //what if query to Async fails, insert into Async
                        if (costTrack.CheckID == -1)
                            ManualTrackingOperations.UploadEntryCostTracking(cost.CheckID, CostOdjhashed);
                        if (costTrack.Hash != CostOdjhashed)
                        {
                            //Console.WriteLine(costTrack.Hash);
                            //Console.WriteLine(CostOdjhashed);
                            //Console.WriteLine("NOT EQUAL");//, UPDATE TO FILEVINE");
                            //Console.WriteLine(cost.CheckID);
                            var projectID = projects.GetProjectByNumber(CostObj.MatterID.ToString());
                            ///sometimes null because literally all of these projectId are unheard of so far to my filevine library list
                            //Console.WriteLine(projectID);
                            //Console.WriteLine(projectID.projectId);
                            //Console.WriteLine(projectID.projectId.native);
                            //Console.ReadLine();
                            if (projectID.projectId == null)
                            {
                                Console.WriteLine(cost.CheckID);
                                Console.WriteLine("ProjectId = DNE");
                            }
                            else if (true == UpdateCostInFilevine(OpAcc, CostObj, projectID.projectId.native, cost.CheckID.ToString()))
                            {
                                //Console.WriteLine("UPDATE TO FILEVINE");
                                ManualTrackingOperations.UpdateEntryCostTracking(cost.CheckID, CostOdjhashed);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Report(ex.ToString(), -1);
                }
            }
            //Console.WriteLine("Program done");
            //Console.ReadLine();
        }
        static public void printODBC(OdbcDataReader read)
        {
            int fCount = read.FieldCount;
            Console.Write(":");
            for (int i = 0; i < fCount; i++)
            {
                String fName = read.GetName(i);
                Console.Write(fName + ":");
            }
            Console.WriteLine();

            while (read.Read())
            {
                Console.Write(":");
                for (int i = 0; i < fCount; i++)
                {
                    String col = read.GetString(i);

                    Console.Write(col + ":");
                }
                Console.WriteLine();
            }
        }

        private static FilevineWebClient CreateConnectionToFilevine()
        {
            var appSettings = ConfigurationManager.AppSettings.AllKeys; //get key and secret from app.config as string array
            string url = "https://api.filevine.io/";
            string key = appSettings[0];
            string secret = appSettings[1];

            string apiTimestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ");
            string hashInput = $"{key}/{apiTimestamp}/{secret}";
            var hash = CreateMD5(hashInput);
            string apiHash = hash;
            //Console.WriteLine("apiTimeStamp");
            //Console.WriteLine(apiTimestamp);
            //Console.WriteLine("hashInput");
            //Console.WriteLine(hashInput);
            //Console.WriteLine("hash");
            //Console.WriteLine(hash);

            var Filevinesettings = new FilevineSetting(url, key, secret);
            var Filevinesession = new FilevineSession(Filevinesettings);
            var _client = new FilevineWebClient(Filevinesettings);
            return _client;
        }

        public static bool InsertCostInFilevine(FilevineOperatingAccounts OpAcc, PCLawOperatingExpense expense, long filevineID, string partnerId)//, ChangeTracking change)
        {
            OperatingAccountRequest operatingAccountRequest = new OperatingAccountRequest();
            itemId operatingAccountItemID = new itemId();
            OperatingAccount operatingObject = new OperatingAccount(expense);

            operatingAccountRequest.dataObject = operatingObject; //the row in PCLAWDB
            operatingAccountRequest.itemId = operatingAccountItemID;
            OperatingAccountResponse response = new OperatingAccountResponse();
            try
            {
                response = OpAcc.PostCollectionItem(filevineID, operatingAccountRequest, partnerId);
                Logging.Report("SuccessfulPostToFilevine", 0); //ChangeOperation.Update  //NOTE THIS WILL OCCUR EVEN THOUGH NOT UPLOADED, IF NO NEW ITEM ON FILEVINE IS BECAUSE PARTNER ID IS ALREADY EXISTED AND CAN'T BE REUSED
            }
            catch (Exception ex)
            {
                Logging.Report(ex.ToString(), -1);
            }
            
            if (response.itemId.partner == partnerId)
                return true;
            else
                return false;
        }

        public static bool UpdateCostInFilevine(FilevineOperatingAccounts OpAcc, PCLawOperatingExpense expense, long filevineID, string partnerId)
        {
            OperatingAccountRequest operatingAccountRequest = new OperatingAccountRequest();
            itemId operatingAccountItemID = new itemId();
            OperatingAccount operatingObject = new OperatingAccount(expense);
            //removed entry
            int operatingType = 1; //we want this to be equal to 2 if status == 2
            if (expense.Status == 2)
            { 
                operatingObject.ToRemoved();
                operatingType = 2;
            }
            operatingAccountRequest.dataObject = operatingObject; //the row in PCLAWDB
            operatingAccountRequest.itemId = operatingAccountItemID;
            OperatingAccountResponse response = new OperatingAccountResponse();
            try
            {
                //Console.WriteLine(filevineID);
                //Console.WriteLine(partnerId);
                response = OpAcc.PatchCollectionItem(filevineID, operatingAccountRequest, partnerId);
                if(response != null)
                    Logging.Report("SuccessfulPatchToFilevine", operatingType); //ChangeOperation.Update //NOTE THIS WILL OCCUR EVEN THOUGH NOT UPLOADED, IF NO NEW ITEM ON FILEVINE IS BECAUSE PARTNER ID IS ALREADY EXISTED AND CAN'T BE REUSED
                if (response == null)
                {
                    response = OpAcc.PostCollectionItem(filevineID, operatingAccountRequest, partnerId);
                    if (response != null)
                        Logging.Report("SuccessfulPostToFilevine", 0);
                }
                
            }
            catch (Exception ex)
            {
                Logging.Report(ex.ToString(), -1);
            }
            //Console.WriteLine(response.itemId.partner);
            if (response == null)
                return false;
            if (response.itemId == null)
                return false;
            else if (response.itemId.partner == partnerId)
            {
                //Console.WriteLine("TRUE PARTNER ID: {0}", response.itemId.partner);
                return true;
            }
            return false;
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
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
        //Insert Into [PCLAWDB_23774].[dbo].[GBAlloc] (GBankAllocInfStatus, GBankAllocInfCheckID, GBankAllocInfAllocID, GBankAllocInfInvDate, GBankAllocInfInvID, MatterID, GBankAllocInfInvNumber, GBankAllocInfGLID, GBankAllocInfTaskID, GBankAllocInfActivityID, GBankAllocInfAmount, GBankAllocInfEntryType, GBankAllocInfGSTCat, GBankAllocInfHoldFlag, GBankAllocInfTaxStatus, GBankAllocInfEOMFlag, GBankAllocInfAdvID, GBankAllocInfQuantity, GBankAllocInfRate, GBankAllocInfMarkupAmt, GBankAllocInfMarkupPct, GBankAllocInfSpareAmount, GBankAllocInfExplanation)
        //values(0,4294967297,0,20200930,0,7673878,0,0,0,0, 0.0 ,0,0,0,0,0,0, 0.0 ,0.0,0.0 ,0,0.0,'expl');

        //UPDATE GBAlloc
        //SET MatterID = 7673878
        //WHERE tbl_PK_id = 184;

        //--NOT ACTUAL DELETE MATE-- DELETE FROM GBAlloc WHERE tbl_PK_id = 6;
        //UPDATE GBAlloc
        //Set GBankAllocInfStatus = 2
        //where tbl_PK_id = 171;
    }

}


//Insert Into[PCLAWDB_23774].[dbo].[GBAlloc]
//(GBankAllocInfStatus, GBankAllocInfCheckID, GBankAllocInfAllocID, GBankAllocInfInvDate, GBankAllocInfInvID, MatterID, GBankAllocInfInvNumber, GBankAllocInfGLID, GBankAllocInfTaskID, GBankAllocInfActivityID, GBankAllocInfAmount, GBankAllocInfEntryType, GBankAllocInfGSTCat, GBankAllocInfHoldFlag, GBankAllocInfTaxStatus, GBankAllocInfEOMFlag, GBankAllocInfAdvID, GBankAllocInfQuantity, GBankAllocInfRate, GBankAllocInfMarkupAmt, GBankAllocInfMarkupPct, GBankAllocInfSpareAmount, GBankAllocInfExplanation)
//values(0,4294977332,0,20200930,0,7673878,0,0,0,0, 0.0 ,0,0,0,0,0,0, 0.0 ,0.0,0.0 ,0,0.0,'expl');

//SELECT TOP(1) ID, Date
//FROM[FilevineExpenseSync].[dbo].[ActiveTracker]

//UPDATE[PCLAWDB_23774].[dbo].[GBAlloc]
//Set GBankAllocInfStatus = 2
//WHERE GBankAllocInfCheckID = 4294977326;

//UPDATE[PCLAWDB_23774].[dbo].[GBAlloc]
//SET GBankAllocInfExplanation = 'change=10/19/2020'
//WHERE GBankAllocInfCheckID = 4294977327;

//SELECT TOP(1) ID,Date
//FROM
