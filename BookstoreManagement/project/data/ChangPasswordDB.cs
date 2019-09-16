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
    public class ChangPasswordDB
    {
        public static SqlConnection connDB = UtilityDB.ConnectDB();
        public static SqlCommand cmd = new SqlCommand();

        public static int CheckUser(User user)
        {
            if (connDB.State == ConnectionState.Closed)
            {
                connDB = UtilityDB.ConnectDB();
                cmd = new SqlCommand();
            }
            cmd.Connection = connDB;
            cmd.CommandText = string.Format("select * from Users where UserName='{0}' and Password='{1}'", user.Username, user.Oldpassword);
            SqlDataReader reader = cmd.ExecuteReader(); //looks like a table 
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            cmd.Dispose();
            connDB.Close();
            return count;
        }

        public static bool UpdatePassword(User user)
        {
            try
            {

                if (connDB.State == ConnectionState.Closed)
                {
                    connDB = UtilityDB.ConnectDB();
                    cmd = new SqlCommand();
                }
                cmd.Connection = connDB;
                cmd.CommandText = string.Format("update users set Password='{0}' where UserName='{1}'", user.Newpassword, user.Username);
                cmd.ExecuteNonQuery();
                connDB.Close();
            }
            catch (Exception)
            {
                return false;
                // throw;
            }

            return true;
        }


        }
    }


