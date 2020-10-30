using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLawData.Contracts
{
    public class PCLawContact : IMappable
    {
        public long ClientID { get; set; }
        public long AddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string FirmName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BusPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public void MapFromDataRow(DataRow row)
        {
            this.ClientID = (long)row["ClientID"];
            this.AddressID = (long)row["AddressID"];
            this.FirstName = (string)row["FirstName"];
            this.LastName = (string)row["LastName"];
            this.Title = (string)row["Title"];
            this.FirmName = (string)row["FirmName"];
            this.AddressLine1 = (string)row["AddressLine1"];
            this.AddressLine2 = (string)row["AddressLine2"];
            this.City = (string)row["City"];
            this.State = (string)row["State"];
            this.Zip = (string)row["ZIP"];
            this.BusPhone = (string)row["BusPhone"];
            this.CellPhone = (string)row["CellPhone"];
            this.HomePhone = (string)row["HomePhone"];
            this.Fax = (string)row["BusFax"];
            this.Email = (string)row["Email"];
        }
    }
}
