using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project.bus;

namespace project.pre
{
    public partial class SalesManagerForm : Form
    {

        DataSet dsProject = new DataSet("project");
        DataTable dtCustomer = new DataTable("customer");
        //DataTable dtCustomerSearch = new DataTable("customerSearch");

        public SalesManagerForm()
        {
            InitializeComponent();
        }
        public void ClearTextBox()
        {
            //foreach (Control control in Controls)
            //{
            //    if (control is TextBox)
            //    {
            //        control.Text = String.Empty;
            //    }
            //}

            textBoxCustomerID.Clear();
            textBoxCustomerName.Clear();
            textBoxStreet.Clear();
            textBoxCity.Clear();
            textBoxPostalCode.Clear();
            textBoxPhoneNumber.Clear();
            textBoxFaxNumber.Clear();
            textBoxCreditLimit.Clear();
            textBoxSearch.Clear();


            //comboBoxSearch.Text = comboBoxSearch.Items[0] as string;
        }
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxCustomerName.Text) && !String.IsNullOrEmpty(textBoxStreet.Text)
                        && !String.IsNullOrEmpty(textBoxCity.Text) && !String.IsNullOrEmpty(textBoxPostalCode.Text)
                        && !String.IsNullOrEmpty(textBoxPhoneNumber.Text) && !String.IsNullOrEmpty(textBoxFaxNumber.Text)
                        && !String.IsNullOrEmpty(textBoxCreditLimit.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }
        private void SalesManagerForm_Load(object sender, EventArgs e)
        {
            textBoxCustomerID.Enabled = false; // Disable CustomerID textBox 
            comboBoxSearch.Items.Add("CustomerID");
            comboBoxSearch.Items.Add("CustomerName");
            comboBoxSearch.Items.Add("City");
            comboBoxSearch.Items.Add("PostalCode");
            comboBoxSearch.Items.Add("PhoneNumber");
            comboBoxSearch.Items.Add("FaxNumber");

            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;

            SalesManager salesManager = new SalesManager();
            this.dsProject = salesManager.CreateDataSetTable(this.dsProject, this.dtCustomer);

            salesManager.ReadData(this.dsProject, this.dtCustomer);

            dataGridViewDisplay.DataSource = dtCustomer;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                SalesManager salesManager = new SalesManager();
                //salesManager.CustomerID = Convert.ToInt32(textBoxCustomerID.Text);
                salesManager.CustomerName = textBoxCustomerName.Text;
                salesManager.Street = textBoxStreet.Text;
                salesManager.City = textBoxCity.Text;
                salesManager.PostalCode = textBoxPostalCode.Text;
                salesManager.PhoneNumber = textBoxPhoneNumber.Text;
                salesManager.FaxNumber = textBoxFaxNumber.Text;
                salesManager.CreditLimit = float.Parse(textBoxCreditLimit.Text);

                if (salesManager.SaveRecord(this.dsProject, this.dtCustomer, salesManager))
                {
                    MessageBox.Show("Recored Inserted..");
                }
                else
                {
                    MessageBox.Show("Recored Is Not Inserted..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dataGridViewDisplay.DataSource = this.dtCustomer;
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBox();
            dataGridViewDisplay.DataSource = null;
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxCustomerID.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    SalesManager salesManager = new SalesManager();
                    salesManager.CustomerID = Convert.ToInt32(textBoxCustomerID.Text);
                    salesManager.CustomerName = textBoxCustomerName.Text;
                    salesManager.Street = textBoxStreet.Text;
                    salesManager.City = textBoxCity.Text;
                    salesManager.PostalCode = textBoxPostalCode.Text;
                    salesManager.PhoneNumber = textBoxPhoneNumber.Text;
                    salesManager.FaxNumber = textBoxFaxNumber.Text;
                    salesManager.CreditLimit = float.Parse(textBoxCreditLimit.Text);


                    if (salesManager.UpdateRecord(this.dsProject, this.dtCustomer, salesManager))
                    {
                        MessageBox.Show("Record Is Updated.");
                    }
                    else
                    {
                        MessageBox.Show("Recored Is Not Updated..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxCustomerID.Text))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SalesManager salesManager = new SalesManager();
                    salesManager.CustomerID = Convert.ToInt32(textBoxCustomerID.Text);

                    if (salesManager.DelRecord(dsProject, dtCustomer, salesManager))
                    {
                        MessageBox.Show("Deletion Completed.");
                    }
                    else
                    {
                        MessageBox.Show("Deletion Is Not Completed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(2))
            {
                try
                {
                    SalesManager salesManager = new SalesManager();

                    if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[0])) // CustomerID
                    {
                        salesManager.CustomerID = Convert.ToInt32(textBoxSearch.Text);
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[1])) // CustomerName
                    {
                        salesManager.CustomerName = textBoxSearch.Text;
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[2])) // City
                    {
                        salesManager.City = textBoxSearch.Text;
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[3])) // Postal Code
                    {
                        salesManager.PostalCode = textBoxSearch.Text;
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[4])) // Phone Number
                    {
                        salesManager.PhoneNumber = textBoxSearch.Text;
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[5])) // Fax Number
                    {
                        salesManager.FaxNumber = textBoxSearch.Text;
                    }

                    DataTable dataTable = salesManager.SearchRecord(dtCustomer, salesManager);

                    if (dataTable != null)
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
                catch (FormatException Ex)
                {
                    MessageBox.Show("Please Select and Enter Correct Search Field and Search ");
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Enter the Data in Search Box.");
            }
        }
        private void dataGridViewDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewDisplay.Rows[e.RowIndex];
                textBoxCustomerID.Text = row.Cells["CustomerID"].Value.ToString();
                textBoxCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                textBoxStreet.Text = row.Cells["Street"].Value.ToString();
                textBoxCity.Text = row.Cells["City"].Value.ToString();
                textBoxPostalCode.Text = row.Cells["PostalCode"].Value.ToString();
                textBoxPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();
                textBoxFaxNumber.Text = row.Cells["FaxNumber"].Value.ToString();
                textBoxCreditLimit.Text = row.Cells["CreditLimit"].Value.ToString();
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
