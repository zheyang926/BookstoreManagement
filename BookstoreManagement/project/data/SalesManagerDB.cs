using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using project.bus;
using System.Windows.Forms;

namespace project.data
{
    public static class SalesManagerDB
    {
        public static SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        public static SqlConnection connection = UtilityDB.ConnectDB();

        public static DataSet CreateDataSetTableDB(DataSet dsProject, DataTable dtCustomer)
        {
            DataColumn customerIDAuto = dtCustomer.Columns.Add("CustomerID", typeof(Int32));
            customerIDAuto.AutoIncrement = true;
            customerIDAuto.AutoIncrementSeed = 1;
            customerIDAuto.AutoIncrementStep = 1;

            dtCustomer.Columns.Add("CustomerName", typeof(string));
            dtCustomer.Columns.Add("Street", typeof(string));
            dtCustomer.Columns.Add("City", typeof(string));
            dtCustomer.Columns.Add("PostalCode", typeof(string));
            dtCustomer.Columns.Add("PhoneNumber", typeof(string));
            dtCustomer.Columns.Add("FaxNumber", typeof(string));
            dtCustomer.Columns.Add("CreditLimit", typeof(float));

            dtCustomer.PrimaryKey = new DataColumn[] { dtCustomer.Columns["CustomerID"] };

            dsProject.Tables.Add(dtCustomer);

            return dsProject;

        }
        public static void ReadDataDB(DataSet dsProject, DataTable dtCustomer)
        {
            //SqlDataAdapter sqlDataAdapter;

            sqlDataAdapter = new SqlDataAdapter("select * from customer", connection);

            sqlDataAdapter.Fill(dsProject.Tables["customer"]);
        }
        public static bool SaveRecordDB(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            try
            {
                dtCustomer.Rows.Add(null, salesManager.CustomerName, salesManager.Street, salesManager.City,
                salesManager.PostalCode, salesManager.PhoneNumber, salesManager.FaxNumber, salesManager.CreditLimit);

                //! whenever you want to make a column auto-increment in DataTable then you have to insert null into it.

                string query = String.Format("Insert into customer values ('{0}','{1}','{2}','{3}','{4}','{5}',{6}) ",
                    salesManager.CustomerName, salesManager.Street, salesManager.City, salesManager.PostalCode,
                    salesManager.PhoneNumber, salesManager.FaxNumber, salesManager.CreditLimit);

                sqlDataAdapter.InsertCommand = new SqlCommand(query, connection);

                sqlDataAdapter.Update(dsProject, "customer"); //! table name is from dataset not from the real database table name.

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public static bool UpdateRecordDB(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            try
            {
                DataRow drFindCustomerID = dtCustomer.Rows.Find(salesManager.CustomerID);

                drFindCustomerID["CustomerID"] = salesManager.CustomerID;
                drFindCustomerID["CustomerName"] = salesManager.CustomerName;
                drFindCustomerID["Street"] = salesManager.Street;
                drFindCustomerID["City"] = salesManager.City;
                drFindCustomerID["PostalCode"] = salesManager.PostalCode;
                drFindCustomerID["PhoneNumber"] = salesManager.PhoneNumber;
                drFindCustomerID["FaxNumber"] = salesManager.FaxNumber;
                drFindCustomerID["CreditLimit"] = salesManager.CreditLimit;

                string query = string.Format("Update customer set CustomerName ='{0}', Street ='{1}',City ='{2}', " +
               " PostalCode ='{3}', PhoneNumber ='{4}',FaxNumber ='{5}',CreditLimit ='{6}' where CustomerID = {7}",
                   salesManager.CustomerName, salesManager.Street, salesManager.City, salesManager.PostalCode,
                   salesManager.PhoneNumber, salesManager.FaxNumber, salesManager.CreditLimit, salesManager.CustomerID);

                sqlDataAdapter.UpdateCommand = new SqlCommand(query, connection);
                sqlDataAdapter.Update(dsProject, "customer");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public static bool DelRecordDB(DataSet dsProject, DataTable dtCustomer, SalesManager salesManager)
        {
            try
            {
                DataRow drFindCustomer = dtCustomer.Rows.Find(salesManager.CustomerID);
                drFindCustomer.Delete();
                string que = string.Format("Delete From customer Where CustomerID = {0}", salesManager.CustomerID);
                sqlDataAdapter.DeleteCommand = new SqlCommand(que, connection);
                sqlDataAdapter.Update(dsProject, "customer");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable SearchRecordDB(DataTable dtCustomer, SalesManager salesManager)
        {
            try
            {
                DataTable dtCustomerSearch = new DataTable("customerSearch");

                string query = "";

                if (salesManager.CustomerID != 0)
                {
                    query = string.Format(" CustomerID = {0} ", salesManager.CustomerID);
                }
                else if (!string.IsNullOrEmpty(salesManager.CustomerName))
                {
                    query = string.Format(" CustomerName LIKE '%{0}%' ", salesManager.CustomerName);
                }
                else if (!string.IsNullOrEmpty(salesManager.City))
                {
                    query = string.Format(" City LIKE '%{0}%' ", salesManager.City);
                }
                else if (!string.IsNullOrEmpty(salesManager.PostalCode))
                {
                    query = string.Format(" PostalCode LIKE '%{0}%' ", salesManager.PostalCode);
                }
                else if (!string.IsNullOrEmpty(salesManager.PhoneNumber))
                {
                    query = string.Format(" PhoneNumber LIKE '%{0}%' ", salesManager.PhoneNumber);
                }
                else if (!string.IsNullOrEmpty(salesManager.FaxNumber))
                {
                    query = string.Format(" FaxNumber LIKE '%{0}%' ", salesManager.FaxNumber);
                }

                dtCustomerSearch = dtCustomer.Select(query).CopyToDataTable();

                return dtCustomerSearch;

                //MessageBox.Show(" " + query);

                //dsProject.Tables.Add("customerSearch");
                //DataRow[] foundRows;

                //// Use the Select method to find all rows matching the filter.
                //foundRows = dtCustomer.Select(query);

                //// Print column 0 of each returned row.
                //for (int i = 0; i < foundRows.Length; i++)
                //{
                //    MessageBox.Show(" " + foundRows[i][0]);
                //}              

                //return dtCustomerSearch;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
