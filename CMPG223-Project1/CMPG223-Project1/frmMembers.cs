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
    public partial class frmMembers : Form
    {

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\telePROJECTS\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";

        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataSet st;
        //private readonly string connectionString = "Data Source=.;Initial Catalog=LibraryDB;Integrated Security=True";
        public frmMembers()
        {
            InitializeComponent();
            LoadMemberGrid();
            LoadComboBoxes();
            HelpToolTip();
            ClearForm();

            //cmbSearchBy.Items.AddRange(new string[] { "FirstName", "LastName", "PhoneNo", "Email" });
            cmbSearchBy.SelectedIndex = 0; // Default to FirstName
            //btnExit.Visible = false;
        }

        private bool ValidateMemberForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNo.Text) ||
                cbxLoaBook.SelectedItem == null ||
                cbxRSVP.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }
        private void LoadMemberGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Members";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Members");

                    //Adding the data into the data grid
                    dgvMembers.DataSource = ds;
                    dgvMembers.DataMember = "Members";

                    //Closing the connection to the database
                    //conn.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            
            if (ValidateMemberForm())
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    bool loanBookValue = cbxLoaBook.SelectedItem != null && cbxLoaBook.SelectedItem.ToString() == "Yes";
                    bool rsvpValue = cbxRSVP.SelectedItem != null && cbxRSVP.SelectedItem.ToString() == "Yes";

                    string query = "INSERT INTO Members (FirstName, LastName, Email, Phone_No, Loan_Book, RSVP) " +
                                   "VALUES (@FirstName, @LastName, @Email, @Phone_No, @Loan_Book, @RSVP)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone_No", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Loan_Book", loanBookValue);
                    cmd.Parameters.AddWithValue("@RSVP", rsvpValue);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Member added successfully!");
                        LoadMemberGrid();
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
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhoneNo.Clear();
            cbxLoaBook.SelectedIndex = -1;
            cbxRSVP.SelectedIndex = -1;
        }
        private void LoadComboBoxes()
        {
            cbxLoaBook.Items.Clear();
            cbxLoaBook.Items.AddRange(new string[] { "Yes", "No" });

            cbxRSVP.Items.Clear();
            cbxRSVP.Items.AddRange(new string[] { "Yes", "No" });
        }
          
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the First Name of the member to update.");
                return;
            }

            // Get updated values from form
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phoneNo = txtPhoneNo.Text;

            // Convert ComboBox selections to boolean
            bool loanBookValue = cbxLoaBook.SelectedItem != null && cbxLoaBook.SelectedItem.ToString() == "Yes";
            bool rsvpValue = cbxRSVP.SelectedItem != null && cbxRSVP.SelectedItem.ToString() == "Yes";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Update the first member with this FirstName
                string sql = @"
                UPDATE TOP(1) Members
                SET LastName = @LastName,
                    Email = @Email,
                    Phone_No = @Phone_No,
                    Loan_Book = @Loan_Book,
                    RSVP = @RSVP
                WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone_No", phoneNo);
                cmd.Parameters.AddWithValue("@Loan_Book", loanBookValue);
                cmd.Parameters.AddWithValue("@RSVP", rsvpValue);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Member updated successfully.");
                    LoadMemberGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No member found with that First Name.");
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
                string sql = @"DELETE TOP(1) FROM Members WHERE FirstName = @FirstName";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstNameToDelete);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Member deleted successfully.");
                    LoadMemberGrid(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("No member found with that First Name.");
                }
            }
        }

        private void dgvMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvMembers.Rows[e.RowIndex];

            // Populate text boxes
            txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
            txtLastName.Text = row.Cells["LastName"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtPhoneNo.Text = row.Cells["Phone_No"].Value.ToString();

            // Populate ComboBoxes (Yes/No)
            cbxLoaBook.Text = Convert.ToBoolean(row.Cells["Loan_Book"].Value) ? "Yes" : "No";
            cbxRSVP.Text = Convert.ToBoolean(row.Cells["RSVP"].Value) ? "Yes" : "No";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedField = cmbSearchBy.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(selectedField)) return;

                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    string sql = $"SELECT * FROM Members WHERE {selectedField} LIKE @searchText";

                    using (SqlCommand command = new SqlCommand(sql, cnn))
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + txtSearch.Text + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Members");

                        dgvMembers.DataSource = ds;
                        dgvMembers.DataMember = "Members";
                    }
                }
            }
            catch (SqlException error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            frmLoanDetails loanForm = new frmLoanDetails();
            loanForm.Show();
        }

        private void cbxLoaBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check LoanBook selection
            if (cbxLoaBook.SelectedItem == null)
            {
                MessageBox.Show("Please select Yes or No for Loan Book.");
                return; // stop further execution
            }

            // Safe conversion to string and boolean
            string loanBookStatus = cbxLoaBook.SelectedItem?.ToString();
            bool loanBookValue = loanBookStatus == "Yes";

            // Message and optionally enable/disable related controls (not the ComboBox itself)
            if (loanBookValue)
            {
                MessageBox.Show("You have chosen to loan a book.");
                // Example: enable a dependent ComboBox with available books
                // cbxAvailableBooks.Enabled = true;
            }
            else
            {
                MessageBox.Show("You have chosen not to loan a book.");
                // Example: disable dependent ComboBox
                // cbxAvailableBooks.Enabled = false;
            }

            // Check RSVP selection
            if (cbxRSVP.SelectedItem == null)
            {
                MessageBox.Show("Please select Yes or No for RSVP.");
                return;
            }

            // Convert RSVP ComboBox to boolean safely
            bool rsvpValue = cbxRSVP.SelectedItem?.ToString() == "Yes";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMembers_Load(object sender, EventArgs e)
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
                //txtLastName.Text = txtLastName.Text.Remove(txtLastName.Text.Length - 1);
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
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtEmail, "Invalid email format (example: member@mail.com).");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

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

        private void cbxRSVP_SelectedIndexChanged(object sender, EventArgs e)
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
            toolTip1.SetToolTip(cbxLoaBook, "Choose whether to loan a book or not");
            toolTip1.SetToolTip(cbxRSVP, "Choose Yes or No for RSVP");
            toolTip1.SetToolTip(btnAddMember, "Click to Add a member");
            toolTip1.SetToolTip(btnUpdate, "Click to Update a member");
            toolTip1.SetToolTip(btnDelete, "Click to Delete a member");
            toolTip1.SetToolTip(cmbSearchBy, "Choose what to search database by");
            toolTip1.SetToolTip(txtSearch, "Enter text to search database");
            toolTip1.SetToolTip(btnBack, "Click to return to Home/Admin Form");
            toolTip1.SetToolTip(dgvMembers, "Display all saved member's details");
            toolTip1.SetToolTip(btnLoan, "Click to navigate to loan form");

            // Populate Help Dictionary (all lowercase for consistency)
            helpDictionary.Add("firstname", "Enter First Name");
            helpDictionary.Add("lastname", "Enter Last Name");
            helpDictionary.Add("email", "Enter email (example: user@mail.com)");
            helpDictionary.Add("phonenumber", "Enter phone number using digits only");
            helpDictionary.Add("loanbook", "Choose whether to loan a book or not");
            helpDictionary.Add("rsvp", "Choose Yes or No for RSVP");
            helpDictionary.Add("addmember", "Click to Add a member");
            helpDictionary.Add("update", "Click to Update a member");
            helpDictionary.Add("delete", "Click to Delete a member");
            helpDictionary.Add("searchby", "Choose what to search database by");
            helpDictionary.Add("search", "Enter text to search database");
            helpDictionary.Add("home", "Click to return to Home/Admin Form");
            helpDictionary.Add("grid", "Display all saved member's details");
            helpDictionary.Add("loan", "Click to navigate to loan form");
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
                MessageBox.Show("No help available for this keyword. Try: firstname, lastname, email, phoneNumber, loanBook, rsvp, etc.",
                    "Help",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
          HelpSearch();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtEmail.Clear();
            txtLastName.Clear();
            txtPhoneNo.Clear();
            txtSearch.Clear();
            cbxLoaBook.SelectedValue = -1;
            cbxRSVP.SelectedValue = -1;
        }
    }
}
