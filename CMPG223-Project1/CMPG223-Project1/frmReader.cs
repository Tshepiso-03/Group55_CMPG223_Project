using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CMPG223_Project1
{
    public partial class frmReader : Form
    {
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Models\GitProject\CMPG223-Project1\CMPG223-Project1\CMPG223-Project1\BookDatabase.mdf;Integrated Security=True";
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\telePROJECTS\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        public frmReader()
        {
            InitializeComponent();
            LoadComboBox();
            LoadReaderGrid();
            HelpToolTip();

            cmbSearchBy.SelectedIndex = 0; // Default to FirstName
        }
        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhoneNo.Clear();
            cbxAvilable.SelectedIndex = -1;
        }
        private void LoadComboBox()
        {
            cbxAvilable.Items.Clear();
            cbxAvilable.Items.AddRange(new string[] { "Available", "Not Available" });
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateReaderForm())
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    bool bookAvilable = cbxAvilable.SelectedItem != null && cbxAvilable.SelectedItem.ToString() == "Yes";


                    string query = "INSERT INTO Readers (FirstName, LastName, Phone_No, Email, Available) " +
                                   "VALUES (@FirstName, @LastName, @Phone_No, @Email, @Available)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Phone_No", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Available", bookAvilable);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Reader added successfully!");
                        LoadReaderGrid();
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
        private bool ValidateReaderForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                cbxAvilable.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }
        private void LoadReaderGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Readers";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Readers");

                    //Adding the data into the data grid
                    dgvReader.DataSource = ds;
                    dgvReader.DataMember = "Readers";

                    //Closing the connection to the database
                    //conn.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the First Name of the Reader to update.");
                return;
            }

            // Get updated values from form
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phoneNo = txtPhoneNo.Text;
            string email = txtEmail.Text;
            // Convert ComboBox selections to boolean
            bool available = cbxAvilable.SelectedItem != null && cbxAvilable.SelectedItem.ToString() == "Available";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Update the first reader with this FirstName
                string sql = @"
                UPDATE TOP(1) Readers
                SET LastName = @LastName,
                    Phone_No = @Phone_No,
                    Email = @Email,
                    Available = @Available
                WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Phone_No", phoneNo);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Available", available);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Reader updated successfully.");
                    LoadReaderGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No reader found with that First Name.");
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
                string sql = @"DELETE TOP(1) FROM Readers WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstNameToDelete);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Reader deleted successfully.");
                    LoadReaderGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No reader found with that First Name.");
                }
            }
        }

        private void dgvReader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvReader.Rows[e.RowIndex];

            // Populate text boxes
            txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtPhoneNo.Text = row.Cells["Phone_No"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            // Populate ComboBoxes (Yes/No)
            cbxAvilable.Text = Convert.ToBoolean(row.Cells["Available"].Value) ? "Available" : "Not Available";
        }

        private void cbxAvilable_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check LoanBook selection
            if (cbxAvilable.SelectedItem == null)
            {
                MessageBox.Show("Please select Available or Not Available.");
                return; // stop further execution
            }

            // Safe conversion to string and boolean
            string availBookStatus = cbxAvilable.SelectedItem?.ToString();
            bool availBookValue = availBookStatus == "Available";

            // Message and optionally enable/disable related controls (not the ComboBox itself)
            if (availBookValue)
            {
                MessageBox.Show("Reader is available.");
            }
            else
            {
                MessageBox.Show("Reader is not avialable");
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
                    string sql = $"SELECT * FROM Readers WHERE {selectedField} LIKE @searchText";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + txtSearch.Text + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Readers");

                        dgvReader.DataSource = ds;
                        dgvReader.DataMember = "Readers";
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            frmEvents eventsForm = new frmEvents();
            eventsForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.Show();
        }

        private void frmReader_Load(object sender, EventArgs e)
        {
            
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
               // txtLastName.Text = txtLastName.Text.Remove(txtLastName.Text.Length - 1);
                txtLastName.SelectionStart = txtLastName.Text.Length;
                errorProvider1.SetError(txtLastName, "Last Name must only contain letters.");
            }
            else
            {
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text,@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtEmail, "Invalid email format (example: member@mail.com).");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // ignore the key
                                  // MessageBox.Show("Phone number must only contain digits.");
                errorProvider1.SetError(txtPhoneNo, "Phone number must only contain digits.");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        Dictionary<string, string> helpDictionary = new Dictionary<string, string>();
        private void HelpToolTip()
        {
            toolTip1 = new ToolTip
            {
                IsBalloon = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Help"
            };

            // Assign ToolTips
            toolTip1.SetToolTip(txtFirstName, "Enter First Name");
            toolTip1.SetToolTip(txtLastName, "Enter Last Name");
            toolTip1.SetToolTip(txtEmail, "Enter email (example: user@mail.com)");
            toolTip1.SetToolTip(txtPhoneNo, "Enter phone number using digits only");
            toolTip1.SetToolTip(cbxAvilable, "Book availability");
            //toolTip1.SetToolTip(cbxRSVP, "Choose Yes or No for RSVP");
            toolTip1.SetToolTip(btnAdd, "Click to Add a reader");
            toolTip1.SetToolTip(btnUpdate, "Click to Update a reader");
            toolTip1.SetToolTip(btnDelete, "Click to Delete a reader");
            toolTip1.SetToolTip(cmbSearchBy, "Choose what to search database by");
            toolTip1.SetToolTip(txtSearch, "Enter text to search database");
            toolTip1.SetToolTip(btnBack, "Click to return to Home/Admin Form");
            toolTip1.SetToolTip(dgvReader, "Display all saved reader's details");
            toolTip1.SetToolTip(btnEvents, "Click to navigate to events form");

            // Populate Help Dictionary (all lowercase for consistency)
            helpDictionary.Add("firstname", "Enter First Name");
            helpDictionary.Add("lastname", "Enter Last Name");
            helpDictionary.Add("email", "Enter email (example: user@mail.com)");
            helpDictionary.Add("phonenumber", "Enter phone number using digits only");
            helpDictionary.Add("available", "Book availability");
            //helpDictionary.Add("rsvp", "Choose Yes or No for RSVP");
            helpDictionary.Add("add", "Click to Add a member");
            helpDictionary.Add("update", "Click to Update a member");
            helpDictionary.Add("delete", "Click to Delete a member");
            helpDictionary.Add("searchby", "Choose what to search database by");
            helpDictionary.Add("search", "Enter text to search database");
            helpDictionary.Add("home", "Click to return to Home/Admin Form");
            helpDictionary.Add("grid", "Display all saved reader's details");
            helpDictionary.Add("events", "Click to navigate to loan form");
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
                MessageBox.Show("No help available for this keyword. Try: firstname, lastname, email, phoneNumber, available, etc.",
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
            txtEmail.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            cbxAvilable.SelectedValue = -1;
            txtPhoneNo.Clear();
            txtSearch.Clear();
        }
    }
}
