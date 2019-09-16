using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project.bus;

namespace project.data
{
    public static class LoginDB
    {
        public static int CheckLoginDB(Login login)
        {
            using (SqlConnection conn = UtilityDB.ConnectDB())
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = String.Format("Select Count(*) from users where UserName = '{0}' and Password = '{1}'",
                                                            login.UserName, login.Password);

                    int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    //todo Return First Column and First Row other columns and rows are ignored.

                    if (count != 0)
                    {
                        sqlCommand.CommandText = String.Format("Select RoleID from users where UserName = '{0}' and Password = '{1}'",
                                                           login.UserName, login.Password);

                        int roleID = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        return roleID;
                    }

                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }

            }
        }
    }
}
