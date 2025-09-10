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
    public partial class frmEvents : Form
    {
        //string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Models\GitProject\CMPG223-Project1\CMPG223-Project1\CMPG223-Project1\EventDatabase.mdf;Integrated Security=True";
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\GIT\Group55_CMPG223_Project\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        SqlConnection conn;
        public frmEvents()
        {
            InitializeComponent();
            LoadReaderCombobox();
            JoinEvents();
            LoadReader();
            //LoadEvents();
            LoadEventsGrid();
            HelpToolTip();
            LoadBooks();
            LoadBookComboboxes();
        }

        private void LoadBookComboboxes()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
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

        private void LoadBooks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
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
        private void LoadEventsGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();

                    string sql = @"SELECT * FROM Events";
                    SqlCommand command = new SqlCommand(sql, conn);

                    //Filling the dataset
                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Events");

                    //Adding the data into the data grid
                    dgvEvents.DataSource = ds;
                    dgvEvents.DataMember = "Events";

                    //Closing the connection to the database
                    //conn.Close();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error loading datatable!!",ex.Message);
            }
        }
        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEvents.Rows[e.RowIndex].Cells["Date"].Value != null)
            {
                DataGridViewRow row = dgvEvents.Rows[e.RowIndex];
                dtpDate.Value = Convert.ToDateTime(row.Cells["Date"].Value);
                //txtBook.Text = row.Cells["Book"].Value.ToString();
                txtAttendees.Text = row.Cells["Attendees"].Value.ToString();
            }

        }
        private bool ValidateEventForm()
        {
            if (cbxBooks.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtAttendees.Text) ||
                cbxReader.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            return true;
        }
        private void LoadReaderCombobox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sqlReader = "SELECT ReaderId, Email FROM Readers";
                    SqlDataAdapter daReader = new SqlDataAdapter(sqlReader, conn);

                    DataSet dsReader = new DataSet();
                    daReader.Fill(dsReader, "Readers");

                    cbxReader.DataSource = dsReader.Tables["Readers"];
                    cbxReader.DisplayMember = "Email";
                    cbxReader.ValueMember = "ReaderId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading combo box: ", ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            try
            {
                if (ValidateEventForm())
                {
                    //return;
                    if (cbxReader.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a reader.");
                        return;
                    }

                    int readerId = Convert.ToInt32(cbxReader.SelectedValue);
                    int bookId = Convert.ToInt32(cbxBooks.SelectedValue);

                    DateTime date = dtpDate.Value;

                    string query = @"INSERT INTO Events (ReaderId, Date, BookId, Attendees) VALUES (@ReaderId, @Date, @BookId, @Attendees)";
                    //string query = @"INSERT INTO Events (ReaderId, BookId, Date_Loaned, Due_Date, Status)
                                  // VALUES (@MemberId, @BookId, @Date_Loaned, @Due_Date, @Status)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("LoanId",)
                    cmd.Parameters.AddWithValue("@ReaderId", readerId);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@Attendees", int.Parse(txtAttendees.Text));

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event inserted successfully!");
                    //LoadEvents();
                    JoinEvents();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed: " + ex.Message);
                conn.Close();
            }
            
             
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                string query = @"
                UPDATE Events
                SET ReaderId = @ReaderId,
                    Date = @Date,
                    BookId = @BookId,
                    Attendees=@Attendees
                WHERE EventsId = @EventsId";
                //string query = "UPDATE Events SET ReaderId=@ReaderId, Date=@Date, Attendees=@Attendees WHERE Book=@Book";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReaderId", cbxReader.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Date", dtpDate.Value);
                cmd.Parameters.AddWithValue("@BookId", cbxBooks.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Attendees", int.Parse(txtAttendees.Text));

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Event updated successfully!");
                JoinEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message);
                conn.Close();
            }
        }
       
        private void JoinEvents()
        {
            string query = @"SELECT 
                            E.EventsId,
                            R.Email,
                            E.Date,
                            B.BookId,
                            E.Attendees
                        FROM Events E
                        INNER JOIN Readers R ON E.ReaderId = R.ReaderId
                        INNER JOIN Books B ON E.BookId = B.BookId";
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();

                    da.Fill(ds, "Events");
                    dgvEvents.DataSource = ds;
                    dgvEvents.DataMember = "Events";
                }
                catch(SqlException ex)
                {
                    MessageBox.Show("Error joining tables: ", ex.Message);
                }
            }
        }
        private void LoadReader()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"SELECT ReaderId, Email FROM Readers";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbxReader.DataSource = dt;
                    cbxReader.DisplayMember = "Email";
                    cbxReader.ValueMember = "ReaderId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading readers: ", ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);
            try
            {
                int eventsId = Convert.ToInt32(dgvEvents.CurrentRow.Cells["LoanId"].Value);
                conn.Open();
                string query = "DELETE FROM Events WHERE EventsId=@EventsId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EventsId", eventsId);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Event deleted successfully!");
                JoinEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
                conn.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 adminForm = new Form1();
            adminForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtAttendees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // ignore the key
                                  // MessageBox.Show("Phone number must only contain digits.");
                errorProvider1.SetError(txtAttendees, "Attendees must only contain digits.");
            }
        }

        private void txtBook_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAttendees_TextChanged(object sender, EventArgs e)
        {

        }
        Dictionary<string, string> helpDictionary = new Dictionary<string, string>();

        private void txtHelpSearch_TextChanged(object sender, EventArgs e)
        {
            HelpSearch();
        }
        private void HelpToolTip()
        {
           toolTip1 = new System.Windows.Forms.ToolTip
           {
               IsBalloon = true,
               ToolTipIcon = ToolTipIcon.Info,
               ToolTipTitle = "Help"
           };

           // Assign ToolTips
           toolTip1.SetToolTip(dtpDate, "Choose date");
           toolTip1.SetToolTip(cbxBooks, "Choose Book Name");
           toolTip1.SetToolTip(txtAttendees, "Enter number of Attendees");
           toolTip1.SetToolTip(cbxReader, "Choose reader by email");
           //toolTip1.SetToolTip(cbxAvilable, "Book availability");
           //toolTip1.SetToolTip(cbxRSVP, "Choose Yes or No for RSVP");
           toolTip1.SetToolTip(btnAdd, "Click to Add a reader");
           toolTip1.SetToolTip(btnUpdate, "Click to Update a reader");
           toolTip1.SetToolTip(btnDelete, "Click to Delete a reader");
           //toolTip1.SetToolTip(cmbSearchBy, "Choose what to search database by");
           //toolTip1.SetToolTip(txtSearch, "Enter text to search database");
           toolTip1.SetToolTip(btnBack, "Click to return to Home/Admin Form");
           toolTip1.SetToolTip(dgvEvents, "Display all saved event's details");
           //toolTip1.SetToolTip(btnEvents, "Click to navigate to events form");

           // Populate Help Dictionary (all lowercase for consistency)
           helpDictionary.Add("date", "Choose date");
           helpDictionary.Add("book", "Choose book Name");
           helpDictionary.Add("attendees", "Enter number of Attendees");
           helpDictionary.Add("reader", "Choose reader by email");
           //helpDictionary.Add("available", "Book availability");
           //helpDictionary.Add("rsvp", "Choose Yes or No for RSVP");
           helpDictionary.Add("add", "Click to Add a member");
           helpDictionary.Add("update", "Click to Update a member");
           helpDictionary.Add("delete", "Click to Delete a member");
           //helpDictionary.Add("searchby", "Choose what to search database by");
           //helpDictionary.Add("search", "Enter text to search database");
           helpDictionary.Add("home", "Click to return to Home/Admin Form");
           helpDictionary.Add("grid", "Display all saved events's details");
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
               MessageBox.Show("No help available for this keyword. Try: date, book, attendees, reader, home, etc.",
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
            txtAttendees.Clear();
            //txtBook.Clear();
            cbxReader.SelectedValue = -1;
        }
    }
}
