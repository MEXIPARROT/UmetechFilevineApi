using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary.FilevineWebAPI.Objects
{
    public class Expense
    {
        [JsonProperty("amountdue")]
        public double amountdue { get; set; }

        [JsonProperty("date")]
        public DateTime date { get; set; }

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("notes")]
        public string notes { get; set; }

        [JsonProperty("datepaid")]
        public DateTime datepaid { get; set; }

        [JsonProperty("re")]
        public string re { get; set; }

        [JsonProperty("payee")]
        public string payee { get; set; }


        //Expense(int i)
        public Expense()
        {
            amountdue = 2.0;
            date = DateTime.Now;
            notes = "test via c# app";
            datepaid = DateTime.Now;
            re = "testRE";
            payee = "testPAYEE";
        }

        public Expense ToExpando()
        {
            var expense = new Expense();
            return expense;
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
