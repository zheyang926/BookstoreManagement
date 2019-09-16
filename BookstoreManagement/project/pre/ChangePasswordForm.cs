using project.bus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.pre
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "" || textBoxOldPassword.Text == "" ||
                textBoxNewPassword.Text == "" || textBoxConfirmPassword.Text == "")
            {
                MessageBox.Show("Can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User user = new User();
                user.Username = textBoxUserName.Text;
                user.Oldpassword = textBoxOldPassword.Text;
                user.Newpassword = textBoxNewPassword.Text;
                int count = user.CheckUser(user);
                if (count == 1 && textBoxNewPassword.Text == textBoxConfirmPassword.Text)
                {
                    if (user.UpdatePassword(user))
                    {
                        MessageBox.Show("Change Password Sucessfully");
                        this.Hide();
                        loginForm login = new loginForm();
                        login.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(" Change Password failed, user does not exist or old password is incorrect ",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
