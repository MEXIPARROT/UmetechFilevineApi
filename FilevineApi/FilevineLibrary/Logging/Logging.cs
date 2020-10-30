using PCLawData.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FilevineLibrary;

namespace PCLawData.Logging
{
    public class Logging
    {
        public string PCLawLogString { get; set; }
        public DateTime LogDate { get; set; }

        public Logging()
        {
            PCLawLogString = "defaultText";
            DateTime date1 = new DateTime(1997, 9, 14);
        }

        public void Report(string error, ChangeOperation Type) //To track when something wants to upload or whatnot
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            {
                DateTime CurrentDate = new DateTime();
                CurrentDate = DateTime.Now;
                //CurrentDate = Convert.ToDateTime(dateTimePicker1)
                //StringBuilder str = new StringBuilder();
                //str.AppendLine("INSERT INTO LogsTemp (PCLawMichelErrors, LogDate) VALUES("+error+", "+CurrentDate+" )");
                SqlCommand cmd = new SqlCommand("INSERT INTO LogsTemp (ErrorString, LogDate, OperatingType) VALUES (@Error , @Date, @Type)", connection);
                //SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                //cmd.CommandType = System.Data.CommandType.Text;
                //INSERT INTO LogsTemp
                //VALUES('testing', '17-12-2015');
                cmd.Parameters.AddWithValue("@Error", error);
                cmd.Parameters.AddWithValue("@Date", CurrentDate);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.ExecuteNonQuery();

            }
        }
        public void Report(string error, int num)//expect -1 only //caught because catch with Reports.Report(string,-1) has occured in a catch
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            {
                DateTime CurrentDate = new DateTime();
                CurrentDate = DateTime.Now;
                SqlCommand cmd = new SqlCommand("INSERT INTO LogsTemp (ErrorString, LogDate, OperatingType) VALUES (@Error , @Date, @Num)", connection);
                cmd.Parameters.AddWithValue("@Error", error);
                cmd.Parameters.AddWithValue("@Date", CurrentDate);
                cmd.Parameters.AddWithValue("@Num", num);
                cmd.ExecuteNonQuery();

            }
        }
    }
}
