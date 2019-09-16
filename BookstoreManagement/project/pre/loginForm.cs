using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project.bus;

namespace project.pre
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxUserName.Text) && !String.IsNullOrEmpty(textBoxPassword.Text);
                default:
                    return false;
            }
        }
        public void ClearTextBox()
        {
            textBoxPassword.Text = "";
            textBoxUserName.Text = "";
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1)) //! Condition is True
            {
                Login login = new Login();
                login.UserName = textBoxUserName.Text;
                login.Password = textBoxPassword.Text;
                int loginOrRoleID = login.CheckLogin(login);
                if (loginOrRoleID != -1)
                {
                    MessageBox.Show("Login Successful");

                    switch (loginOrRoleID)
                    {
                        case 1:
                            MISManagerForm misManager = new MISManagerForm();
                            this.Hide();
                            misManager.ShowDialog(); //! Open MISManager Form
                            this.Close();
                            break;

                        case 2:
                            SalesManagerForm salesManager = new SalesManagerForm();
                            this.Hide();
                            salesManager.ShowDialog(); //! Open SalesManager Form
                            this.Close();
                            break;
                        case 3:
                            OrderClerksForm orderClerks = new OrderClerksForm();
                            this.Hide();
                            orderClerks.ShowDialog(); //! Open OrderClerks Form
                            this.Close();
                            break;
                        case 4:
                            AccountantForm accountant = new AccountantForm();
                            this.Hide();
                            accountant.ShowDialog(); //! Open Accountant Form
                            this.Close();
                            break;
                        case 5:
                            InventoryControllerForm inventoryController = new InventoryControllerForm();
                            this.Hide();
                            inventoryController.ShowDialog(); //! Open InventoryController Form
                            this.Close();
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Login Unsuccessful");
                    textBoxPassword.Clear();
                    textBoxUserName.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                // Enter key pressed
                buttonLogin_Click(sender, e);
            }
        }
    }
}
