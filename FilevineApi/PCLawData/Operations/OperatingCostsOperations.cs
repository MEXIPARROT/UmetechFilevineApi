using PCLawData.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Operations
{
    public class OperatingCostsOperations
    {
        public static PCLawManualTrack SelectCost(long ID)
        {
            var item = new PCLawManualTrack();

            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT TOP (1) ID, Date"); //ideal is select the 1 and only row that exists in this table (only update never insert into)
                str.AppendLine("FROM ManualTracking");
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                DataRow row = DataFactory.GetDataRow(cmd);
                item.MapFromDataRow(row);
                connection.Close();
            }

            return item;
        }
        public static PCLawOperatingExpense SelectCostODBCCheckId(long ID)
        {
            var item = new PCLawOperatingExpense();

            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                //str.AppendLine("GBAlloc.tbl_PK_id AS 'OpExpenseID', " +
                str.AppendLine("GBAlloc.GBankAllocInfStatus AS Status, GBAlloc.GBankAllocInfCheckID AS CheckID, GBAlloc.MatterID AS MatterID,");
                str.AppendLine("GBAlloc.GBankAllocInfActivityID AS ExplantionType, GBAlloc.GBankAllocInfAmount AS Amount, GBAlloc.GBankAllocInfEntryType AS Type, GBAlloc.GBankAllocInfExplanation AS Explanation,");
                str.AppendLine("GBAlloc.GBankAllocInfCheckID,");
                str.AppendLine("GBAlloc.GBankAllocInfInvNumber AS InvNum");
                str.AppendLine("FROM GBAlloc");
                str.AppendLine("WHERE GBAlloc.GBankAllocInfCheckID = " + ID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                DataRow row = DataFactory.GetDataRow(cmd);
                item.MapFromDataRowFirst(row);
                item.CaseName = CaseName(item.MatterID);
                item.PaidTo = PaidTo(item.CheckID);
                item.CheckNum = CheckNum(item.CheckID);
                item.Date = Date(item.CheckID);
                //item.Hash = CreateMD5(item.CreateHash());
                //item.Hash = MD5HashGenerator
                connection.Close();
            }

            return item;
        }

        public static PCLawOperatingExpense SelectCostODBC(long ID)
        {
            var item = new PCLawOperatingExpense();

            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                //str.AppendLine("GBAlloc.tbl_PK_id AS 'OpExpenseID', " +
                str.AppendLine("GBAlloc.GBankAllocInfStatus AS Status, GBAlloc.GBankAllocInfCheckID AS CheckID, GBAlloc.MatterID AS MatterID,");
                str.AppendLine("GBAlloc.GBankAllocInfActivityID AS ExplantionType, GBAlloc.GBankAllocInfAmount AS Amount, GBAlloc.GBankAllocInfEntryType AS Type, GBAlloc.GBankAllocInfExplanation AS Explanation,");
                str.AppendLine("GBAlloc.GBankAllocInfCheckID,");
                str.AppendLine("GBAlloc.GBankAllocInfInvNumber AS InvNum");
                str.AppendLine("FROM GBAlloc");
                str.AppendLine("WHERE GBAlloc.GBankAllocInfCheckID = " + ID);

                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);
                //"SELECT GBAlloc.tbl_PK_id AS 'OpExpenseID', GBAlloc.GBankAllocInfStatus AS 'Status', GBAlloc.GBankAllocInfCheckID AS 'CheckID', GBAlloc.MatterID AS 'MatterID', GBAlloc.GBankAllocInfActivityID AS 'ExplantionType', GBAlloc.GBankAllocInfAmount AS 'Amount', GBAlloc.GBankAllocInfEntryType AS 'Type', GBAlloc.GBankAllocInfExplanation AS 'Explanation', GBAlloc.GBankAllocInfCheckID, GBAlloc.GBankAllocInfInvNumber AS 'InvNum' FROM GBAlloc WHERE GBAlloc.tbl_PK_id = " + ID + "",//

                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.AddWithValue("@ID", ID);
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                item.MapFromDataRowFirst(row);
                //item.MapFromDataRow(row);

                //Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", item.OpExpenseID, item.Status, item.CheckID, item.Date, item.MatterID, item.ExplanationCodeType, item.Amount, item.Type, item.Explanation, item.CaseName, item.PaidTo,/*item.GBankAllocInfCheckID,*/item.InvNum);
                //Console.WriteLine(item.OpExpenseID);
                item.CaseName = CaseName(item.MatterID);
                //Console.WriteLine(item.CaseName);
                item.PaidTo = PaidTo(item.CheckID);
                //Console.WriteLine(item.PaidTo);
                item.CheckNum = CheckNum(item.CheckID);
                //Console.WriteLine(item.CheckNum);
                item.Date = Date(item.CheckID);
                //Console.WriteLine(item.Date);

                connection.Close();
            }

            return item;
        }

        public static string CaseName(long MatterID)
        {
            string CaseName = "";
            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT MatterInfoFileDesc AS CaseName ");
                str.AppendLine("FROM MattInf");
                str.AppendLine("WHERE MatterID = " + MatterID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                connection.Close();
                if (row != null)
                    CaseName = row.ItemArray[0].ToString();
                else
                    return "";
            }
            return CaseName;
        }

        public static string PaidTo(long CheckID)
        {
            string PaidTo = "";
            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT GBMemTrn.GBankMemTranPaid2 AS PaidTo");
                str.AppendLine("FROM GBMemTrn");
                str.AppendLine("WHERE GBMemTrn.GBankMemTranCheckID = " + CheckID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                connection.Close();
                if (row != null)
                    PaidTo = row.ItemArray[0].ToString();
                else
                    return "";
            }
            return PaidTo;
        }

        public static string CheckNum(long CheckID)
        {
            string CheckNum = "";
            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT GBComm.GBankCommInfCheck AS CheckNum");
                str.AppendLine("FROM GBComm");
                str.AppendLine("WHERE GBComm.GBankCommInfID = " + CheckID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                connection.Close();
                if (row != null)
                    CheckNum = row.ItemArray[0].ToString();
                else
                    return "";
            }
            return CheckNum;
        }

        public static DateTime Date(long CheckID)
        {
            DateTime Date;
            var ExpenseTemp = new PCLawOperatingExpense();
            using (var connection = PCLawData.Connection.ODBCConnection())
            {
                connection.Open();
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT GBComm.GBankCommInfDate AS Date");
                str.AppendLine("FROM GBComm");
                str.AppendLine("WHERE GBComm.GBankCommInfID = " + CheckID);
                OdbcCommand cmd = new OdbcCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                connection.Close();
                if (row != null)
                    ExpenseTemp.MapFromDataDATE(row);//CaseName = row.ItemArray[0].ToString();
                else
                    return Date = new DateTime(1800, 1, 1); ;//basically empty

                //Console.WriteLine(ExpenseTemp.Date);
                Date = ExpenseTemp.Date;
            }
            return Date;
        }


        public static List<PCLawOperatingExpense> SelectOpExpenseByMatter(long matterID)
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
