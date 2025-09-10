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
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Windows.Forms;

namespace CMPG223_Project1
{
    public partial class frmBook : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\telePROJECTS\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        // string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Models\GitProject\CMPG223-Project1\CMPG223-Project1\CMPG223-Project1\BookDatabase.mdf;Integrated Security=True";
        
        private System.Windows.Forms.ToolTip toolTip1;
        public frmBook()
        {
            InitializeComponent();
            LoadBookGrid();
            LoadAuthorCombobox();
            LoadBooks();
            LoadComboBox();
            HelpToolTip();

            cmbSearchBy.SelectedIndex = 0; // Default to FirstName
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (ValidateBookForm())
            {
                if (cbxAuthorId.SelectedValue == null)
                {
                    MessageBox.Show("Please select an Author.");
                    return;
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    int authorId = Convert.ToInt32(cbxAuthorId.SelectedValue);
                    bool status = cbxStatu1.SelectedItem != null && cbxStatu1.SelectedItem.ToString() == "Available";

                    string query = "INSERT INTO Books (AuthorId, Title, Genre, Author, Phone_No, Published_Year, Status) " +
                                   "VALUES (@AuthorId, @Title, @Genre, @Author, @Phone_No, @Published_Year, @Status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AuthorId", authorId);
                    cmd.Parameters.AddWithValue("@Title", txtTitle1.Text);
                    cmd.Parameters.AddWithValue("@Genre", txtGenre1.Text);
                    cmd.Parameters.AddWithValue("@Author", txtAuthor11.Text);
                    cmd.Parameters.AddWithValue("@Phone_No", txtPhoneNum.Text);
                    cmd.Parameters.AddWithValue("@Published_Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Status", status);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book added successfully!");
                        LoadBooks();
                        LoadBookGrid();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        private void ClearForm()
        {
            txtTitle1.Clear();
            txtGenre1.Clear();
            txtAuthor11.Clear();
            txtYear.Clear();
            txtPhoneNum.Clear();
            cbxStatu1.SelectedIndex = -1;
        }
        private void LoadComboBox()
        {
            cbxStatu1.Items.Clear();
            cbxStatu1.Items.AddRange(new string[] { "Available", "Not Available" });
        }
        private void LoadBookGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Books";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Books");

                    //Adding the data into the data grid
                    dgvBooks.DataSource = ds;
                    dgvBooks.DataMember = "Books";

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateBookForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle1.Text) ||
                string.IsNullOrWhiteSpace(txtGenre1.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor11.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNum.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                cbxStatu1.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }
        private void LoadAuthorCombobox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //Author Combo Box
                string sqlBook = "SELECT AuthorId, LastName FROM Author";
                SqlDataAdapter daBook = new SqlDataAdapter(sqlBook, conn);
                DataTable dtBook = new DataTable();
                daBook.Fill(dtBook);
                cbxAuthorId.DataSource = dtBook;
                cbxAuthorId.DisplayMember = "LastName";
                cbxAuthorId.ValueMember = "AuthorId";
            }
        }
        private void LoadBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT B.BookId, 
                                          A.LastName AS AuthorLastName,
                                          B.Title,
                                          B.Genre,
                                          B.Author,
                                          B.Phone_No,
                                          B.Published_Year,
                                          B.Status
                                FROM Books B
                                INNER JOIN Author A ON B.AuthorId = A.AuthorId";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Books");
                    dgvBooks.DataSource = ds;
                    dgvBooks.DataMember = "Books";

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Books: ", ex.Message);
            }
        }
        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow == null) return;
            try
            {
                // Get updated values from form
                string title = txtTitle1.Text;
                string genre = txtGenre1.Text;
                string author = txtAuthor11.Text;
                string phoneNo = txtPhoneNum.Text;
                string year = txtYear.Text;


                // Convert ComboBox selections to boolean
                bool status = cbxStatu1.SelectedItem != null && cbxStatu1.SelectedItem.ToString() == "Available";
                int bookId = Convert.ToInt32(dgvBooks.CurrentRow.Cells["BookId"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Update the first member with this FirstName
                    string sql = @"
                UPDATE Books
                SET AuthorId = @AuthorId,
                    Title = @Title,
                    Genre = @Genre,
                    Author = @Author,
                    Phone_No = @Phone_No,
                    Published_Year = @Published_Year,
                    Status = @Status
                WHERE BookId = @BookId";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@AuthorId", cbxAuthorId.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Genre", genre);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Phone_No", phoneNo);
                    cmd.Parameters.AddWithValue("@Published_Year", year);
                    cmd.Parameters.AddWithValue("@Status", status);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book updated successfully.");
                        LoadBooks();
                        LoadBookGrid(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No Book found with that Title.");
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error updating Book", ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedField = cmbSearchBy.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(selectedField)) return;

                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    string sql = $"SELECT * FROM Books WHERE {selectedField} LIKE @searchText";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + txtSearch.Text + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Books");

                        dgvBooks.DataSource = ds;
                        dgvBooks.DataMember = "Books";
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvBooks.Rows[e.RowIndex];

            // Populate text boxes
            //cbxAuthorId.Text = row
            txtTitle1.Text = row.Cells["Title"].Value.ToString();
            txtGenre1.Text = row.Cells["Genre"].Value.ToString();
            txtAuthor11.Text = row.Cells["Author"].Value.ToString();
            txtPhoneNum.Text = row.Cells["Phone_No"].Value.ToString();
            txtYear.Text = row.Cells["Published_Year"].Value.ToString();

            // Populate ComboBoxes (Yes/No)
            cbxStatu1.Text = Convert.ToBoolean(row.Cells["Status"].Value) ? "Available" : "Not Available";
        }

        private void cbxStatu1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check Status selection
            if (cbxStatu1.SelectedItem == null)
            {
                MessageBox.Show("Please select Available or Not Available for a Book.");
                return; // stop further execution
            }

            // Safe conversion to string and boolean
            string status = cbxStatu1.SelectedItem?.ToString();
            bool bookValue = status == "Available";

            // Message and optionally enable/disable related controls (not the ComboBox itself)
            if (bookValue)
            {
                MessageBox.Show("The book you have chosen is Available.");
            }
            else
            {
                MessageBox.Show("The book you have chosen is Not Available.");
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBooks.CurrentRow == null) return;
                //string bookidToDelete = txtTitle1.Text;
                int bookidToDelete = Convert.ToInt32(dgvBooks.CurrentRow.Cells["BookId"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Delete the first member matching the first name
                    string sql = @"DELETE TOP(1) FROM Books WHERE BookId = @BookId";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@BookId", bookidToDelete);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Book deleted successfully.");
                        LoadBookGrid(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete a Book.");
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error unable to load delete fuction: ", ex.Message);
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            txtTitle1.Clear();
            txtGenre1.Clear();
            cbxStatu1.SelectedValue = -1;
            txtYear.Clear();
            txtAuthor11.Clear();
            txtPhoneNum.Clear();
            cbxAuthorId.SelectedValue = -1;
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.ShowDialog();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtGenre1_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtGenre1.Text, @"^[a-zA-Z]+$"))
            {
                    //txtGenre1.Text = txtGenre1.Text.Remove(txtGenre1.Text.Length - 1);
                    txtGenre1.SelectionStart = txtGenre1.Text.Length; // keep cursor at end
                    errorProvider1.SetError(txtGenre1, "Genre must only contain letters.");
            }
            else
            {
                errorProvider1.SetError(txtGenre1, "");
            }
        }

        private void txtAuthor11_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtAuthor11.Text, @"^[a-zA-Z]+$"))
            {
                //txtAuthor11.Text = txtAuthor11.Text.Remove(txtAuthor11.Text.Length - 1);
                txtAuthor11.SelectionStart = txtAuthor11.Text.Length; // keep cursor at end
                errorProvider1.SetError(txtAuthor11, "Author must only contain letters.");
            }
            else
            {
                errorProvider1.SetError(txtAuthor11, "");
            }
        }

        private void txtPhoneNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // ignore the key
                                  // MessageBox.Show("Phone number must only contain digits.");
                errorProvider1.SetError(txtPhoneNum, "Phone number must only contain digits.");
            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // ignore the key
                errorProvider1.SetError(txtYear, "Year must only contain digits.");
            }
        }
        //ToolTip toolTip2 = new ToolTip();
        Dictionary<string, string> helpDictionary = new Dictionary<string, string>();
        private void HelpToolTip()
        {
            toolTipHelp = new System.Windows.Forms.ToolTip
            {
                IsBalloon = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Help"
            };

            // Assign ToolTips
            toolTipHelp.SetToolTip(txtTitle1, "Enter Title of the book");
            toolTipHelp.SetToolTip(txtGenre1, "Enter Genre of a book");
            toolTipHelp.SetToolTip(txtAuthor11, "Enter Author of a book");
            toolTipHelp.SetToolTip(txtPhoneNum, "Enter phone number using digits only");
            toolTipHelp.SetToolTip(cbxAuthorId, "Choose the author using their last name");
            toolTipHelp.SetToolTip(cbxStatu1, "Choose available or not available");
            toolTipHelp.SetToolTip(btnAddBook, "Click to Add a book");
            toolTipHelp.SetToolTip(btnUpdateBook, "Click to Update a book");
            toolTipHelp.SetToolTip(btnDeleteBook, "Click to Delete a book");
            toolTipHelp.SetToolTip(cmbSearchBy, "Choose what to search database by");
            toolTipHelp.SetToolTip(txtSearch, "Enter text to search database");
            toolTipHelp.SetToolTip(btnBack1, "Click to return to Home/Admin Form");
            toolTipHelp.SetToolTip(dgvBooks, "Display all saved book's details");
            toolTipHelp.SetToolTip(btnClear1, "Click to clear the boxes");
            toolTipHelp.SetToolTip(txtYear, "Enter year in which the book was published");

            // Populate Help Dictionary (all lowercase for consistency)
            helpDictionary.Add("title", "Enter Title of the book");
            helpDictionary.Add("genre", "Enter Genre of a book");
            helpDictionary.Add("author", "Enter Author of a book");
            helpDictionary.Add("phonenumber", "Enter phone number using digits only");
            helpDictionary.Add("status", "Choose available or not available");
            helpDictionary.Add("year", "Enter year in which the book was published");
            helpDictionary.Add("add", "Click to Add a book");
            helpDictionary.Add("update", "Click to Update a book");
            helpDictionary.Add("delete", "Click to Delete a book");
            helpDictionary.Add("searchby", "Choose what to search database by");
            helpDictionary.Add("search", "Enter text to search database");
            helpDictionary.Add("home", "Click to return to Home/Admin Form");
            helpDictionary.Add("grid", "Display all saved book's details");
            helpDictionary.Add("clear", "Click to clear the boxes");
        }

        private void HelpSearch()
        {
            string keyWord = txtHelpSearch.Text.ToLower().Trim();

            if (helpDictionary.ContainsKey(keyWord))
            {
                MessageBox.Show(helpDictionary[keyWord],
                    "Help - " + keyWord,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No help available for this keyword. Try: title, genre, author, phoneNumber, status, year, etc.",
                    "Help",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void txtHelpSearch_TextChanged(object sender, EventArgs e)
        {
            //HelpSearch();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpSearch();
        }
    }
}
