using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Contracts
{
    [Serializable]
    public class PCLawOperatingExpense
    {
        public int OpExpenseID { get; set; }
        public byte Status { get; set; }
        public long MatterID { get; set; }
        public long CheckID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public long ExplanationCodeType { get; set; }
        public string Explanation { get; set; }
        public string CaseName { get; set; }
        public string ExplanationCodeName { get; set; }
        public string PaidTo { get; set; }
        public string CheckNum { get; set; }
        public long InvNum { get; set; }

        public string Hash { get; set; }

        public string CreateHash()
        {
            string Hash;
            Hash = OpExpenseID.ToString() + Status.ToString() + MatterID.ToString() + CheckID.ToString() + Date.ToString() + Amount.ToString() + Type.ToString() + TypeName + Explanation + CaseName + PaidTo + CheckNum + InvNum.ToString();
            return Hash;
        }

        private string GetTypeName(int typeID)
        {
            switch (typeID)
            {
                case 1100:
                    return "Unknown";
                case 1101:
                    return "Invoice Payment";
                case 1102:
                    return "Fee Receipt";
                case 1103:
                    return "Rtnr Alloc On Inv";
                case 1104:
                    return "Retainers Carried Forward";
                case 1105:
                    return "Receipt Balance Forward";
                case 1300:
                    return "XFER";
                case 1400:
                    return "Client Expense";
                case 1600:
                    return "Expense Recovery";
                case 1899:
                    return "Write Off";
                case 6500:
                    return "Accounts Payable";
                default:
                    return "";
            }
        }
        public void MapFromDataRow(DataRow row)
        {
            this.OpExpenseID = (int)row["OpExpenseID"];
            this.Status = (byte)row["Status"];
            this.MatterID = (long)row["MatterID"];
            this.CheckID = (long)row["CheckID"];
            this.Amount = (double)row["Amount"];
            this.Type = (int)row["Type"];
            this.TypeName = GetTypeName(Type);
            this.ExplanationCodeType = (long)row["ExplantionType"];
            this.Explanation = (string)row["Explanation"];

            this.InvNum = (long)row["InvNum"];

            if (!(row["CheckNum"].GetType() == typeof(DBNull)))//!NULL
                this.CheckNum = (string)row["CheckNum"];

            if (!(row["CaseName"].GetType() == typeof(DBNull)))
                this.CaseName = (string)row["CaseName"];
            if (!(row["ExplanationCodeName"].GetType() == typeof(DBNull)))
                this.ExplanationCodeName = (string)row["ExplanationCodeName"];
            if (!(row["PaidTo"].GetType() == typeof(DBNull)))
                this.PaidTo = (string)row["PaidTo"];

            if (!(row["Date"].GetType() == typeof(DBNull)))
            {
                var date = (long)row["Date"];
                try
                {
                    var dateString = date.ToString();
                    if (dateString.Length == 8)
                    {
                        var year = dateString.Substring(0, 4);
                        var month = dateString.Substring(4, 2);
                        var day = dateString.Substring(6, 2);
                        this.Date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                    }
                    else
                    {
                        this.Date = new DateTime(1800, 1, 1);
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                this.Date = new DateTime(1800, 1, 1);
            }

        }

        public void MapFromDataRowFirst(DataRow row)
        {
            //this.OpExpenseID = (int)row["OpExpenseID"];
            this.Status = (byte)row["Status"];
            this.CheckID = (long)row["CheckID"];
            this.MatterID = (long)row["MatterID"]; 
            this.ExplanationCodeType = (long)row["ExplantionType"];
            this.Amount = (double)row["Amount"];
            this.Type = (int)row["Type"];
            this.TypeName = GetTypeName(Type);
            if (!(row["Explanation"].GetType() == typeof(DBNull)))
                this.Explanation = (string)row["Explanation"];
            this.InvNum = (long)row["InvNum"];

        }

        public void MapFromDataDATE(DataRow row)
        {
            if (!(row["Date"].GetType() == typeof(DBNull)))
            {
                var date = (long)row["Date"];
                try
                {
                    var dateString = date.ToString();
                    if (dateString.Length == 8)
                    {
                        var year = dateString.Substring(0, 4);
                        var month = dateString.Substring(4, 2);
                        var day = dateString.Substring(6, 2);
                        this.Date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                    }
                    else
                    {
                        this.Date = new DateTime(1800, 1, 1);
                    }
                }
                catch (Exception ex) { }
            }
            else
            {
                this.Date = new DateTime(1800, 1, 1);
            }
        }

    }
}
