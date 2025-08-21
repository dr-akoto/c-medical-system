using System;
using System.Data;
using System.Windows.Forms;
using MedicalAppointmentSystem.Data;

namespace MedicalAppointmentSystem.Forms
{
    public partial class DoctorListForm : Form
    {
        private readonly DatabaseHelper dbHelper;

        public DoctorListForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void DoctorListForm_Load(object sender, EventArgs e)
        {
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                DataTable doctors = dbHelper.GetAllDoctors();
                dgvDoctors.DataSource = doctors;

                // Format DataGridView
                dgvDoctors.Columns["DoctorID"].HeaderText = "ID";
                dgvDoctors.Columns["FullName"].HeaderText = "Doctor Name";
                dgvDoctors.Columns["Specialty"].HeaderText = "Specialty";
                dgvDoctors.Columns["Availability"].HeaderText = "Available";

                // Resize columns
                dgvDoctors.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbShowOnlyAvailable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbShowOnlyAvailable.Checked)
                {
                    // Show only available doctors
                    DataTable availableDoctors = dbHelper.GetAvailableDoctors();
                    dgvDoctors.DataSource = availableDoctors;

                    // Format DataGridView
                    dgvDoctors.Columns["DoctorID"].HeaderText = "ID";
                    dgvDoctors.Columns["FullName"].HeaderText = "Doctor Name";
                    dgvDoctors.Columns["Specialty"].HeaderText = "Specialty";
                }
                else
                {
                    // Show all doctors
                    LoadDoctors();
                }

                // Resize columns
                dgvDoctors.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering doctors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
