using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Contracts
{

    public class PCLawManualTrack
    {
        public long CheckID { get; set; }
        public DateTime Date { get; set; }
        public long SudoDate { get; set; }
        public string Hash { get; set; }

        private ChangeOperation StringToOperation(string op)
        {
            switch (op)
            {
                case "I":
                    return ChangeOperation.Insert;
                case "U":
                    return ChangeOperation.Update;
                case "D":
                    return ChangeOperation.Delete;
                default:
                    return ChangeOperation.Insert;
            }
        }
        public void MapFromDataRowCostTracking(DataRow row)
        {
            this.CheckID = (long)row["ID"];
            this.Hash = (string)row["Hash"];
        }
            public void MapFromDataRow(DataRow row)
        {
            this.CheckID = (long)row["ID"];
            if (!(row["Date"].GetType() == typeof(DBNull)))
            {
                var date = (long)row["Date"];
                SudoDate = date;

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
        public void MapFromDataRowID(DataRow row)
        {
            this.CheckID = (long)row["ID"];
        }

            public void MapFromDataRowManualTable(DataRow row)
        {
            this.CheckID = (long)row["ID"];
            if (!(row["Date"].GetType() == typeof(DBNull)))
            {
                var date = (long)row["Date"];
                SudoDate = date;

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
