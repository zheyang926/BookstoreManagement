using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using project.data;

namespace project.bus
{
    public class SalesManager
    {
        private int customerID;
        private string customerName;
        private string street;
        private string city;
        private string postalCode;
        private string phoneNumber;
        private string faxNumber;
        private float creditLimit;

        public int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string FaxNumber { get => faxNumber; set => faxNumber = value; }
        public float CreditLimit { get => creditLimit; set => creditLimit = value; }


        public DataSet CreateDataSetTable(DataSet dsProject, DataTable dtCustomer)
        {
            return SalesManagerDB.CreateDataSetTableDB(dsProject, dtCustomer);
        }
        public void ReadData(DataSet dsProject, DataTable dtCustomer)
        {
            SalesManagerDB.ReadDataDB(dsProject, dtCustomer);
        }
        public bool SaveRecord(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            return SalesManagerDB.SaveRecordDB(dsProject, dtCustomer, salesManager);
        }

        public bool UpdateRecord(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            return SalesManagerDB.UpdateRecordDB(dsProject, dtCustomer, salesManager);
        }

        public DataTable SearchRecord(DataTable dtCustomer, SalesManager salesManager)
        {
            return SalesManagerDB.SearchRecordDB(dtCustomer, salesManager);
        }
        public bool DelRecord(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            return SalesManagerDB.DelRecordDB(dsProject, dtCustomer, salesManager);
        }
    }
}
