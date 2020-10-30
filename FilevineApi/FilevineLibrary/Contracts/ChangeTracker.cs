 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Contracts
{
    public class ChangeTracker : IMappable
    {
        public int ID { get; set; }
        public int AppID { get; set; }
        public int TableID { get; set; }
        public long LastVersion { get; set; }
        public string AppName { get; set; }
        public string TableName { get; set; }
        public string TablePKName { get; set; }

        public void MapFromDataRow(DataRow row)
        {
            this.ID = (int)row["ID"];
            this.AppID = (int)row["AppID"];
            this.TableID = (int)row["TableID"];
            this.LastVersion = (long)row["LastVersion"];
            this.AppName = (string)row["AppName"];
            this.TableName = (string)row["TableName"];
            this.TablePKName = (string)row["TablePKName"];
        }
    }
}
