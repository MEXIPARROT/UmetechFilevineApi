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
    public class ChangeTrackingOperations
    {
        public static List<ChangeTracker> SelectActiveTrackers(int appID)
        {
            var items = new List<ChangeTracker>();

            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT");
                str.AppendLine("Tracker.ID, Tracker.AppID, Tracker.TableID, Tracker.LastVersion,");
                str.AppendLine("TrackerApps.AppName, TrackerTables.TableName, TrackerTables.TablePKName");
                str.AppendLine("FROM ChangeTracker AS Tracker");
                str.AppendLine("LEFT JOIN ChangeTrackerApps AS TrackerApps ON TrackerApps.AppID = Tracker.AppID");
                str.AppendLine("LEFT JOIN ChangeTrackerTables AS TrackerTables ON TrackerTables.TableID = Tracker.TableID");
                str.AppendLine("WHERE TrackerTables.Active = 1");
                str.AppendLine("AND Tracker.AppID = @AppID");

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@AppID", appID);

                //Console.WriteLine(cmd.CommandText);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new ChangeTracker();
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }

        public static List<ChangeTracking> SelectChanges(string table, string pk, long lastVersion)
        {
            var items = new List<ChangeTracking>();

            var tableChk = table.Split(' ');
            if (tableChk.Length > 1)
                return items;
            var pkChk = table.Split(' ');
            if (pkChk.Length > 1)
                return items;

            using (var connection = DataFactory.CreateSqlConnection())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT SYS_CHANGE_VERSION, SYS_CHANGE_CREATION_VERSION, SYS_CHANGE_OPERATION, " + pk + " AS 'PK'"); //had to hardcode pk, if not says error in ChangeTracking.cs
                str.AppendLine("FROM CHANGETABLE(CHANGES " + table + ",  @LastVersion ) AS CT1 ORDER BY SYS_CHANGE_VERSION"); //I had to hardcode table, free to try with Brandon

                SqlCommand cmd = new SqlCommand(str.ToString(), connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Table", table);
                cmd.Parameters.AddWithValue("@PK", pk);
                cmd.Parameters.AddWithValue("@LastVersion", lastVersion);

                //Console.WriteLine(cmd.CommandText);

                DataTable dt = DataFactory.GetDataTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new ChangeTracking();
                    item.MapFromDataRow(dr);
                    items.Add(item);
                }
            }
            return items;
        }

        public static string UpdateTrackerLastVersion(ChangeTracker tracker, long lastVersion)//was void 
        {
            using (var connection = DataFactory.CreateAsyncBrokerConnection())
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ChangeTracker] SET LastVersion = @LastVersion WHERE ID = " + tracker.ID, connection); //when it was @lastversion and @ID it, cmd.CommandText wouldn't add in the actual values such as 16 and 7

                cmd.Parameters.AddWithValue("@ID", tracker.ID);
                cmd.Parameters.AddWithValue("@LastVersion", lastVersion);

                cmd.ExecuteNonQuery();
                return cmd.CommandText;
                //return cmd.ToString();//"fromUpdateTrackerLastVersion";
            }
        }
    }
}
