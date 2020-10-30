using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData
{
    public class DataFactory
    {
        private static readonly string SqlConnectionString;
        private static readonly string AsyncBrokerConnectionString;

        static DataFactory()
        {
            SqlConnectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            AsyncBrokerConnectionString = ConfigurationManager.ConnectionStrings["AsyncSQLConnectionString"].ConnectionString; //actually NOT async, just accessing a different Database in the same server!
        }


        public static SqlConnection CreateSqlConnection(bool open = true)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            if (open)
                connection.Open();
            return connection;
        }

        public static SqlConnection CreateAsyncBrokerConnection(bool open = true)
        {
            SqlConnection connection = new SqlConnection(AsyncBrokerConnectionString);
            if (open)
                connection.Open();
            return connection;
        }


        public static DataRow GetDataRow(SqlCommand cmd)
        {
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            //Console.WriteLine(adapt.SelectCommand);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }


        public static DataTable GetDataTable(SqlCommand cmd)
        {
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            return dt;
        }
    }
}
