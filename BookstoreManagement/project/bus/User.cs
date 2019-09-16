using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.data;

namespace project.bus
{
    public class User
    {
        private string username;
        private string password;
        private string oldpassword;
        private string newpassword;


        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Oldpassword { get => oldpassword; set => oldpassword = value; }
        public string Newpassword { get => newpassword; set => newpassword = value; }
        

        public int CheckUser(User user)
        {
            return ChangPasswordDB.CheckUser(user);
        }

        public bool UpdatePassword(User user)
        {

            return ChangPasswordDB.UpdatePassword(user);
        }


    }
}
