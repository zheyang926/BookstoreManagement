using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.data;

namespace project.bus
{
    public class Login
    {
        private string userName;
        private string password;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }

        public int CheckLogin(Login login)
        {
            return LoginDB.CheckLoginDB(login);
        }
    }
}
