using PCLawData.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Operations
{
    public class ManualTrackingOperations
    {
        //Upload to CostTrackingTable
        public static void UploadEntryCostTracking(long ID, string Hash)
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection())//PCLawData.Connection.ODBCConnection())
            {
                //connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("INSERT INTO [FilevineExpenseSync].[dbo].[CostTracking] (ID, Hash)");
                str.AppendLine("VALUES (" + ID + ",'" + Hash + "')");
                //OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);
                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.ExecuteScalar();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.AddWithValue("@ID", ID);
                //cmd.Parameters.AddWithValue("@Hash", Hash);

                //Console.WriteLine(str.ToString());

                connection.Close();
            }
        }

        public static void UpdateEntryCostTracking(long ID, string Hash)
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection()) //PCLawData.Connection.ODBCConnection())
            {
                //connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("UPDATE [FilevineExpenseSync].[dbo].[CostTracking]");
                str.AppendLine("SET Hash = '" + Hash + "'");
                str.AppendLine("WHERE ID = " + ID);
                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                //OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);
                cmd.ExecuteScalar();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.AddWithValue("@ID", ID);
                //cmd.Parameters.AddWithValue("@Hash", Hash);

                //Console.WriteLine(str.ToString());

                connection.Close();
            }
        }

        public static void UpdateActiveTrackers(long ID, long sudoDate)
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            { 
                //connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("UPDATE [FilevineExpenseSync].[dbo].[ActiveTracker]"); //ideal is select the 1 and only row that exists in this table (only update never insert into)
                str.AppendLine("SET ID = " + ID + ", Date = " + sudoDate);
                str.AppendLine("WHERE ID > 0");
                SqlCommand cmd = new SqlCommand(str.ToString(), connection);

                //Console.WriteLine(str.ToString());
                cmd.CommandType = System.Data.CommandType.Text;
                DataRow row = DataFactory.GetDataRow(cmd);

                //Console.WriteLine(row.ItemArray[0].ToString());

                connection.Close();
            }
        }
        public static List<PCLawManualTrack> CheckDBChanges(long ID) //date in long not datetime
        {
            var items = new List<PCLawManualTrack>();

            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT GBankAllocInfCheckID AS ID, GBankAllocInfInvDate AS Date"); //ideal is select the 1 and only row that exists in this table (only update never insert into)
                str.AppendLine("FROM GBAlloc");
                str.AppendLine("WHERE GBankAllocInfCheckID > " + ID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //DataRow row = DataFactory.GetDataRow(cmd);
                //item.MapFromDataRow(row);
               

                DataTable dt = DataFactory.GetDataTable(cmd);
                //Console.WriteLine("DBChanges received: {0} rows that all contain {1} columns", dt.Rows.Count, dt.Columns.Count);
                
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new PCLawManualTrack();
                    item.MapFromDataRowManualTable(dr);
                    items.Add(item);
                }
                connection.Close();
            }
            return items;
        } 
        public static List<PCLawManualTrack> Top5000(long LastCheckID)
        {
            var items = new List<PCLawManualTrack>();

            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                long num = LastCheckID - 5000;
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT GBankAllocInfCheckID AS ID");//SELECT TOP (5000) *");
                //str.AppendLine("SELECT TOP (10) tbl_PK_id as 'ID'");
                str.AppendLine("FROM GBAlloc");
                //str.AppendLine("WHERE GBankAllocInfCheckID >= " + num);
                //str.AppendLine("WHERE GBankAllocInfCheckID = ")
                str.AppendLine("WHERE GBankAllocInfCheckID >= " + num);//4294974319
                //WHERE tbl_PK_id < 5000"); //
                //str.AppendLine("ORDER BY tbl_PK_id ASC");
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //DataRow row = DataFactory.GetDataRow(cmd);
                //item.MapFromDataRow(row);


                DataTable dt = DataFactory.GetDataTable(cmd);
                //Console.WriteLine("Top5000 received: {0} rows that all contain {1} columns", dt.Rows.Count, dt.Columns.Count);

                foreach (DataRow dr in dt.Rows)
                {
                    var item = new PCLawManualTrack();
                    item.MapFromDataRowID(dr);
                    items.Add(item);
                }
                connection.Close();
            }
            return items;
        }

        public static PCLawManualTrack SelectFromOurDB(long num)
        {
            var item = new PCLawManualTrack();
            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            {
                //connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT ID, Hash");
                str.AppendLine("FROM [FilevineExpenseSync].[dbo].[CostTracking]");
                str.AppendLine("WHERE ID = " + num);
                SqlCommand cmd = new SqlCommand(str.ToString(), connection);


                cmd.CommandType = System.Data.CommandType.Text;
                DataRow row = DataFactory.GetDataRow(cmd);
                if (row != null)
                    item.MapFromDataRowCostTracking(row);
                else
                    item.CheckID = -1;
                connection.Close();
            }
            return item;
        }

        public static PCLawManualTrack SelectActiveTrackers()//long ID)
        {
            var item = new PCLawManualTrack();
            using (var connection = DataFactory.CreateAsyncBrokerConnection())//PCLawData.Connection.ODBCConnection())
            {
                //connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT TOP (1) ID, Date"); //ideal is select the 1 and only row that exists in this table (only update never insert into)
                str.AppendLine("FROM [FilevineExpenseSync].[dbo].[ActiveTracker]");
                //OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);
                SqlCommand cmd = new SqlCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                DataRow row = DataFactory.GetDataRow(cmd);
                DataTable dt = DataFactory.GetDataTable(cmd);
                //Console.WriteLine("SelectActiveTrackers received: {0} rows that all contain {1} columns", dt.Rows.Count, dt.Columns.Count);

                item.MapFromDataRow(row);
                connection.Close();
            }

            return item;
        }
        public static List<ChangeTracking> SelectChanges(string table, string pk, long lastVersion)
        {
            var items = new List<ChangeTracking>();

            var tableChk = table.Split(' ');
            if (tableChk.Length > 1)
                return items;
            var pkChk = table.Split(' ');
            if (pkChk.Length > 1)
                return items;

            using (var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, " + pk + " AS 'PK'"); //had to hardcode pk, if not says error in ChangeTracking.cs
                str.AppendLine("FROM CHANGETABLE(CHANGES " + table + ",  @LastVersion ) AS CT1 ORDER BY SYS_CHANGE_VERSION"); //I had to hardcode table, free to try with Brandon

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Table", table);
                cmd.Parameters.AddWithValue("@PK", pk);
                cmd.Parameters.AddWithValue("@LastVersion", lastVersion);

                //Console.WriteLine(cmd.CommandText);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new ChangeTracking();
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }

        public static List<PCLawOperatingExpense> SelectOpExpenseByMatter(long matterID) //not meant to be used, connot use joins for ODBC?
        {
            var items = new List<PCLawOperatingExpense>();

            using (var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                str.AppendLine("GBAlloc.tbl_PK_id AS 'OpExpenseID', GBAlloc.GBankAllocInfStatus AS 'Status', GBAlloc.GBankAllocInfCheckID AS 'CheckID', GBComm.GBankCommInfDate AS 'Date', GBAlloc.MatterID AS 'MatterID',");
                str.AppendLine("GBAlloc.GBankAllocInfActivityID AS 'ExplantionType', GBAlloc.GBankAllocInfAmount AS 'Amount', GBAlloc.GBankAllocInfEntryType AS 'Type', GBAlloc.GBankAllocInfExplanation AS 'Explanation',");
                str.AppendLine("Matter.MatterInfoFileDesc AS 'CaseName', ActCode.ActivityCodesName AS 'ExplanationCodeName', GBMemTrn.GBankMemTranPaid2 AS 'PaidTo', GBAlloc.GBankAllocInfCheckID,");
                str.AppendLine("GBComm.GBankCommInfCheck AS 'CheckNum', GBAlloc.GBankAllocInfInvNumber AS 'InvNum'");
                str.AppendLine("FROM GBAlloc");
                str.AppendLine("LEFT JOIN MattInf AS Matter ON GBAlloc.MatterID = Matter.MatterID");
                str.AppendLine("LEFT JOIN ActCode ON GBAlloc.GBankAllocInfActivityID = ActCode.ActivityCodesID");
                str.AppendLine("LEFT JOIN GBMemTrn ON GBAlloc.GBankAllocInfCheckID = GBMemTrn.GBankMemTranCheckID");
                str.AppendLine("LEFT JOIN GBComm ON GBAlloc.GBankAllocInfCheckID = GBComm.GBankCommInfID");
                str.AppendLine("WHERE GBAlloc.MatterID = @ID");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", matterID);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new PCLawOperatingExpense();
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }

            return items;
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


    }
}
