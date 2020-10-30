using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Contracts
{
    public enum ChangeOperation
    {
        Insert,
        Update,
        Delete
    }

    public class ChangeTracking : IMappable
    {
        public long SysChangeVersion { get; set; }
        public long SysChangeCreationVersion { get; set; }
        public ChangeOperation SysChangeOperation { get; set; }
        public long Identity { get; set; }


        public void MapFromDataRow(DataRow row)
        {
            this.SysChangeVersion = (long)row["SYS_CHANGE_VERSION"];

            if (!(row["SYS_CHANGE_CREATION_VERSION"].GetType() == typeof(DBNull)))
                this.SysChangeCreationVersion = (long)row["SYS_CHANGE_CREATION_VERSION"];
            if (!(row["SYS_CHANGE_OPERATION"].GetType() == typeof(DBNull)))
            {
                string op = (string)row["SYS_CHANGE_OPERATION"];
                this.SysChangeOperation = StringToOperation(op);
            }
            if ((row["PK"].GetType() == typeof(long)))
            {
                this.Identity = (long)row["PK"];
            }
            else
            {
                var intVal = (int)row["PK"];
                this.Identity = Convert.ToInt64(intVal);
            }

        }

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
    }
}
