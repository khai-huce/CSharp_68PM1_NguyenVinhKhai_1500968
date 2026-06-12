namespace QUANLYSV
{
    partial class QLSinhVien
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvStdView = new System.Windows.Forms.DataGridView();
            this.StdID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFrist = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InputStudentForm = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnDeleteStd = new System.Windows.Forms.Button();
            this.btnEditStd = new System.Windows.Forms.Button();
            this.btnAddStd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStdDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.txtStdId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStdView)).BeginInit();
            this.panel1.SuspendLayout();
            this.InputStudentForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStdView
            // 
            this.dgvStdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStdView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StdID,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvStdView.Location = new System.Drawing.Point(19, 80);
            this.dgvStdView.Name = "dgvStdView";
            this.dgvStdView.RowHeadersWidth = 51;
            this.dgvStdView.RowTemplate.Height = 24;
            this.dgvStdView.Size = new System.Drawing.Size(634, 355);
            this.dgvStdView.TabIndex = 7;
            this.dgvStdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStdView_CellClick);
            // 
            // StdID
            // 
            this.StdID.HeaderText = "Mã sinh viên";
            this.StdID.MinimumWidth = 6;
            this.StdID.Name = "StdID";
            this.StdID.ReadOnly = true;
            this.StdID.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Họ và tên";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Giới tính";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Ngày sinh";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Lớp";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 125;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvStdView);
            this.panel1.Controls.Add(this.btnFrist);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPre);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.txtFind);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(367, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 542);
            this.panel1.TabIndex = 6;
            // 
            // btnFrist
            // 
            this.btnFrist.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFrist.Location = new System.Drawing.Point(160, 461);
            this.btnFrist.Name = "btnFrist";
            this.btnFrist.Size = new System.Drawing.Size(37, 32);
            this.btnFrist.TabIndex = 5;
            this.btnFrist.Text = "<<";
            this.btnFrist.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(504, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(449, 461);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 32);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPre
            // 
            this.btnPre.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPre.Location = new System.Drawing.Point(220, 461);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(37, 32);
            this.btnPre.TabIndex = 5;
            this.btnPre.Text = "<";
            this.btnPre.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Trang 1/1 | 0 bản ghi";
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnFind.Location = new System.Drawing.Point(372, 28);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(103, 46);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = false;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(19, 40);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(324, 22);
            this.txtFind.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Tìm kiếm (mssv/Tên/Lớp)";
            // 
            // InputStudentForm
            // 
            this.InputStudentForm.AccessibleDescription = "";
            this.InputStudentForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InputStudentForm.Controls.Add(this.btnReload);
            this.InputStudentForm.Controls.Add(this.btnDeleteStd);
            this.InputStudentForm.Controls.Add(this.btnEditStd);
            this.InputStudentForm.Controls.Add(this.btnAddStd);
            this.InputStudentForm.Controls.Add(this.groupBox1);
            this.InputStudentForm.Cursor = System.Windows.Forms.Cursors.Default;
            this.InputStudentForm.Location = new System.Drawing.Point(3, 3);
            this.InputStudentForm.Name = "InputStudentForm";
            this.InputStudentForm.Size = new System.Drawing.Size(358, 542);
            this.InputStudentForm.TabIndex = 5;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.BackColor = System.Drawing.Color.Gray;
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReload.Location = new System.Drawing.Point(179, 454);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(170, 47);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "Làm mới";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnDeleteStd
            // 
            this.btnDeleteStd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteStd.BackColor = System.Drawing.Color.Tomato;
            this.btnDeleteStd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteStd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeleteStd.Location = new System.Drawing.Point(6, 454);
            this.btnDeleteStd.Name = "btnDeleteStd";
            this.btnDeleteStd.Size = new System.Drawing.Size(170, 47);
            this.btnDeleteStd.TabIndex = 5;
            this.btnDeleteStd.Text = "Xóa";
            this.btnDeleteStd.UseVisualStyleBackColor = false;
            // 
            // btnEditStd
            // 
            this.btnEditStd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditStd.BackColor = System.Drawing.Color.Green;
            this.btnEditStd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditStd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEditStd.Location = new System.Drawing.Point(179, 401);
            this.btnEditStd.Name = "btnEditStd";
            this.btnEditStd.Size = new System.Drawing.Size(170, 47);
            this.btnEditStd.TabIndex = 4;
            this.btnEditStd.Text = "Sửa";
            this.btnEditStd.UseVisualStyleBackColor = false;
            this.btnEditStd.Click += new System.EventHandler(this.btnEditStd_Click);
            // 
            // btnAddStd
            // 
            this.btnAddStd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddStd.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAddStd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddStd.Location = new System.Drawing.Point(6, 401);
            this.btnAddStd.Name = "btnAddStd";
            this.btnAddStd.Size = new System.Drawing.Size(170, 47);
            this.btnAddStd.TabIndex = 3;
            this.btnAddStd.Text = "Thêm";
            this.btnAddStd.UseVisualStyleBackColor = false;
            this.btnAddStd.Click += new System.EventHandler(this.btnAddStd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStdDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbClass);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbGender);
            this.groupBox1.Controls.Add(this.txtStdId);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 383);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin sinh viên";
            // 
            // txtStdDate
            // 
            this.txtStdDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtStdDate.Location = new System.Drawing.Point(9, 171);
            this.txtStdDate.Name = "txtStdDate";
            this.txtStdDate.Size = new System.Drawing.Size(331, 22);
            this.txtStdDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày sinh";
            // 
            // cbClass
            // 
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(8, 299);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(332, 24);
            this.cbClass.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sinh viên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Lớp";
            // 
            // cbGender
            // 
            this.cbGender.DisplayMember = "NAm";
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cbGender.Location = new System.Drawing.Point(8, 234);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(332, 24);
            this.cbGender.TabIndex = 6;
            this.cbGender.Tag = "";
            // 
            // txtStdId
            // 
            this.txtStdId.Location = new System.Drawing.Point(9, 53);
            this.txtStdId.Name = "txtStdId";
            this.txtStdId.Size = new System.Drawing.Size(332, 22);
            this.txtStdId.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 110);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(332, 22);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Giới tính\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Họ và tên";
            // 
            // QLSinhVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.InputStudentForm);
            this.Name = "QLSinhVien";
            this.Size = new System.Drawing.Size(1067, 546);
            this.Load += new System.EventHandler(this.QLSinhVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStdView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.InputStudentForm.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStdView;
        private System.Windows.Forms.DataGridViewTextBoxColumn StdID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFrist;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel InputStudentForm;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnDeleteStd;
        private System.Windows.Forms.Button btnEditStd;
        private System.Windows.Forms.Button btnAddStd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker txtStdDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbGender;
        private System.Windows.Forms.TextBox txtStdId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
