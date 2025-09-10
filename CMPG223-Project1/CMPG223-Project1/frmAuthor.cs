using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CMPG223_Project1
{
    public partial class frmAuthor : Form
    {
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Models\GitProject\CMPG223-Project1\CMPG223-Project1\CMPG223-Project1\AuthorDatabase.mdf;Integrated Security=True";
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\telePROJECTS\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        public frmAuthor()
        {
            InitializeComponent();
            LoadAuthorGrid();
            HelpToolTip();

            cmbSearchBy.SelectedIndex = 0; // Default to FirstName
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateAuthorForm())
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Author (FirstName, LastName, Biography, Nationality) " +
                                   "VALUES (@FirstName, @LastName, @Biography, @Nationality)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Biography", txtBio.Text);
                    cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Author added successfully!");
                        LoadAuthorGrid();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                //LoadMemberGrid();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the First Name of the Author to update.");
                return;
            }

            // Get updated values from form
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string bio = txtBio.Text;
            string nationality = txtNationality.Text;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Update the first member with this FirstName
                string sql = @"
                UPDATE TOP(1) Author
                SET LastName = @LastName,
                    Biography = @Biography,
                    Nationality = @Nationality
                WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Biography", bio);
                cmd.Parameters.AddWithValue("@Nationality", nationality);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Author updated successfully.");
                    LoadAuthorGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No Author found with that First Name.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter a First Name to delete.");
                return;
            }

            string firstNameToDelete = txtFirstName.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Delete the first member matching the first name
                string sql = @"DELETE TOP(1) FROM Author WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstNameToDelete);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Author deleted successfully.");
                    LoadAuthorGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No Author found with that First Name.");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedField = cmbSearchBy.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(selectedField)) return;

                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    string sql = $"SELECT * FROM Author WHERE {selectedField} LIKE @searchText";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + txtSearch.Text + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Author");

                        dgvAuthor.DataSource = ds;
                        dgvAuthor.DataMember = "Author";
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void dgvAuthor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvAuthor.Rows[e.RowIndex];

            // Populate text boxes
            txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtBio.Text = row.Cells["Biography"].Value.ToString();
            txtNationality.Text = row.Cells["Nationality"].Value.ToString();
        }

        private void tntBook_Click(object sender, EventArgs e)
        {
            frmBook bookForm = new frmBook();
            bookForm.Show();
        }
        private void LoadAuthorGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Author";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Author");

                    //Adding the data into the data grid
                    dgvAuthor.DataSource = ds;
                    dgvAuthor.DataMember = "Author";

                    //Closing the connection to the database
                    //conn.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateAuthorForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtBio.Text) ||
                string.IsNullOrWhiteSpace(txtNationality.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }
        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtBio.Clear();
            txtNationality.Clear();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.Show();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]+$"))
            {
                //txtFirstName.Text = txtFirstName.Text.Remove(txtFirstName.Text.Length - 1);
                txtFirstName.SelectionStart = txtFirstName.Text.Length; // keep cursor at end
                errorProvider1.SetError(txtFirstName, "First Name must only contain letters.");
            }
            else
            {
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]+$"))
            {
                //txtLastName.Text = txtLastName.Text.Remove(txtLastName.Text.Length - 1);
                txtLastName.SelectionStart = txtLastName.Text.Length; // keep cursor at end
                errorProvider1.SetError(txtLastName, "Last Name must only contain letters.");
            }
            else
            {
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void txtNationality_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtNationality.Text, @"^[a-zA-Z]+$"))
            {
                //txtNationality.Text = txtNationality.Text.Remove(txtNationality.Text.Length - 2, 2);
                txtNationality.SelectionStart = txtNationality.Text.Length; // keep cursor at end
                errorProvider1.SetError(txtNationality, "Nationality must only contain letters.");               
            }
            else
            {
                errorProvider1.SetError(txtNationality, "");
            }
        }

        private void txtBio_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtBio.Text, @"^[\w\s\p{P}]{2,100}$"))
            {
                errorProvider1.SetError(txtBio, "Invalid biography format!");
            }
            else
            {
                errorProvider1.SetError(txtBio, "");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        Dictionary<string, string> helpDictionary = new Dictionary<string, string>();
        private void HelpToolTip()
        {
            toolTip1 = new System.Windows.Forms.ToolTip
            {
                IsBalloon = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Help"
            };

            // Assign ToolTips
            toolTip1.SetToolTip(txtFirstName, "Enter First Name");
            toolTip1.SetToolTip(txtLastName, "Enter Last Name");
            toolTip1.SetToolTip(txtNationality, "Enter Nationality");
            toolTip1.SetToolTip(txtBio, "Enter biography");
            toolTip1.SetToolTip(btnAdd, "Click to Add a author");
            toolTip1.SetToolTip(btnUpdate, "Click to Update a author");
            toolTip1.SetToolTip(btnDelete, "Click to Delete a author");
            toolTip1.SetToolTip(cmbSearchBy, "Choose what to search database by");
            toolTip1.SetToolTip(txtSearch, "Enter text to search database");
            toolTip1.SetToolTip(btnHome, "Click to return to Home/Admin Form");
            toolTip1.SetToolTip(dgvAuthor, "Display all saved authos's details");
            toolTip1.SetToolTip(tntBook, "Click to navigate to book form");

            // Populate Help Dictionary (all lowercase for consistency)
            helpDictionary.Add("firstname", "Enter First Name");
            helpDictionary.Add("lastname", "Enter Last Name");
            helpDictionary.Add("nationality", "Enter email (example: user@mail.com)");
            helpDictionary.Add("biography", "Enter phone number using digits only");
            //helpDictionary.Add("loanbook", "Choose whether to loan a book or not");
            //helpDictionary.Add("rsvp", "Choose Yes or No for RSVP");
            helpDictionary.Add("add", "Click to Add a author");
            helpDictionary.Add("update", "Click to Update a author");
            helpDictionary.Add("delete", "Click to Delete a author");
            helpDictionary.Add("searchby", "Choose what to search database by");
            helpDictionary.Add("search", "Enter text to search database");
            helpDictionary.Add("home", "Click to return to Home/Admin Form");
            helpDictionary.Add("grid", "Display all saved author's details");
            helpDictionary.Add("book", "Click to navigate to book form");
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
                MessageBox.Show("No help available for this keyword. Try: firstname, lastname, nationality, biography, home, etc.",
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtFirstName.Clear();
            txtBio.Clear();
            txtNationality.Clear();
            txtLastName.Clear();
        }
    }
}
