
namespace CMPG223_Project1
{
    partial class frmBook
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnClear1 = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnUpdateBook = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxAuthorId = new System.Windows.Forms.ComboBox();
            this.txtGenre1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblGenre1 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblAuthor1 = new System.Windows.Forms.Label();
            this.txtPhoneNum = new System.Windows.Forms.TextBox();
            this.txtTitle1 = new System.Windows.Forms.TextBox();
            this.txtAuthor11 = new System.Windows.Forms.TextBox();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.cbxStatu1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
            this.txtHelpSearch = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack1
            // 
            this.btnBack1.BackColor = System.Drawing.Color.LightCoral;
            this.btnBack1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack1.Location = new System.Drawing.Point(776, 371);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(75, 22);
            this.btnBack1.TabIndex = 52;
            this.btnBack1.Text = "Home";
            this.btnBack1.UseVisualStyleBackColor = false;
            this.btnBack1.Click += new System.EventHandler(this.btnBack1_Click);
            // 
            // btnClear1
            // 
            this.btnClear1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnClear1.Location = new System.Drawing.Point(102, 312);
            this.btnClear1.Name = "btnClear1";
            this.btnClear1.Size = new System.Drawing.Size(75, 23);
            this.btnClear1.TabIndex = 51;
            this.btnClear1.Text = "Clear";
            this.btnClear1.UseVisualStyleBackColor = false;
            this.btnClear1.Click += new System.EventHandler(this.btnClear1_Click);
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDeleteBook.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteBook.Location = new System.Drawing.Point(187, 273);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(75, 35);
            this.btnDeleteBook.TabIndex = 50;
            this.btnDeleteBook.Text = "Delete Book";
            this.btnDeleteBook.UseVisualStyleBackColor = false;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            // 
            // btnUpdateBook
            // 
            this.btnUpdateBook.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnUpdateBook.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateBook.Location = new System.Drawing.Point(102, 272);
            this.btnUpdateBook.Name = "btnUpdateBook";
            this.btnUpdateBook.Size = new System.Drawing.Size(75, 34);
            this.btnUpdateBook.TabIndex = 49;
            this.btnUpdateBook.Text = "Update Book";
            this.btnUpdateBook.UseVisualStyleBackColor = false;
            this.btnUpdateBook.Click += new System.EventHandler(this.btnUpdateBook_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnAddBook.ForeColor = System.Drawing.Color.Black;
            this.btnAddBook.Location = new System.Drawing.Point(20, 272);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(75, 34);
            this.btnAddBook.TabIndex = 48;
            this.btnAddBook.Text = "Add Book";
            this.btnAddBook.UseVisualStyleBackColor = false;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 25);
            this.label2.TabIndex = 47;
            this.label2.Text = "BOOK FORM";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbxAuthorId);
            this.groupBox2.Controls.Add(this.txtGenre1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTitle1);
            this.groupBox2.Controls.Add(this.btnClear1);
            this.groupBox2.Controls.Add(this.lblYear);
            this.groupBox2.Controls.Add(this.btnDeleteBook);
            this.groupBox2.Controls.Add(this.lblGenre1);
            this.groupBox2.Controls.Add(this.btnUpdateBook);
            this.groupBox2.Controls.Add(this.btnAddBook);
            this.groupBox2.Controls.Add(this.txtYear);
            this.groupBox2.Controls.Add(this.lblAuthor1);
            this.groupBox2.Controls.Add(this.txtPhoneNum);
            this.groupBox2.Controls.Add(this.txtTitle1);
            this.groupBox2.Controls.Add(this.txtAuthor11);
            this.groupBox2.Controls.Add(this.lblStatus1);
            this.groupBox2.Controls.Add(this.cbxStatu1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 348);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Book Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "AuthorId";
            // 
            // cbxAuthorId
            // 
            this.cbxAuthorId.FormattingEnabled = true;
            this.cbxAuthorId.Location = new System.Drawing.Point(143, 32);
            this.cbxAuthorId.Name = "cbxAuthorId";
            this.cbxAuthorId.Size = new System.Drawing.Size(119, 24);
            this.cbxAuthorId.TabIndex = 35;
            // 
            // txtGenre1
            // 
            this.txtGenre1.Location = new System.Drawing.Point(143, 103);
            this.txtGenre1.Name = "txtGenre1";
            this.txtGenre1.Size = new System.Drawing.Size(119, 22);
            this.txtGenre1.TabIndex = 23;
            this.txtGenre1.TextChanged += new System.EventHandler(this.txtGenre1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "Phone Number";
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(17, 67);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(34, 16);
            this.lblTitle1.TabIndex = 17;
            this.lblTitle1.Text = "Title";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(15, 211);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(100, 16);
            this.lblYear.TabIndex = 33;
            this.lblYear.Text = "Published Year";
            // 
            // lblGenre1
            // 
            this.lblGenre1.AutoSize = true;
            this.lblGenre1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenre1.Location = new System.Drawing.Point(17, 103);
            this.lblGenre1.Name = "lblGenre1";
            this.lblGenre1.Size = new System.Drawing.Size(45, 16);
            this.lblGenre1.TabIndex = 18;
            this.lblGenre1.Text = "Genre";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(143, 211);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(119, 22);
            this.txtYear.TabIndex = 32;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            // 
            // lblAuthor1
            // 
            this.lblAuthor1.AutoSize = true;
            this.lblAuthor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor1.Location = new System.Drawing.Point(17, 139);
            this.lblAuthor1.Name = "lblAuthor1";
            this.lblAuthor1.Size = new System.Drawing.Size(46, 16);
            this.lblAuthor1.TabIndex = 19;
            this.lblAuthor1.Text = "Author";
            // 
            // txtPhoneNum
            // 
            this.txtPhoneNum.Location = new System.Drawing.Point(143, 174);
            this.txtPhoneNum.Name = "txtPhoneNum";
            this.txtPhoneNum.Size = new System.Drawing.Size(119, 22);
            this.txtPhoneNum.TabIndex = 31;
            this.txtPhoneNum.TextChanged += new System.EventHandler(this.txtPhoneNum_TextChanged);
            this.txtPhoneNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneNum_KeyPress);
            // 
            // txtTitle1
            // 
            this.txtTitle1.Location = new System.Drawing.Point(143, 67);
            this.txtTitle1.Name = "txtTitle1";
            this.txtTitle1.Size = new System.Drawing.Size(119, 22);
            this.txtTitle1.TabIndex = 22;
            // 
            // txtAuthor11
            // 
            this.txtAuthor11.Location = new System.Drawing.Point(143, 139);
            this.txtAuthor11.Name = "txtAuthor11";
            this.txtAuthor11.Size = new System.Drawing.Size(119, 22);
            this.txtAuthor11.TabIndex = 24;
            this.txtAuthor11.TextChanged += new System.EventHandler(this.txtAuthor11_TextChanged);
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus1.Location = new System.Drawing.Point(18, 243);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(45, 16);
            this.lblStatus1.TabIndex = 26;
            this.lblStatus1.Text = "Status";
            // 
            // cbxStatu1
            // 
            this.cbxStatu1.FormattingEnabled = true;
            this.cbxStatu1.Location = new System.Drawing.Point(141, 243);
            this.cbxStatu1.Name = "cbxStatu1";
            this.cbxStatu1.Size = new System.Drawing.Size(121, 24);
            this.cbxStatu1.TabIndex = 30;
            this.cbxStatu1.SelectedIndexChanged += new System.EventHandler(this.cbxStatu1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "Search Here:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(401, 48);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 22);
            this.txtSearch.TabIndex = 44;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(294, 76);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(557, 287);
            this.dgvBooks.TabIndex = 43;
            this.dgvBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellContentClick);
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Items.AddRange(new object[] {
            "Title",
            "Genre",
            "Author",
            "Phone_No",
            "Published_Year",
            "Status"});
            this.cmbSearchBy.Location = new System.Drawing.Point(401, 21);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(79, 21);
            this.cmbSearchBy.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(309, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 54;
            this.label4.Text = "Search by :";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtHelpSearch
            // 
            this.txtHelpSearch.Location = new System.Drawing.Point(385, 371);
            this.txtHelpSearch.Name = "txtHelpSearch";
            this.txtHelpSearch.Size = new System.Drawing.Size(100, 20);
            this.txtHelpSearch.TabIndex = 57;
            this.txtHelpSearch.TextChanged += new System.EventHandler(this.txtHelpSearch_TextChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(491, 369);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(51, 23);
            this.btnHelp.TabIndex = 56;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Search keyword";
            // 
            // frmBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(923, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtHelpSearch);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBack1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvBooks);
            this.Name = "frmBook";
            this.Text = "frmBook";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Button btnClear1;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnUpdateBook;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtGenre1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblGenre1;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblAuthor1;
        private System.Windows.Forms.TextBox txtPhoneNum;
        private System.Windows.Forms.TextBox txtTitle1;
        private System.Windows.Forms.TextBox txtAuthor11;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.ComboBox cbxStatu1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxAuthorId;
        private System.Windows.Forms.ToolTip toolTipHelp;
        private System.Windows.Forms.TextBox txtHelpSearch;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label6;
    }
}