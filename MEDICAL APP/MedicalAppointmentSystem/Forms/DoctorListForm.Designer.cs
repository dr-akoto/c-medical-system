namespace MedicalAppointmentSystem.Forms
{
    partial class DoctorListForm
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
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbShowOnlyAvailable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Doctors List:";
            // 
            // dgvDoctors
            // 
            this.dgvDoctors.AllowUserToAddRows = false;
            this.dgvDoctors.AllowUserToDeleteRows = false;
            this.dgvDoctors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctors.Location = new System.Drawing.Point(17, 84);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.RowHeadersWidth = 51;
            this.dgvDoctors.RowTemplate.Height = 24;
            this.dgvDoctors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctors.Size = new System.Drawing.Size(654, 312);
            this.dgvDoctors.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(562, 414);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbShowOnlyAvailable
            // 
            this.cbShowOnlyAvailable.AutoSize = true;
            this.cbShowOnlyAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowOnlyAvailable.Location = new System.Drawing.Point(17, 57);
            this.cbShowOnlyAvailable.Name = "cbShowOnlyAvailable";
            this.cbShowOnlyAvailable.Size = new System.Drawing.Size(227, 22);
            this.cbShowOnlyAvailable.TabIndex = 3;
            this.cbShowOnlyAvailable.Text = "Show only available doctors";
            this.cbShowOnlyAvailable.UseVisualStyleBackColor = true;
            this.cbShowOnlyAvailable.CheckedChanged += new System.EventHandler(this.cbShowOnlyAvailable_CheckedChanged);
            // 
            // DoctorListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 465);
            this.Controls.Add(this.cbShowOnlyAvailable);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvDoctors);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DoctorListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctors List";
            this.Load += new System.EventHandler(this.DoctorListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDoctors;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox cbShowOnlyAvailable;
    }
}
