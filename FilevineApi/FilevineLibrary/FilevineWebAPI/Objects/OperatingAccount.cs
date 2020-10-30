using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLawData.Operations;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class OperatingAccount
    {
        [JsonProperty("date")]
        public DateTime date { get; set; } //1st

        [JsonProperty("entry")] //originally "amount"
        public int entry { get; set; } //amount

        [JsonProperty("checkNumber")]
        public string checkNumber { get; set; } //3rd

        [JsonProperty("invoice")]
        public int invoice { get; set; }//might be long  //4th

        [JsonProperty("type")]
        public string type { get; set; } //5th

        [JsonProperty("ExplCode")]
        public string ExplCode { get; set; } //6th

        [JsonProperty("Explanation")]
        public string Explanation { get; set; } //7th

        [JsonProperty("RcptsAmount")]
        public double RcptsAmount { get; set; } //might be decimal //8th

        [JsonProperty("DisbsAmount")]
        public double DisbsAmount { get; set; } //9th


        //Expense(int i)
        public OperatingAccount(PCLawData.Contracts.PCLawOperatingExpense expense)//
        {
            date = expense.Date;
            entry = (int)expense.CheckID;
            type = expense.TypeName;
            checkNumber = expense.CheckNum;
            invoice = (int)expense.InvNum;
            ExplCode = expense.ExplanationCodeName;
            Explanation = expense.Explanation;
            
            

            if (expense.Type > 0 && expense.Type < 1200)
                RcptsAmount = expense.Amount;
            else
                DisbsAmount = expense.Amount;
        
        }

        public Expense ToExpando() //I don't think I use this
        {
            var expense = new Expense();
            return expense;
        }

        public void ToRemoved()
        {
            type = "";
            checkNumber = "";
            invoice = 0;
            ExplCode = "";
            Explanation = "Removed Entry";
            RcptsAmount = 0;
            DisbsAmount = 0;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
