using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CMPG223_Project1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=\\143.160.81.140\CTX_Redirected_Data$\43252532\Documents\telePROJECTS\CMPG223-Project1\CMPG223-Project1\LibraryDatabase.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
           
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            frmMembers memberForm = new frmMembers();
            memberForm.Show();
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            frmAuthor authorForm = new frmAuthor();
            authorForm.Show();
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            frmLoanDetails loanForm = new frmLoanDetails();
            loanForm.Show();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            frmBook booksForm = new frmBook();
            booksForm.Show();
        }

        private void btnReader_Click(object sender, EventArgs e)
        {
            frmReader readerForm = new frmReader();
            readerForm.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMembers memberForm = new frmMembers();
            memberForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuthor authorForm = new frmAuthor();
            authorForm.Show();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBook booksForm = new frmBook();
            booksForm.Show();
        }

        private void loanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoanDetails loanForm = new frmLoanDetails();
            loanForm.Show();
        }

        private void readerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReader readerForm = new frmReader();
            readerForm.Show();
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEvents eventsForm = new frmEvents();
            eventsForm.Show();
        }
        private void FormatDataGridView()
        {
            dgvReports.Columns[0].HeaderText = "Book ID";
            dgvReports.Columns[1].HeaderText = "Book Title";
            dgvReports.Columns[2].HeaderText = "Event Count";
            dgvReports.Columns[3].HeaderText = "Total Attendees";

            // Set column width
            dgvReports.Columns[0].Width = 80;
            dgvReports.Columns[1].Width = 200;
            dgvReports.Columns[2].Width = 100;
            dgvReports.Columns[3].Width = 120;

            // Set text alignment
            dgvReports.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReports.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvReports.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReports.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Get the selected start and end dates from DateTimePickers
            DateTime startDate = dtpStart.Value;
            DateTime endDate = dtpEnd.Value;

            // Check if the end date is later than the start date
            if (startDate > endDate)
            {
                MessageBox.Show("End Date cannot be earlier than Start Date.");
                return;
            }

            // Updated SQL query to fetch the top 5 events with associated books
            string query = @"
                SELECT TOP 5 
                    b.BookId,
                    b.Title,
                    COUNT(e.EventsId) AS EventCount,
                    SUM(e.Attendees) AS TotalAttendees
                FROM Events e
                INNER JOIN Books b ON e.BookId = b.BookId   -- Join Events with Books using BookId
                WHERE e.Date BETWEEN @StartDate AND @EndDate
                GROUP BY b.BookId, b.Title
                ORDER BY TotalAttendees DESC";

            try
            {
                // Create a connection to the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create a command object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    // Create a DataAdapter to fill the DataSet
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    // Fill the DataSet with the query result
                    da.Fill(ds, "Top5Events");

                    // Bind the result to the DataGridView
                    dgvReports.DataSource = ds;
                    dgvReports.DataMember = "Top5Events";

                    // Format DataGridView
                    FormatDataGridView();

                    // Optional: If you need to manually process the rows, loop through them
                    if (ds.Tables["Top5Events"].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables["Top5Events"].Rows)
                        {
                            string bookTitle = row["Title"].ToString();
                            int eventCount = Convert.ToInt32(row["EventCount"]);
                            int totalAttendees = Convert.ToInt32(row["TotalAttendees"]);

                            // Add any additional processing here (e.g., displaying in another control)
                            Console.WriteLine($"Book: {bookTitle}, Events: {eventCount}, Total Attendees: {totalAttendees}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No results found.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message in case of a failure
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }
    }
}
