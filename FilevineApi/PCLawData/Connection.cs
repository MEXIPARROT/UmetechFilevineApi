using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData
{
    public class Connection
    {
        private static readonly string OdbcConnectionString;
        static Connection()
        {
            OdbcConnectionString = ConfigurationManager.ConnectionStrings["ITGSQL"].ConnectionString;
        }
        public static OdbcConnection ODBCConnection()
        {
            OdbcConnection connection = new OdbcConnection("DSN=PCLAWDB");//OdbcConnectionString);
            return connection;
        }
        public static DataRow GetDataRow(OdbcCommand cmd)
        {
            OdbcDataAdapter adapt = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        public static DataTable GetDataTable(OdbcCommand cmd)
        {
            OdbcDataAdapter adapt = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            return dt;
        }

    }
}
