using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.data;
using System.Data;
using System.Data.SqlClient;

namespace project.bus
{
    public class MISManager
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string userName;
        private string password;
        private int roleId;

        public int UserId { get => userId; set => userId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public int RoleId { get => roleId; set => roleId = value; }


        public DataTable ReadData()
        {
            return MISManagerDB.ReadDataDB();
        }
        //public bool CheckUserName(string UserName)
        //{
        //    return MISManagerDB.CheckUserNameDB(UserName);
        //}
        public bool SaveRecord(MISManager manager)
        {
            return MISManagerDB.SaveRecordDB(manager);
        }
        public bool UpdateRecord(MISManager manager)
        {
            return MISManagerDB.UpdateRecordDB(manager);
        }
        public DataTable SearchRecord(MISManager manager)
        {
            return MISManagerDB.SearchRecordDB(manager);
        }
        public bool DelRecord(MISManager manager)
        {
            return MISManagerDB.DelRecoredDB(manager);
        }

    }
}
