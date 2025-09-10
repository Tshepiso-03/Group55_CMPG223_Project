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

namespace CMPG223_Project1
{
    public partial class frmLoanDetails : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\GIT\Group55_CMPG223_Project\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        public frmLoanDetails()
        {
            InitializeComponent();
            LoadLoanGrid();
            statusCombobox();
            //LoadStatusCombo();
            LoadLoans();
            LoadBooks();
            LoadMembers();

            dtpLoanDate.ValueChanged += (s, e) =>
            {
                dtpDueDate.MinDate = dtpLoanDate.Value;
            };
        }

        private void LoadLoanGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Loan";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Loan");

                    //Adding the data into the data grid
                    dgvLoan.DataSource = ds;
                    dgvLoan.DataMember = "Loan";

                    //Closing the connection to the database
                    //conn.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateLoanForm()
        {
            if (cbxMembers.SelectedItem == null)
            {
                MessageBox.Show("Please a select Member.");
                return false;
            }
            if (cbxBooks.SelectedItem == null)
            {
                MessageBox.Show("Please a select Book.");
                return false;
            }
            if (dtpLoanDate.Value == null)
            {
                MessageBox.Show("Please a select Loan Date.");
                return false;
            }
            if (dtpDueDate.Value == null)
            {
                MessageBox.Show("Please a select Due Date.");
                return false;
            }
            if (dtpDueDate.Value.Date <= dtpLoanDate.Value.Date)
            {
                MessageBox.Show("Due Date must be after Loan Date");
                return false;
            }
            if (cbxStatus.SelectedItem == null)
            {
                MessageBox.Show("Please a select Status.");
                return false;
            }
            return true;
        }

        
        private void LoadComboboxes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //Members Combo box
                string sqlMember = "SELECT MemberId, Email FROM Members";
                SqlDataAdapter daMember = new SqlDataAdapter(sqlMember, conn);
                DataTable dtMembers = new DataTable();
                daMember.Fill(dtMembers);
                cbxMembers.DataSource = dtMembers;
                cbxMembers.DisplayMember = "Email";
                cbxMembers.ValueMember = "MemberId";

                //Books Combo Box
                string sqlBook = "SELECT BookId, Title FROM Books";
                SqlDataAdapter daBook = new SqlDataAdapter(sqlBook, conn);
                DataTable dtBook = new DataTable();
                daBook.Fill(dtBook);
                cbxBooks.DataSource = dtBook;
                cbxBooks.DisplayMember = "Title";
                cbxBooks.ValueMember = "BookId";

            }
        }
        private void ClearForm()
        {
            cbxBooks.SelectedValue = -1;
            cbxMembers.SelectedValue = -1;
            cbxStatus.SelectedValue = -1;
            //dtpDueDate.Sel
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateLoanForm())
            {
                //return;
                if (cbxMembers.SelectedValue == null || cbxBooks.SelectedValue == null)
                {
                    MessageBox.Show("Please select both a member and a book.");
                    return;
                }

                int memberId = Convert.ToInt32(cbxMembers.SelectedValue);
                int bookId = Convert.ToInt32(cbxBooks.SelectedValue);

                DateTime loanDate = dtpLoanDate.Value;
                DateTime dueDate = dtpDueDate.Value;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Loan (MemberId, BookId, Date_Loaned, Due_Date, Status)
                                   VALUES (@MemberId, @BookId, @Date_Loaned, @Due_Date, @Status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("LoanId",)
                    cmd.Parameters.AddWithValue("@MemberId", cbxMembers.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BookId", cbxBooks.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Date_Loaned", dtpLoanDate.Value);
                    cmd.Parameters.AddWithValue("@Due_Date", dtpDueDate.Value);
                    cmd.Parameters.AddWithValue("@Status", cbxStatus.SelectedItem?.ToString() ?? "Available");

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Loan added successfully!");
                        LoadLoanGrid();
                        LoadLoans();
                        LoadBooks();
                        LoadMembers();
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
            
            if (dgvLoan.CurrentRow == null) return;
            int loanId = Convert.ToInt32(dgvLoan.CurrentRow.Cells["LoanId"].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"
                UPDATE Loan
                SET MemberId = @MemberId,
                    BookId = @BookId,
                    Date_Loaned = @Date_Loaned,
                    Due_Date = @Due_Date,
                    Status = @Status
                WHERE LoanId = @LoanId";

                   // bool status = cbxStatus.SelectedItem != null && cbxStatus.SelectedItem.ToString() == "Available";
                    string status = cbxStatus.SelectedItem?.ToString() ?? "Available";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    cmd.Parameters.AddWithValue("@MemberId", cbxMembers.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BookId", cbxBooks.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Date_Loaned", dtpLoanDate.Value);
                    cmd.Parameters.AddWithValue("@Due_Date", dtpDueDate.Value);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Loan updated successfully.");
                        LoadLoans();
                        LoadBooks();
                        LoadMembers();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Loan.");
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Error updating loan", ex.Message);
            }
            
        }
        private void LoadLoans()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT L.LoanId, 
                                          M.Email AS MemberEmail,
                                          B.Title AS BookTitle, 
                                          L.Date_Loaned, 
                                          L.Due_Date,
                                          L.Status
                                FROM Loan L
                                INNER JOIN Members M ON L.MemberId = M.MemberId
                                INNER JOIN Books B ON L.BookId = B.BookId";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Loan");
                    dgvLoan.DataSource = ds;
                    dgvLoan.DataMember = "Loan";

                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading loans: ", ex.Message);
            }
        }
        private void LoadMembers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT MemberId, Email FROM Members";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxMembers.DataSource = dt;
                    cbxMembers.DisplayMember = "Email";
                    cbxMembers.ValueMember = "MemberId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading members: ", ex.Message);
            }
        }
        private void LoadBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = @"SELECT BookId, Title FROM Books";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxBooks.DataSource = dt;
                    cbxBooks.DisplayMember = "Title";
                    cbxBooks.ValueMember = "BookId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: ", ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoan.CurrentRow == null) return;
            int loadId = Convert.ToInt32(dgvLoan.CurrentRow.Cells["LoanId"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"DELETE FROM Loan WHERE LoanId = @LoanId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@LoanId", loadId);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    MessageBox.Show("Loan deleted successfully!");
                    LoadLoans();
                }
                else
                {
                    MessageBox.Show("Failed to delete Loan");
                }
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void dgvLoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvLoan.Rows[e.RowIndex];

            // Populate text boxes
            //cbxMembers.SelectedValue = row.Cells["MemberId"].Value.ToString();
            //cbxBooks.SelectedValue = row.Cells["BookId"].Value.ToString();
            dtpLoanDate.Value = Convert.ToDateTime(row.Cells["Date_Loaned"].Value.ToString());
            dtpDueDate.Value = Convert.ToDateTime(row.Cells["Due_Date"].Value.ToString());
            cbxStatus.SelectedValue = row.Cells["Status"].Value.ToString();
        }
        private void ValidatesDate()
        {
            DateTime loanDate = dtpLoanDate.Value;
            DateTime dueDate = dtpDueDate.Value;

            if(dueDate < loanDate)
            {
                MessageBox.Show("Due Date cannot be earlier than the Loan Date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDueDate.Value = loanDate.AddDays(30);//Resets DueDate to Loan Date + 30 Days
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ValidatesDate();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ValidatesDate();
        }
        private void statusCombobox()
        {
            cbxStatus.Items.Clear();
            cbxStatus.Items.AddRange(new string[] { "Available", "Not Available" });
        }
        private void LoadStatusCombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT DISTINCT Status FROM Loan";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    //conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cbxStatus.Items.Add(reader["Status"].ToString());
                    }
                    reader.Close();
                }
                if (cbxStatus.Items.Count > 0)
                {
                    cbxStatus.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Loading ComboBox", ex.Message);
            }
        }
        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
