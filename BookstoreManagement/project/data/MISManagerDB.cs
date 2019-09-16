using project.bus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.data
{
    public static class MISManagerDB
    {
        public static DataTable ExecuteReaderQuery(string queryText)
        {
            DataTable dataTable = new DataTable();

            if (string.IsNullOrEmpty(queryText))
            {
                return dataTable;
            }

            try
            {
                using (SqlConnection conn = UtilityDB.ConnectDB())
                {
                    //SqlCommand sqlCommand = new SqlCommand();
                    //sqlCommand.Connection = conn;
                    //sqlCommand.CommandText = queryText;

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = queryText
                    };

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(dataReader);
                    return dataTable;
                }
            }
            catch (Exception)
            {
                return dataTable;

            }
        }
        private static bool SqlNonQuery(string queryText)
        {
            try
            {
                using (SqlConnection conn = UtilityDB.ConnectDB())
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = queryText;
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static DataTable ReadDataDB()
        {
            string queryText = String.Format("Select * from users");
            return MISManagerDB.ExecuteReaderQuery(queryText);
        }
        public static bool SaveRecordDB(MISManager manager)
        {
            string queryText = String.Format("Insert into users values ('{0}','{1}','{2}','{3}',{4})",
                manager.FirstName, manager.LastName, manager.UserName, manager.Password, manager.RoleId);

            return MISManagerDB.SqlNonQuery(queryText);
        }
        public static bool UpdateRecordDB(MISManager manager)
        {
            string queryText = String.Format("Update users set FirstName = '{0}', LastName = '{1}', " +
                " UserName = '{2}', Password='{3}', RoleID = {4}   where UserID = {5} ",
                 manager.FirstName, manager.LastName, manager.UserName, manager.Password, manager.RoleId, manager.UserId);
            return MISManagerDB.SqlNonQuery(queryText);
        }
        public static bool DelRecoredDB(MISManager manager)
        {
            string queryText = String.Format("Delete From users where UserID = {0} ", manager.UserId);
            return MISManagerDB.SqlNonQuery(queryText);
        }
        public static DataTable SearchRecordDB(MISManager manager)
        {
            //string queryText = string.Format("Select * from users where UserID = {0} or FirstName LIKE '%{1}%' " +
            //    "or LastName LIKE '%{2}%' or UserName LIKE '%{3}%' or RoleID = {4} ",
            //    manager.UserId, manager.FirstName, manager.LastName, manager.UserName, manager.RoleId);

            string query = " Select * from users where ";

            if (manager.UserId != 0)
            {
                query += string.Format(" UserID = {0} ", manager.UserId);
            }
            else if (!string.IsNullOrEmpty(manager.FirstName))
            {
                query += string.Format(" FirstName LIKE '%{0}%' ", manager.FirstName);
            }
            else if (!string.IsNullOrEmpty(manager.LastName))
            {
                query += string.Format(" LastName LIKE '%{0}%' ", manager.LastName);
            }
            else if (!string.IsNullOrEmpty(manager.UserName))
            {
                query += string.Format(" UserName LIKE '%{0}%' ", manager.UserName);
            }
            else if (manager.RoleId != 0)
            {
                query += string.Format(" RoleID = '{0}' ", manager.RoleId);
            }

            return MISManagerDB.ExecuteReaderQuery(query);
        }

        //public static bool CheckUserNameDB(String UserName)
        //{
        //    using (SqlConnection conn = UtilityDB.ConnectDB())
        //    {
        //        try
        //        {
        //            SqlCommand sqlCommand = new SqlCommand();
        //            sqlCommand.Connection = conn;
        //            sqlCommand.CommandText = String.Format("Select Count(*) from users where UserName = '{0}'",
        //                                                    UserName);

        //            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());

        //            return count > 0;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }

        //    }
        //}

    }
}
