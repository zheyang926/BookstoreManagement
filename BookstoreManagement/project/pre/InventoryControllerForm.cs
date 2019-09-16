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
    public partial class InventoryControllerForm : Form
    {
        public InventoryControllerForm()
        {
            InitializeComponent();
        }


        //ClearTextBox
        public void ClearTextBox()
        {
            textBoxBookID.Clear();
            textBoxISBN.Clear();
            textBoxTitle.Clear();
            textBoxPrice.Clear();
            textBoxQoh.Clear();
            textBoxCategoryID.Clear();
            textBoxAuthorID.Clear();
            textBoxPublishID.Clear();
            publishYear.Value = DateTime.Now.Date;
            textBoxSearch.Clear();
            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;
        }


        private void DisplayBookInformation()
        {
            projectEntities project = new projectEntities();

            var Display = from bo in project.books
                          join au in project.authors on bo.AuthorID equals au.AuthorID
                          join pu in project.publishers on bo.PublishID equals pu.PublisherID
                          join ca in project.categories on bo.CategoryID equals ca.CategoryID
                          select new
                          {
                              bo.BookID,
                              bo.ISBN,
                              bo.Title,
                              bo.UnitPrice,
                              bo.YearPublished,
                              bo.QOH,
                              au.AuthorID,
                              pu.PublisherID,
                              pu.Name,
                              ca.CategoryID,
                              ca.CategoryName
                          };

            dataGridViewDisplayBook.DataSource = Display.ToList();

        }

        //Display
        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            this.DisplayBookInformation();
        }


        // Check TextBox Empty
        public bool ValidateEmptyTextBox(int checkNumber)
        {
            switch (checkNumber)
            {
                case 1:
                    return !String.IsNullOrEmpty(textBoxISBN.Text) && !String.IsNullOrEmpty(textBoxTitle.Text)
                        && !String.IsNullOrEmpty(textBoxPrice.Text) && !String.IsNullOrEmpty(textBoxQoh.Text)
                        && !String.IsNullOrEmpty(textBoxAuthorID.Text) && !String.IsNullOrEmpty(textBoxPublishID.Text)
                        && !String.IsNullOrEmpty(textBoxCategoryID.Text);
                case 2:
                    return !String.IsNullOrEmpty(textBoxSearch.Text);

                default:
                    //! Return false in default case 
                    return false;
            }
        }


        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {


            if (ValidateEmptyTextBox(1))
            {
                try
                {
                    projectEntities project = new projectEntities();

                    book book = new book();
                    book.ISBN = textBoxISBN.Text;
                    book.Title = textBoxTitle.Text;
                    book.UnitPrice = Convert.ToDouble(textBoxPrice.Text);
                    book.YearPublished = publishYear.Value.Date;
                    book.QOH = Convert.ToInt32(textBoxQoh.Text);
                    book.AuthorID = Convert.ToInt32(textBoxAuthorID.Text);
                    book.PublishID = Convert.ToInt32(textBoxPublishID.Text);
                    book.CategoryID = Convert.ToInt32(textBoxCategoryID.Text);


                    project.books.Add(book);

                    project.SaveChanges();
                    this.DisplayBookInformation();
                    MessageBox.Show("Recored Inserted..");
                    this.ClearTextBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Recored Is Not Inserted..\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ClearTextBox();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Values. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Form Load
        private void InventoryControllerForm_Load(object sender, EventArgs e)
        {
            textBoxBookID.Enabled = false;

            this.DisplayBookInformation();

            comboBoxSearch.Items.Add("BookID");
            comboBoxSearch.Items.Add("Title");
            comboBoxSearch.Items.Add("Category Name");
            comboBoxSearch.Items.Add("Author ID");
            comboBoxSearch.Items.Add("Publisher Name");
            

            comboBoxSearch.Text = comboBoxSearch.Items[0] as string;
        }


        //Update
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxBookID.Text))
            {
                if (MessageBox.Show("Do You Want to Update this Record", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        int bookid = Convert.ToInt32(textBoxBookID.Text);

                        var checkBookID = project.books.Find(bookid);

                        if (checkBookID != null)
                        {
                            checkBookID.ISBN = textBoxISBN.Text;
                            checkBookID.Title = textBoxTitle.Text;
                            checkBookID.UnitPrice = Convert.ToDouble(textBoxPrice.Text);
                            checkBookID.YearPublished = publishYear.Value.Date;
                            checkBookID.QOH = Convert.ToInt32(textBoxQoh.Text);
                            checkBookID.CategoryID = Convert.ToInt32(textBoxCategoryID.Text);
                            checkBookID.AuthorID = Convert.ToInt32(textBoxAuthorID.Text);
                            checkBookID.PublishID = Convert.ToInt32(textBoxPublishID.Text);

                            project.SaveChanges();
                            this.DisplayBookInformation();
                            MessageBox.Show("Record Is Updated.");
                            this.ClearTextBox();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Recored Is Not Updated..\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row For Update Operation.");
            }
        }

        private void dataGridViewDisplayBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewDisplayBook.Rows[e.RowIndex];
                textBoxBookID.Text = row.Cells["BookID"].Value.ToString();
                textBoxISBN.Text = row.Cells["ISBN"].Value.ToString();
                textBoxTitle.Text = row.Cells["Title"].Value.ToString();
                textBoxPrice.Text = row.Cells["UnitPrice"].Value.ToString();
                publishYear.Value = Convert.ToDateTime(row.Cells["YearPublished"].Value);
                textBoxQoh.Text = row.Cells["QOH"].Value.ToString();
                textBoxAuthorID.Text = row.Cells["AuthorID"].Value.ToString();
                textBoxPublishID.Text = row.Cells["PublisherID"].Value.ToString();
                textBoxCategoryID.Text = row.Cells["CategoryID"].Value.ToString();
            }
        }


        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(1) && !string.IsNullOrEmpty(textBoxBookID.Text))
            {
                if (MessageBox.Show("Do You Want to Delete this Record", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        projectEntities project = new projectEntities();

                        int bookid = Convert.ToInt32(textBoxBookID.Text);

                        var checkBookID = project.books.Find(bookid);

                        if (checkBookID != null)
                        {
                            project.books.Remove(checkBookID);
                            project.SaveChanges();
                            this.DisplayBookInformation();
                            MessageBox.Show("Deletion Completed.");
                            this.ClearTextBox();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Deletion Is Not Completed.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Clear All
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.ClearTextBox();
            dataGridViewDisplayBook.DataSource = null;
        }

        //Search
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (ValidateEmptyTextBox(2))
            {
                try
                {
                    projectEntities project = new projectEntities();

                    if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[0])) // BookID
                    {
                        int BookID = Convert.ToInt32(textBoxSearch.Text);

                        var checkBookID = from bo in project.books
                                          join au in project.authors on bo.AuthorID equals au.AuthorID
                                          join pu in project.publishers on bo.PublishID equals pu.PublisherID
                                          join ca in project.categories on bo.CategoryID equals ca.CategoryID
                                          where bo.BookID == BookID
                                          select new
                                          {
                                              bo.BookID,
                                              bo.ISBN,
                                              bo.Title,
                                              bo.UnitPrice,
                                              bo.YearPublished,
                                              bo.QOH,
                                              au.AuthorID,
                                              pu.PublisherID,
                                              pu.Name,
                                              ca.CategoryID,
                                              ca.CategoryName
                                          };

                        if (checkBookID.Count() != 0)
                        {
                            dataGridViewDisplayBook.DataSource = checkBookID.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[1])) // Title
                    {
                        string BookTitle = textBoxSearch.Text;

                        var checkBookTitle = from bo in project.books
                                             join au in project.authors on bo.AuthorID equals au.AuthorID
                                             join pu in project.publishers on bo.PublishID equals pu.PublisherID
                                             join ca in project.categories on bo.CategoryID equals ca.CategoryID
                                             where bo.Title.Contains(BookTitle) 
                                             select new
                                             {
                                                 bo.BookID,
                                                 bo.ISBN,
                                                 bo.Title,
                                                 bo.UnitPrice,
                                                 bo.YearPublished,
                                                 bo.QOH,
                                                 au.AuthorID,
                                                 pu.PublisherID,
                                                 pu.Name,
                                                 ca.CategoryID,
                                                 ca.CategoryName
                                             };

                        if (checkBookTitle.Count() != 0)
                        {
                            dataGridViewDisplayBook.DataSource = checkBookTitle.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[2])) // Category Name
                    {
                        string BookCategory = textBoxSearch.Text;

                        var checkBookCategory = from bo in project.books
                                                join au in project.authors on bo.AuthorID equals au.AuthorID
                                                join pu in project.publishers on bo.PublishID equals pu.PublisherID
                                                join ca in project.categories on bo.CategoryID equals ca.CategoryID
                                                where ca.CategoryName.Contains(BookCategory) 
                                                select new
                                                {
                                                    bo.BookID,
                                                    bo.ISBN,
                                                    bo.Title,
                                                    bo.UnitPrice,
                                                    bo.YearPublished,
                                                    bo.QOH,
                                                    au.AuthorID,
                                                    pu.PublisherID,
                                                    pu.Name,
                                                    ca.CategoryID,
                                                    ca.CategoryName
                                                };

                        if (checkBookCategory.Count() != 0)
                        {
                            dataGridViewDisplayBook.DataSource = checkBookCategory.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[3])) // Author ID
                    {
                        int AuthorID = Convert.ToInt32(textBoxSearch.Text);

                        var checkAuthorName = from bo in project.books
                                              join au in project.authors on bo.AuthorID equals au.AuthorID
                                              join pu in project.publishers on bo.PublishID equals pu.PublisherID
                                              join ca in project.categories on bo.CategoryID equals ca.CategoryID
                                              where au.AuthorID == AuthorID
                                              select new
                                              {
                                                  bo.BookID,
                                                  bo.ISBN,
                                                  bo.Title,
                                                  bo.UnitPrice,
                                                  bo.YearPublished,
                                                  bo.QOH,
                                                  au.AuthorID,
                                                  pu.PublisherID,
                                                  pu.Name,
                                                  ca.CategoryID,
                                                  ca.CategoryName
                                              };
                        if (checkAuthorName.Count() != 0)
                        {
                            dataGridViewDisplayBook.DataSource = checkAuthorName.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Record not Found.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else if (comboBoxSearch.Text == Convert.ToString(comboBoxSearch.Items[4])) // Publisher Name
                    {
                        string PublisherName = textBoxSearch.Text;

                        var checkPubliserName = from bo in project.books
                                                join au in project.authors on bo.AuthorID equals au.AuthorID
                                                join pu in project.publishers on bo.PublishID equals pu.PublisherID
                                                join ca in project.categories on bo.CategoryID equals ca.CategoryID
                                                where pu.Name.Contains(PublisherName)
                                                select new
                                                {
                                                    bo.BookID,
                                                    bo.ISBN,
                                                    bo.Title,
                                                    bo.UnitPrice,
                                                    bo.YearPublished,
                                                    bo.QOH,
                                                    au.AuthorID,
                                                    pu.PublisherID,
                                                    pu.Name,
                                                    ca.CategoryID,
                                                    ca.CategoryName
                                                };

                        if (checkPubliserName.Count() != 0)
                        {
                            dataGridViewDisplayBook.DataSource = checkPubliserName.ToList();
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

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm();
            this.Hide();
            changePassword.ShowDialog(); // Open ChangePasswordForm
            this.Close();
        }
    }

}
