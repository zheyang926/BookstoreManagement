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
    public partial class OrderClerksForm : Form
    {
        public OrderClerksForm()
        {
            InitializeComponent();
        }
        public void ClearTextBoxCO()
        {
            textBoxCustomerOrderID1.Clear();
            textBoxCustomerID.Clear();
            orderDate.Value = DateTime.Now;
            textBoxCOSearch.Clear();
            comboBoxCOSearch.Text = comboBoxCOSearch.Items[0] as string;

        }
        public void ClearTextBoxBO()
        {
            textBoxCustomerOrderID2.Clear();
            textBoxBookID.Clear();
            textBoxOrderQuantity.Clear();
            textBoxBOSearch.Clear();
            comboBoxBOSearch.Text = comboBoxBOSearch.Items[0] as string;

        }
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxCustomerOrderID1.Text) && !String.IsNullOrEmpty(textBoxCustomerID.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxCOSearch.Text);
                case 3:
                    return !String.IsNullOrEmpty(textBoxCustomerOrderID2.Text) && !String.IsNullOrEmpty(textBoxBookID.Text)
                        && !String.IsNullOrEmpty(textBoxOrderQuantity.Text);
                case 4:
                    return !String.IsNullOrEmpty(textBoxBOSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }

        private void DisplayCustomerOrder()
        {
            projectEntities project = new projectEntities();
            // Display -> customerOrder using Lambda
            //var CODisplay = project.customerOrders.Select(x => new { x.CustomerOrderID, x.CustomerID, x.OrderDate });

            // Display -> customerOrder and customer using Lambda
            var CODisplay = project.customerOrders.Join(
                project.customers,   // Target Table Name               join __X__  on _____ = _____
                x1 => x1.CustomerID, // Before Condition of ON Cause    join _____  on __X__ = _____
                x2 => x2.CustomerID, // After Condition of ON Cause     join _____  on _____ = __X__
                (x1, x2) => new   // Select
                {
                    x1.CustomerOrderID,
                    x1.CustomerID,
                    x1.OrderDate,
                    x2.CustomerName
                });

            var displayCustomer = project.customers.Select(x => x);


            // Display -> customerOrder using LINQ
            //var CODisplay = from co in project.customerOrders
            //                select co;

            // Display -> customerOrder and customer using LINQ
            //var CODisplay = from co in project.customerOrders
            //                join cu in project.customers on co.CustomerID equals cu.CustomerID
            //                select new { co.CustomerOrderID,co.CustomerID,co.OrderDate,
            //                             cu.CustomerName };

            dataGridViewCustomerOrder.DataSource = CODisplay.ToList();
            dataGridViewCOSearch.DataSource = displayCustomer.ToList();
        }
        private void DisplayBookOrder()
        {
            projectEntities project = new projectEntities();
            //var BODisplay = project.bookOrders.Select(x => new { x.CustomerOrderID, x.BookID, x.OrderQuality });


            var BODisplay = project.bookOrders.Join(
                project.books,   // Target Table Name               join __X__  on _____ = _____
                x1 => x1.BookID, // Before Condition of ON Cause    join _____  on __X__ = _____
                x2 => x2.BookID, // After Condition of ON Cause     join _____  on _____ = __X__
                (x1, x2) => new   // Select
                {
                    x1.CustomerOrderID,
                    x1.BookID,
                    x1.OrderQuantity,
                    x2.Title
                });

            var displayBook = project.books.Select(x => x);

            dataGridViewBookOrder.DataSource = BODisplay.ToList();
            dataGridViewBOSearch.DataSource = displayBook.ToList();
        }
        private void OrderClerksForm_Load(object sender, EventArgs e)
        {
            textBoxCustomerOrderID1.Enabled = false;

            this.DisplayCustomerOrder();
            this.DisplayBookOrder();

            comboBoxCOSearch.Items.Add("CustomerOrderID");
            comboBoxCOSearch.Items.Add("CustomerID(Customer Order)");
            comboBoxCOSearch.Items.Add("CustomerID(Customer)");
            comboBoxCOSearch.Items.Add("CustomerName(Customer Order)");
            comboBoxCOSearch.Items.Add("CustomerName(Customer)");
            comboBoxCOSearch.Text = comboBoxCOSearch.Items[0] as string;

            comboBoxBOSearch.Items.Add("CustomerOrderID");
            comboBoxBOSearch.Items.Add("BookID(Book Order)");
            comboBoxBOSearch.Items.Add("BookID(Book)");
            comboBoxBOSearch.Items.Add("BookName(Book Order)");
            comboBoxBOSearch.Items.Add("BookName(Book)");
            comboBoxBOSearch.Text = comboBoxBOSearch.Items[0] as string;
        }

        private void buttonCustomerOrderAdd_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1))
            {
                try
                {
                    projectEntities project = new projectEntities();
                    customerOrder cOrder = new customerOrder();
                    cOrder.CustomerID = Convert.ToInt32(textBoxCustomerID.Text);
                    cOrder.OrderDate = orderDate.Value.Date;
                    project.customerOrders.Add(cOrder);
                    project.SaveChanges();
                    this.DisplayCustomerOrder();
                    MessageBox.Show("Recored Inserted..");
                    this.ClearTextBoxCO();
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

        private void buttonCustomerOrderUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxCustomerOrderID1.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        int customerOrderID = Convert.ToInt32(textBoxCustomerOrderID1.Text);
                        // var = customerOrder
                        var checkCustomerOrder = project.customerOrders.Find(customerOrderID);
                        //MessageBox.Show("checkEmployee = " + checkEmployee);

                        if (checkCustomerOrder != null)
                        {
                            checkCustomerOrder.CustomerID = Convert.ToInt32(textBoxCustomerID.Text);
                            checkCustomerOrder.OrderDate = orderDate.Value.Date;
                            project.SaveChanges();
                            this.DisplayCustomerOrder();
                            MessageBox.Show("Record Is Updated.");
                            this.ClearTextBoxCO();
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

        private void buttonCustomerOrderDelete_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxCustomerID.Text))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();
                        int customerOrderID = Convert.ToInt32(textBoxCustomerOrderID1.Text);
                        // var = customerOrder
                        var checkCustomerOrder = project.customerOrders.Find(customerOrderID);

                        if (checkCustomerOrder != null)
                        {
                            project.customerOrders.Remove(checkCustomerOrder);
                            project.SaveChanges();
                            this.DisplayCustomerOrder();
                            MessageBox.Show("Deletion Completed.");
                            this.ClearTextBoxCO();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Deletion Is Not Completed.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ClearTextBoxCO();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
                this.ClearTextBoxCO();
            }

        }

        private void buttonCustomerOrderDisplay_Click(object sender, EventArgs e)
        {
            this.DisplayCustomerOrder();
        }

        private void buttonCustomerOrderClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBoxCO();
            dataGridViewCOSearch.DataSource = null;
            dataGridViewCustomerOrder.DataSource = null;
        }

        private void dataGridViewCustomerOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewCustomerOrder.Rows[e.RowIndex];
                textBoxCustomerOrderID1.Text = row.Cells["CustomerOrderID"].Value.ToString();
                textBoxCustomerID.Text = row.Cells["CustomerID"].Value.ToString();
                orderDate.Text = row.Cells["OrderDate"].Value.ToString();
            }
        }

        private void buttonCustomerOrderSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(2))
            {
                try
                {
                    projectEntities project = new projectEntities();

                    if (comboBoxCOSearch.Text == Convert.ToString(comboBoxCOSearch.Items[0])) // CustomerOrderID
                    {
                        int customerOrderID = Convert.ToInt32(textBoxCOSearch.Text);

                        var checkCustomerOrder = project.customerOrders.Join(
                        project.customers,   // Target Table Name               join __X__  on _____ = _____
                        x1 => x1.CustomerID, // Before Condition of ON Cause    join _____  on __X__ = _____
                        x2 => x2.CustomerID, // After Condition of ON Cause     join _____  on _____ = __X__
                        (x1, x2) => new   // Select
                        {
                            x1.CustomerOrderID,
                            x1.CustomerID,
                            x1.OrderDate,
                            x2.CustomerName
                        }).Where(x => x.CustomerOrderID == customerOrderID);

                        //MessageBox.Show("checkEmployee = " + checkEmployee);

                        if (checkCustomerOrder.Count() != 0)
                        {
                            dataGridViewCOSearch.DataSource = checkCustomerOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxCOSearch.Text == Convert.ToString(comboBoxCOSearch.Items[1])) // CustomerID(CustomerOrder)
                    {
                        int customerID = Convert.ToInt32(textBoxCOSearch.Text);

                        var checkCustomerOrder = project.customerOrders.Join(
                        project.customers,   // Target Table Name               join __X__  on _____ = _____
                        x1 => x1.CustomerID, // Before Condition of ON Cause    join _____  on __X__ = _____
                        x2 => x2.CustomerID, // After Condition of ON Cause     join _____  on _____ = __X__
                        (x1, x2) => new   // Select
                        {
                            x1.CustomerOrderID,
                            x1.CustomerID,
                            x1.OrderDate,
                            x2.CustomerName
                        }).Where(x => x.CustomerID == customerID);

                        if (checkCustomerOrder.Count() != 0)
                        {
                            dataGridViewCOSearch.DataSource = checkCustomerOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxCOSearch.Text == Convert.ToString(comboBoxCOSearch.Items[2])) // CustomerID(Customer)
                    {
                        int customerID = Convert.ToInt32(textBoxCOSearch.Text);

                        var checkCustomer = project.customers.Where(x => x.CustomerID == customerID).Select(x => x);

                        if (checkCustomer.Count() != 0)
                        {
                            dataGridViewCOSearch.DataSource = checkCustomer.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxCOSearch.Text == Convert.ToString(comboBoxCOSearch.Items[3])) // CustomerName(CustomerOrder)
                    {
                        string customerName = textBoxCOSearch.Text;

                        var checkCustomerOrder = project.customerOrders.Join(
                        project.customers,   // Target Table Name               join __X__  on _____ = _____
                        x1 => x1.CustomerID, // Before Condition of ON Cause    join _____  on __X__ = _____
                        x2 => x2.CustomerID, // After Condition of ON Cause     join _____  on _____ = __X__
                        (x1, x2) => new   // Select
                        {
                            x1.CustomerOrderID,
                            x1.CustomerID,
                            x1.OrderDate,
                            x2.CustomerName
                        }).Where(x => x.CustomerName.Contains(customerName));

                        if (checkCustomerOrder.Count() != 0)
                        {
                            dataGridViewCOSearch.DataSource = checkCustomerOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxCOSearch.Text == Convert.ToString(comboBoxCOSearch.Items[4])) // CustomerName(Customer)
                    {
                        string customerName = textBoxCOSearch.Text;

                        var checkCustomer = project.customers.Where(x => x.CustomerName.Contains(customerName));

                        if (checkCustomer.Count() != 0)
                        {
                            dataGridViewCOSearch.DataSource = checkCustomer.ToList();
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
                    this.ClearTextBoxCO();
                }
            }
            else
            {
                MessageBox.Show("Please Enter the Data in Search Box.");
            }
        }

        //=========================================================================================================
        //==================================== BOOK ORDER =========================================================

        int cOrderIDUpdate;
        int bookIDUpdate;
        int orderQuantityUpdate;
        private void buttonBookOrderAdd_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(3))
            {
                try
                {
                    projectEntities project = new projectEntities();
                    bookOrder bOrder = new bookOrder();
                    bOrder.CustomerOrderID = Convert.ToInt32(textBoxCustomerOrderID2.Text);
                    bOrder.BookID = Convert.ToInt32(textBoxBookID.Text);
                    bOrder.OrderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);

                    int checkBookID = Convert.ToInt32(textBoxBookID.Text);
                    int customerOrderID = Convert.ToInt32(textBoxCustomerOrderID2.Text);
                    // Find Book
                    var checkBook = project.books.Find(checkBookID);

                    if (Convert.ToInt32(textBoxOrderQuantity.Text) <= checkBook.QOH)
                    {
                        project.bookOrders.Add(bOrder);

                        // Update the book qoh 
                        int newBookQOH = checkBook.QOH - Convert.ToInt32(textBoxOrderQuantity.Text);
                        checkBook.QOH = newBookQOH;

                        // Check in invoice table id is exist or not 
                        double unitPrice = checkBook.UnitPrice;
                        int orderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);
                        if (project.invoices.Any(x => x.InvoiceID == customerOrderID))
                        {
                            // update to invoice table
                            var updateInvoice = project.invoices.Find(customerOrderID);

                            double oldTotalPrice = updateInvoice.TotalPrice;
                            double newTotalPrice = oldTotalPrice + (unitPrice * orderQuantity);
                            MessageBox.Show(" newTotalPrice " + newTotalPrice + " customerOrderID " + customerOrderID);
                            updateInvoice.TotalPrice = newTotalPrice;
                        }
                        else
                        {
                            // insert to invoice table
                            invoice invoice = new invoice();
                            invoice.InvoiceID = customerOrderID;
                            invoice.TotalPrice = (unitPrice * orderQuantity);
                            invoice.TypeOfPayment = "None";
                            //DateTime date = new DateTime(1111, 11, 11);
                            //invoice.PaymentDate = date;
                            invoice.IsPaid = "n";
                            project.invoices.Add(invoice);
                        }


                        project.SaveChanges();
                        MessageBox.Show("Recored Inserted..");
                        this.DisplayBookOrder();
                        this.ClearTextBoxBO();
                    }
                    else
                    {
                        string msg = string.Format(" We only have {0} books for \n " +
                            " Book Id = {1} \n " +
                            " Book Name = {2} ",
                            checkBook.QOH, Convert.ToInt32(textBoxBookID.Text), checkBook.Title);
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


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

        private void buttonBookOrderUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(3) && !string.IsNullOrEmpty(textBoxCustomerOrderID2.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        var checkBookOrder = project.bookOrders.Where(x => x.CustomerOrderID == cOrderIDUpdate
                        && x.BookID == bookIDUpdate && x.OrderQuantity == orderQuantityUpdate).SingleOrDefault();

                        if (checkBookOrder != null)
                        {
                            // check Book
                            var checkBook = project.books.Find(bookIDUpdate);

                            //  old(book)   order(bookorder)    new (book)
                            //  150         100                 50
                            //  1) 50       80                  70
                            //  2) 50       110                 40
                            int oldBookQOH = checkBook.QOH;
                            int oldBookOrderQuantity = orderQuantityUpdate;
                            int newBookOrderQuantity = Convert.ToInt32(textBoxOrderQuantity.Text);

                            // update to invoice table
                            var updateInvoice = project.invoices.Find(cOrderIDUpdate);
                            //double unitPrice = checkBook.UnitPrice;
                            double oldtotalPrice = checkBook.UnitPrice * oldBookOrderQuantity;
                            double newtotalPrice = checkBook.UnitPrice * newBookOrderQuantity;

                            if (newBookOrderQuantity > oldBookOrderQuantity)
                            {
                                // Book QOH
                                int newBookQOH = oldBookQOH - (newBookOrderQuantity - oldBookOrderQuantity);
                                checkBook.QOH = newBookQOH;

                                // Invoice TotalPrice
                                double totalPrice = updateInvoice.TotalPrice + (newtotalPrice - oldtotalPrice);
                                updateInvoice.TotalPrice = totalPrice;
                                MessageBox.Show("more " + totalPrice);
                            }
                            else if (newBookOrderQuantity < oldBookOrderQuantity)
                            {
                                // Book QOH
                                int newBookQOH = oldBookQOH + (oldBookOrderQuantity - newBookOrderQuantity);
                                checkBook.QOH = newBookQOH;

                                // Invoice TotalPrice
                                double totalPrice = updateInvoice.TotalPrice - (oldtotalPrice - newtotalPrice);
                                updateInvoice.TotalPrice = totalPrice;
                                MessageBox.Show("less " + totalPrice);

                            }

                            string query = string.Format(" Update bookOrder " +
                                " set CustomerOrderID = {0} , BookID = {1} , OrderQuantity = {2} where " +
                                " CustomerOrderID = {3} and BookID = {4} and OrderQuantity = {5} ",
                                Convert.ToInt32(textBoxCustomerOrderID2.Text), Convert.ToInt32(textBoxBookID.Text),
                                newBookOrderQuantity, cOrderIDUpdate,
                                bookIDUpdate, orderQuantityUpdate);

                            project.Database.ExecuteSqlCommand(query);

                            project.SaveChanges();

                            MessageBox.Show("Record Is Updated.");
                            this.DisplayBookOrder();
                            this.ClearTextBoxBO();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Recored Is Not Updated.. \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //this.ClearTextBoxCO();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Update Operation.");
            }
        }

        private void buttonBookOrderDelete_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(3))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        var checkBookOrder = project.bookOrders.Where(x => x.CustomerOrderID == cOrderIDUpdate
                         && x.BookID == bookIDUpdate && x.OrderQuantity == orderQuantityUpdate).SingleOrDefault();

                        if (checkBookOrder != null)
                        {
                            project.bookOrders.Remove(checkBookOrder);

                            // add Book quantity to QOH(book) when delete.
                            var checkBook = project.books.Find(bookIDUpdate);
                            checkBook.QOH = checkBook.QOH + orderQuantityUpdate;

                            // remove price of that book order in invoice table
                            var checkInvoice = project.invoices.Find(cOrderIDUpdate);
                            checkInvoice.TotalPrice = checkInvoice.TotalPrice - (checkBook.UnitPrice * orderQuantityUpdate);

                            project.SaveChanges();
                            MessageBox.Show("Deletion Completed.");
                            this.DisplayBookOrder();
                            this.ClearTextBoxBO();
                        }
                        else
                        {
                            MessageBox.Show("Deletion Is Not Completed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ClearTextBoxBO();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Deletion Is Not Completed.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ClearTextBoxCO();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Delete Operation.");
                this.ClearTextBoxCO();
            }
        }

        private void buttonBookOrderDisplay_Click(object sender, EventArgs e)
        {
            this.DisplayBookOrder();
        }

        private void buttonBookOrderClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBoxBO();
            dataGridViewBookOrder.DataSource = null;
            dataGridViewBOSearch.DataSource = null;
        }

        private void buttonBookOrderSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(4))
            {
                try
                {
                    projectEntities project = new projectEntities();

                    if (comboBoxBOSearch.Text == Convert.ToString(comboBoxBOSearch.Items[0])) // CustomerOrderID
                    {
                        int customerOrderID = Convert.ToInt32(textBoxBOSearch.Text);

                        var checkBookOrder = project.bookOrders.Join(
                        project.books,   // Target Table Name               join __X__  on _____ = _____
                        x1 => x1.BookID, // Before Condition of ON Cause    join _____  on __X__ = _____
                        x2 => x2.BookID, // After Condition of ON Cause     join _____  on _____ = __X__
                        (x1, x2) => new   // Select
                        {
                            x1.CustomerOrderID,
                            x1.BookID,
                            x1.OrderQuantity,
                            x2.Title
                        }).Where(x => x.CustomerOrderID == customerOrderID);

                        if (checkBookOrder.Count() != 0)
                        {
                            dataGridViewBOSearch.DataSource = checkBookOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxBOSearch.Text == Convert.ToString(comboBoxBOSearch.Items[1])) // BookID(BookOrder)
                    {
                        int bookID = Convert.ToInt32(textBoxBOSearch.Text);

                        var checkBookOrder = project.bookOrders.Join(
                         project.books,   // Target Table Name               join __X__  on _____ = _____
                         x1 => x1.BookID, // Before Condition of ON Cause    join _____  on __X__ = _____
                         x2 => x2.BookID, // After Condition of ON Cause     join _____  on _____ = __X__
                         (x1, x2) => new   // Select
                         {
                             x1.CustomerOrderID,
                             x1.BookID,
                             x1.OrderQuantity,
                             x2.Title
                         }).Where(x => x.BookID == bookID);

                        if (checkBookOrder.Count() != 0)
                        {
                            dataGridViewBOSearch.DataSource = checkBookOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxBOSearch.Text == Convert.ToString(comboBoxBOSearch.Items[2])) // BookID(Book)
                    {
                        int bookID = Convert.ToInt32(textBoxBOSearch.Text);

                        var checkBook = project.books.Where(x => x.BookID == bookID);

                        if (checkBook.Count() != 0)
                        {
                            dataGridViewBOSearch.DataSource = checkBook.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxBOSearch.Text == Convert.ToString(comboBoxBOSearch.Items[3])) // BookName(Book Order)
                    {
                        string bookName = textBoxBOSearch.Text;

                        var checkBookOrder = project.bookOrders.Join(
                         project.books,   // Target Table Name               join __X__  on _____ = _____
                         x1 => x1.BookID, // Before Condition of ON Cause    join _____  on __X__ = _____
                         x2 => x2.BookID, // After Condition of ON Cause     join _____  on _____ = __X__
                         (x1, x2) => new   // Select
                         {
                             x1.CustomerOrderID,
                             x1.BookID,
                             x1.OrderQuantity,
                             x2.Title
                         }).Where(x => x.Title.Contains(bookName));

                        if (checkBookOrder.Count() != 0)
                        {
                            dataGridViewBOSearch.DataSource = checkBookOrder.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxBOSearch.Text == Convert.ToString(comboBoxBOSearch.Items[4])) // BookName(Book)
                    {
                        string bookName = textBoxBOSearch.Text;

                        var checkBook = project.books.Where(x => x.Title.Contains(bookName));

                        if (checkBook.Count() != 0)
                        {
                            dataGridViewBOSearch.DataSource = checkBook.ToList();
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
                    this.ClearTextBoxCO();
                }
            }
            else
            {
                MessageBox.Show("Please Enter the Data in Search Box.");
            }
        }

        private void dataGridViewBookOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewBookOrder.Rows[e.RowIndex];
                textBoxCustomerOrderID2.Text = row.Cells["CustomerOrderID"].Value.ToString();
                textBoxBookID.Text = row.Cells["BookID"].Value.ToString();
                textBoxOrderQuantity.Text = row.Cells["OrderQuantity"].Value.ToString();

                cOrderIDUpdate = Convert.ToInt32(row.Cells["CustomerOrderID"].Value);
                bookIDUpdate = Convert.ToInt32(row.Cells["BookID"].Value);
                orderQuantityUpdate = Convert.ToInt32(row.Cells["OrderQuantity"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm();
            this.Hide();
            changePassword.ShowDialog(); // Open ChangePasswordForm
            this.Close();
        }
    }
}
