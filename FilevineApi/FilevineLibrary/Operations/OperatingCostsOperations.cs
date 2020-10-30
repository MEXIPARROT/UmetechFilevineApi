using PCLawData.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Operations
{
    public class OperatingCostsOperations
    {
        public static PCLawOperatingExpense SelectCost(long ID)
        {
            var item = new PCLawOperatingExpense();

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
                str.AppendLine("WHERE GBAlloc.tbl_PK_id = @ID");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                //Console.WriteLine("HEREHERE");
                //Console.WriteLine(cmd.CommandText);
                DataRow row = DataFactory.GetDataRow(cmd);
                

                item.MapFromDataRow(row);
            }

            return item;
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
    }
}
