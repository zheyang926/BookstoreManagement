using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.data
{
    public static class UtilityDB
    {
        public static SqlConnection ConnectDB()
        {
            SqlConnection connection = new SqlConnection("data source=(local);database=project;user=sa;password=demo");
            connection.Open();
            return connection;
        }
    }
}
