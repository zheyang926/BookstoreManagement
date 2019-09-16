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
    public partial class MISManagerForm : Form
    {
        public MISManagerForm()
        {
            InitializeComponent();
        }
        private void MISManagerForm_Load(object sender, EventArgs e)
        {
            textBoxUserID.Enabled = false; // Disable user Id textBox bcz it is autoincrement in database.
            this.DisplayData();

            comboBoxSearch.Items.Add("UserID");
            comboBoxSearch.Items.Add("FirstName");
            comboBoxSearch.Items.Add("LastName");
            comboBoxSearch.Items.Add("UserName");
            comboBoxSearch.Items.Add("RoleID");

            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;
        }
        public void ClearTextBox()
        {
            textBoxUserID.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxRoleID.Clear();
            textBoxSearch.Clear();
        }
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxFirstName.Text) && !String.IsNullOrEmpty(textBoxLastName.Text)
                        && !String.IsNullOrEmpty(textBoxUserName.Text) && !String.IsNullOrEmpty(textBoxPassword.Text)
                        && !String.IsNullOrEmpty(textBoxRoleID.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }
        public void DisplayData()
        {
            MISManager mISManager = new MISManager();
            dataGridViewDisplay.DataSource = mISManager.ReadData();
            this.ClearTextBox();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                MISManager mISManager = new MISManager();
                mISManager.FirstName = textBoxFirstName.Text;
                mISManager.LastName = textBoxLastName.Text;
                mISManager.UserName = textBoxUserName.Text;
                mISManager.Password = textBoxPassword.Text;
                mISManager.RoleId = Convert.ToInt32(textBoxRoleID.Text);



                if (mISManager.SaveRecord(mISManager))  //! TRUE
                {
                    MessageBox.Show("Recored Inserted..");
                    this.DisplayData();
                }
                else
                {
                    MessageBox.Show("Recored Is Not Inserted.. \n" +
                        " OR \n Username is Exist -> " + mISManager.UserName,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.ClearTextBox();

            }
            else
            {
                MessageBox.Show("Please Enter Values. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            this.DisplayData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !String.IsNullOrEmpty(textBoxUserID.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MISManager mISManager = new MISManager();
                    mISManager.UserId = Convert.ToInt32(textBoxUserID.Text);
                    mISManager.FirstName = textBoxFirstName.Text;
                    mISManager.LastName = textBoxLastName.Text;
                    mISManager.UserName = textBoxUserName.Text;
                    mISManager.Password = textBoxPassword.Text;
                    mISManager.RoleId = Convert.ToInt32(textBoxRoleID.Text);

                    if (mISManager.UpdateRecord(mISManager))
                    {
                        MessageBox.Show("Record Updated.");
                        this.DisplayData();
                    }
                    else
                    {
                        MessageBox.Show("Record Is Not Updated.(Check Data or UserName Must be Unique)");
                    }
                }
                this.ClearTextBox();
            }
            else
            {
                MessageBox.Show("Please Select a Row For Update Operation.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !String.IsNullOrEmpty(textBoxUserID.Text))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MISManager mISManager = new MISManager();
                    mISManager.UserId = Convert.ToInt32(textBoxUserID.Text);

                    if (mISManager.DelRecord(mISManager))
                    {
                        MessageBox.Show("Deletion Completed.");
                    }
                    else
                    {
                        MessageBox.Show("Deletion Is Not Completed.");
                    }
                    this.DisplayData();
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
            }
        }

        private void dataGridViewDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewDisplay.Rows[e.RowIndex];
                textBoxUserID.Text = row.Cells["UserID"].Value.ToString();
                textBoxFirstName.Text = row.Cells["FirstName"].Value.ToString();
                textBoxLastName.Text = row.Cells["LastName"].Value.ToString();
                textBoxUserName.Text = row.Cells["UserName"].Value.ToString();
                textBoxPassword.Text = row.Cells["Password"].Value.ToString();
                textBoxRoleID.Text = row.Cells["RoleID"].Value.ToString();

            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBox();
            //this.DisplayData();
            dataGridViewDisplay.DataSource = null;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(2))
            {
                MISManager mISManager = new MISManager();

                if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[0])) //UserID
                {
                    mISManager.UserId = Convert.ToInt32(textBoxSearch.Text);
                }
                else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[1])) //FirstName
                {
                    mISManager.FirstName = textBoxSearch.Text;
                }
                else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[2])) // LastName
                {
                    mISManager.LastName = textBoxSearch.Text;
                }
                else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[3])) // UserName
                {
                    mISManager.UserName = textBoxSearch.Text;

                }
                else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[4])) // RoleID
                {
                    mISManager.RoleId = Convert.ToInt32(textBoxSearch.Text);

                }

                DataTable dataTable = mISManager.SearchRecord(mISManager);

                if (dataTable.Rows.Count != 0)
                {
                    dataGridViewDisplay.DataSource = dataTable;
                }
                else
                {
                    dataGridViewDisplay.DataSource = null;
                    MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                this.ClearTextBox();
            }
            else
            {
                MessageBox.Show("Please Enter the Data in Search Box.");
            }
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm();
            this.Hide();
            changePassword.ShowDialog(); // Open ChangePasswordForm
            this.Close();
        }
    }
}
