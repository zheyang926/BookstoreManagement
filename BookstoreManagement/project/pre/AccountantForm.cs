using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.pre
{
    public partial class AccountantForm : Form
    {
        public AccountantForm()
        {
            InitializeComponent();
        }
        private void DisplayData()
        {
            projectEntities project = new projectEntities();

            var display = project.invoices.Select(x => new
            {
                x.InvoiceID,
                x.TotalPrice,
                x.TypeOfPayment,
                x.PaymentDate,
                x.IsPaid
            });

            dataGridViewDisplay.DataSource = display.ToList();
        }
        public void ClearTextBox()
        {
            textBoxInvoiceID.Clear();
            textBoxTotalPrice.Clear();
            paymentDate.Value = DateTime.Now.Date;
            comboBoxTypeOfPayment.Text = comboBoxTypeOfPayment.Items[0] as string;
            comboBoxIsPaid.Text = comboBoxIsPaid.Items[0] as string;
            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;
            textBoxSearch.Clear();

        }
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxInvoiceID.Text) && !String.IsNullOrEmpty(textBoxTotalPrice.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }
        private void AccountantForm_Load(object sender, EventArgs e)
        {
            comboBoxTypeOfPayment.Items.Add("None");
            comboBoxTypeOfPayment.Items.Add("Credit Card");
            comboBoxTypeOfPayment.Items.Add("Debit Card");
            comboBoxTypeOfPayment.Items.Add("Cash");
            comboBoxTypeOfPayment.Text = comboBoxTypeOfPayment.Items[0] as string;

            comboBoxIsPaid.Items.Add("N");
            comboBoxIsPaid.Items.Add("Y");
            comboBoxIsPaid.Text = comboBoxIsPaid.Items[0] as string;

            comboBoxSearch.Items.Add("Invoice ID");
            comboBoxSearch.Items.Add("Date(MM--DD-YYYY)");
            comboBoxSearch.Items.Add("Payment Type");
            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;

            this.DisplayData();

            textBoxInvoiceID.Enabled = false;
            textBoxTotalPrice.Enabled = false;
            buttonAdd.Enabled = false;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                try
                {
                    projectEntities project = new projectEntities();
                    invoice invoice = new invoice();

                    invoice.InvoiceID = Convert.ToInt32(textBoxInvoiceID.Text);
                    invoice.TotalPrice = Convert.ToDouble(textBoxTotalPrice.Text);
                    invoice.TypeOfPayment = Convert.ToString(comboBoxTypeOfPayment.SelectedItem);
                    invoice.PaymentDate = paymentDate.Value.Date;
                    invoice.IsPaid = Convert.ToString(comboBoxIsPaid.SelectedItem);

                    project.invoices.Add(invoice);
                    project.SaveChanges();
                    this.DisplayData();
                    MessageBox.Show("Recored Inserted..");
                    this.ClearTextBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Recored Is Not Inserted..\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.ClearTextBoxCO();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Values. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        int invoiceID = Convert.ToInt32(textBoxInvoiceID.Text);
                        var checkInvoice = project.invoices.Find(invoiceID);

                        if (checkInvoice != null)
                        {
                            //checkInvoice.TotalPrice = Convert.ToDouble(textBoxTotalPrice.Text);
                            checkInvoice.PaymentDate = paymentDate.Value.Date;
                            checkInvoice.TypeOfPayment = comboBoxTypeOfPayment.SelectedItem.ToString();
                            checkInvoice.IsPaid = comboBoxIsPaid.SelectedItem.ToString();

                            project.SaveChanges();
                            this.DisplayData();
                            MessageBox.Show("Record Is Updated.");
                            this.ClearTextBox();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Recored Is Not Updated..\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //this.ClearTextBoxCO();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Update Operation.");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    projectEntities project = new projectEntities();

                    int invoiceID = Convert.ToInt32(textBoxInvoiceID.Text);
                    var checkInvoice = project.invoices.Find(invoiceID);

                    if (checkInvoice != null)
                    {
                        project.invoices.Remove(checkInvoice);

                        project.SaveChanges();
                        this.DisplayData();
                        MessageBox.Show("Record Is Updated.");
                        this.ClearTextBox();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
                this.ClearTextBox();
            }
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            this.DisplayData();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBox();
            dataGridViewDisplay.DataSource = null;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(2))
            {
                try
                {
                    projectEntities project = new projectEntities();

                    if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[0])) // Invoice ID
                    {
                        int invoiceID = Convert.ToInt32(textBoxSearch.Text);

                        var checkInvoice = project.invoices.Where(x => x.InvoiceID == invoiceID);


                        //MessageBox.Show("checkEmployee = " + checkEmployee);

                        if (checkInvoice.Count() != 0)
                        {
                            dataGridViewDisplay.DataSource = checkInvoice.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[1])) // Date(DD-MM-YYYY)
                    {
                        DateTime dateSearch = Convert.ToDateTime(textBoxSearch.Text);

                        var checkInvoice = project.invoices.Where(x => x.PaymentDate == dateSearch);

                        if (checkInvoice.Count() != 0)
                        {
                            dataGridViewDisplay.DataSource = checkInvoice.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[2])) // Payment Type
                    {
                        string typeOfPayment = textBoxSearch.Text;

                        var checkInvoice = project.invoices.Where(x => x.TypeOfPayment.Contains(typeOfPayment));

                        if (checkInvoice.Count() != 0)
                        {
                            dataGridViewDisplay.DataSource = checkInvoice.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Please Select and Enter Correct Search Field and Search \n" + ex.Message);
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
                textBoxInvoiceID.Text = row.Cells["InvoiceID"].Value.ToString();
                textBoxTotalPrice.Text = row.Cells["TotalPrice"].Value.ToString();

                if (row.Cells["PaymentDate"].Value != null)
                {
                    paymentDate.Text = row.Cells["PaymentDate"].Value.ToString();
                }
                comboBoxTypeOfPayment.Text = row.Cells["TypeOfPayment"].Value.ToString();
                comboBoxIsPaid.Text = row.Cells["IsPaid"].Value.ToString();
            }
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm();
            this.Hide();
            changePassword.ShowDialog(); // Open ChangePasswordForm
            this.Close();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxInvoiceID.Text))
            {
                projectEntities project = new projectEntities();
                int id = Convert.ToInt32(textBoxInvoiceID.Text);
                string txtFilePath = @"../../txt/" + id + ".txt";

                using (StreamWriter writer = File.CreateText(txtFilePath))
                {
                    var printInvoice = project.invoices.Find(id);

                    writer.WriteLine(printInvoice.InvoiceID + "|" + printInvoice.TotalPrice
                                     + "|" + printInvoice.PaymentDate + "|" + printInvoice.TypeOfPayment
                                     + "|" + printInvoice.IsPaid);
                    MessageBox.Show("Print Successful (the file is in project -> txt)");
                }
            }
            else
            {
                MessageBox.Show("Select a row to print.");
            }
        }
    }
}
