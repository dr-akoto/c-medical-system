namespace MedicalAppointmentSystem.Forms
{
    partial class AppointmentForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnBookAppointment = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Book New Appointment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Doctor:";
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(191, 63);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(301, 26);
            this.cmbDoctor.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Select Patient:";
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatient.FormattingEnabled = true;
            this.cmbPatient.Location = new System.Drawing.Point(191, 109);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(301, 26);
            this.cmbPatient.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Appointment Date:";
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpAppointmentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppointmentDate.Location = new System.Drawing.Point(191, 153);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(301, 24);
            this.dtpAppointmentDate.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(191, 201);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(301, 96);
            this.txtNotes.TabIndex = 8;
            // 
            // btnBookAppointment
            // 
            this.btnBookAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookAppointment.Location = new System.Drawing.Point(191, 327);
            this.btnBookAppointment.Name = "btnBookAppointment";
            this.btnBookAppointment.Size = new System.Drawing.Size(165, 39);
            this.btnBookAppointment.TabIndex = 9;
            this.btnBookAppointment.Text = "Book Appointment";
            this.btnBookAppointment.UseVisualStyleBackColor = true;
            this.btnBookAppointment.Click += new System.EventHandler(this.btnBookAppointment_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(380, 327);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 39);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBookAppointment);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpAppointmentDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPatient);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDoctor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AppointmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Appointment";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnBookAppointment;
        private System.Windows.Forms.Button btnCancel;
    }
}
