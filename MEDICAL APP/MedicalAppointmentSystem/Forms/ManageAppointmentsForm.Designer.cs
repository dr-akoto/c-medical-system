namespace MedicalAppointmentSystem.Forms
{
    partial class ManageAppointmentsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlUpdateAppointment = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.pnlUpdateAppointment.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage Appointments";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(17, 104);
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.RowTemplate.Height = 24;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(893, 297);
            this.dgvAppointments.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search Appointments:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(165, 62);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(322, 24);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(17, 416);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(151, 35);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit Appointment";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(183, 416);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(170, 35);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete Appointment";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(801, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 35);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlUpdateAppointment
            // 
            this.pnlUpdateAppointment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUpdateAppointment.Controls.Add(this.btnCancel);
            this.pnlUpdateAppointment.Controls.Add(this.btnUpdate);
            this.pnlUpdateAppointment.Controls.Add(this.txtNotes);
            this.pnlUpdateAppointment.Controls.Add(this.label6);
            this.pnlUpdateAppointment.Controls.Add(this.dtpAppointmentDate);
            this.pnlUpdateAppointment.Controls.Add(this.label7);
            this.pnlUpdateAppointment.Controls.Add(this.cmbPatient);
            this.pnlUpdateAppointment.Controls.Add(this.label8);
            this.pnlUpdateAppointment.Controls.Add(this.cmbDoctor);
            this.pnlUpdateAppointment.Controls.Add(this.label9);
            this.pnlUpdateAppointment.Controls.Add(this.label10);
            this.pnlUpdateAppointment.Location = new System.Drawing.Point(17, 466);
            this.pnlUpdateAppointment.Name = "pnlUpdateAppointment";
            this.pnlUpdateAppointment.Size = new System.Drawing.Size(893, 336);
            this.pnlUpdateAppointment.TabIndex = 9;
            this.pnlUpdateAppointment.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(379, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 39);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(190, 276);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(165, 39);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "Update Appointment";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(190, 185);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(301, 72);
            this.txtNotes.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 18);
            this.label6.TabIndex = 18;
            this.label6.Text = "Notes:";
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpAppointmentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppointmentDate.Location = new System.Drawing.Point(190, 144);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(301, 24);
            this.dtpAppointmentDate.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Appointment Date:";
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatient.FormattingEnabled = true;
            this.cmbPatient.Location = new System.Drawing.Point(190, 104);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(301, 26);
            this.cmbPatient.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "Select Patient:";
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(190, 64);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(301, 26);
            this.cmbDoctor.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 18);
            this.label9.TabIndex = 12;
            this.label9.Text = "Select Doctor:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(217, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "Update Appointment Info:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(665, 416);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ManageAppointmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 823);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pnlUpdateAppointment);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ManageAppointmentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Appointments";
            this.Load += new System.EventHandler(this.ManageAppointmentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.pnlUpdateAppointment.ResumeLayout(false);
            this.pnlUpdateAppointment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlUpdateAppointment;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRefresh;
    }
}
