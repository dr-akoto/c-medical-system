namespace MedicalAppointmentSystem.Forms
{
    partial class MainForm
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
            this.btnViewDoctors = new System.Windows.Forms.Button();
            this.btnBookAppointment = new System.Windows.Forms.Button();
            this.btnManageAppointments = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Medical Appointment System";
            // 
            // btnViewDoctors
            // 
            this.btnViewDoctors.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDoctors.Location = new System.Drawing.Point(125, 84);
            this.btnViewDoctors.Name = "btnViewDoctors";
            this.btnViewDoctors.Size = new System.Drawing.Size(232, 43);
            this.btnViewDoctors.TabIndex = 1;
            this.btnViewDoctors.Text = "View Available Doctors";
            this.btnViewDoctors.UseVisualStyleBackColor = true;
            this.btnViewDoctors.Click += new System.EventHandler(this.btnViewDoctors_Click);
            // 
            // btnBookAppointment
            // 
            this.btnBookAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookAppointment.Location = new System.Drawing.Point(125, 146);
            this.btnBookAppointment.Name = "btnBookAppointment";
            this.btnBookAppointment.Size = new System.Drawing.Size(232, 43);
            this.btnBookAppointment.TabIndex = 2;
            this.btnBookAppointment.Text = "Book Appointment";
            this.btnBookAppointment.UseVisualStyleBackColor = true;
            this.btnBookAppointment.Click += new System.EventHandler(this.btnBookAppointment_Click);
            // 
            // btnManageAppointments
            // 
            this.btnManageAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageAppointments.Location = new System.Drawing.Point(125, 208);
            this.btnManageAppointments.Name = "btnManageAppointments";
            this.btnManageAppointments.Size = new System.Drawing.Size(232, 43);
            this.btnManageAppointments.TabIndex = 3;
            this.btnManageAppointments.Text = "Manage Appointments";
            this.btnManageAppointments.UseVisualStyleBackColor = true;
            this.btnManageAppointments.Click += new System.EventHandler(this.btnManageAppointments_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(125, 270);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(232, 43);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 353);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnManageAppointments);
            this.Controls.Add(this.btnBookAppointment);
            this.Controls.Add(this.btnViewDoctors);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medical Appointment System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewDoctors;
        private System.Windows.Forms.Button btnBookAppointment;
        private System.Windows.Forms.Button btnManageAppointments;
        private System.Windows.Forms.Button btnExit;
    }
}
