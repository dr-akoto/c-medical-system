using System;
using System.Data;
using System.Windows.Forms;
using MedicalAppointmentSystem.Data;

namespace MedicalAppointmentSystem.Forms
{
    public partial class DoctorManagementForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private bool isEditMode = false;
        private int editDoctorId = -1;

        public DoctorManagementForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void DoctorManagementForm_Load(object sender, EventArgs e)
        {
            LoadDoctors();
            cmbSpecialty.Items.AddRange(new string[] {
                "Cardiology", 
                "Dermatology", 
                "Neurology", 
                "Orthopedics", 
                "Pediatrics", 
                "Psychiatry", 
                "General Medicine", 
                "Gynecology"
            });
            
            // Set initial state of form
            SetFormState(false);
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

        private void SetFormState(bool editing)
        {
            isEditMode = editing;
            btnSave.Text = editing ? "Update" : "Add";
            lblFormTitle.Text = editing ? "Edit Doctor" : "Add New Doctor";
            
            if (!editing)
            {
                txtFullName.Clear();
                cmbSpecialty.SelectedIndex = -1;
                chkAvailable.Checked = true;
                editDoctorId = -1;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SetFormState(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected doctor's details
                DataGridViewRow selectedRow = dgvDoctors.SelectedRows[0];
                editDoctorId = Convert.ToInt32(selectedRow.Cells["DoctorID"].Value);
                txtFullName.Text = selectedRow.Cells["FullName"].Value.ToString();
                cmbSpecialty.Text = selectedRow.Cells["Specialty"].Value.ToString();
                chkAvailable.Checked = Convert.ToBoolean(selectedRow.Cells["Availability"].Value);

                // Set form to edit mode
                SetFormState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctor details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter doctor's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (cmbSpecialty.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cmbSpecialty.Text))
            {
                MessageBox.Show("Please select or enter a specialty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSpecialty.Focus();
                return;
            }

            try
            {
                bool success;
                
                if (isEditMode)
                {
                    // Update existing doctor
                    success = dbHelper.UpdateDoctor(
                        editDoctorId,
                        txtFullName.Text.Trim(),
                        cmbSpecialty.Text.Trim(),
                        chkAvailable.Checked);

                    if (success)
                    {
                        MessageBox.Show("Doctor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Add new doctor
                    int newDoctorId = dbHelper.AddDoctor(
                        txtFullName.Text.Trim(),
                        cmbSpecialty.Text.Trim(),
                        chkAvailable.Checked);

                    success = newDoctorId > 0;

                    if (success)
                    {
                        MessageBox.Show("Doctor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Refresh the list and reset form if successful
                if (success)
                {
                    LoadDoctors();
                    SetFormState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving doctor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected doctor ID
                DataGridViewRow selectedRow = dgvDoctors.SelectedRows[0];
                int doctorId = Convert.ToInt32(selectedRow.Cells["DoctorID"].Value);

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this doctor? This will also delete all associated appointments.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = dbHelper.DeleteDoctor(doctorId);

                    if (success)
                    {
                        MessageBox.Show("Doctor deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDoctors();
                        SetFormState(false);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete doctor. They may have associated appointments.", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting doctor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormState(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
