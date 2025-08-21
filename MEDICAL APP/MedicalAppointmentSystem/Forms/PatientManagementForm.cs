using System;
using System.Data;
using System.Windows.Forms;
using MedicalAppointmentSystem.Data;

namespace MedicalAppointmentSystem.Forms
{
    public partial class PatientManagementForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private bool isEditMode = false;
        private int editPatientId = -1;

        public PatientManagementForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void PatientManagementForm_Load(object sender, EventArgs e)
        {
            LoadPatients();
            
            // Set initial state of form
            SetFormState(false);
        }

        private void LoadPatients()
        {
            try
            {
                DataTable patients = dbHelper.GetAllPatients();
                dgvPatients.DataSource = patients;

                // Format DataGridView
                dgvPatients.Columns["PatientID"].HeaderText = "ID";
                dgvPatients.Columns["FullName"].HeaderText = "Patient Name";
                dgvPatients.Columns["Email"].HeaderText = "Email";

                // Resize columns
                dgvPatients.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormState(bool editing)
        {
            isEditMode = editing;
            btnSave.Text = editing ? "Update" : "Add";
            lblFormTitle.Text = editing ? "Edit Patient" : "Add New Patient";
            
            if (!editing)
            {
                txtFullName.Clear();
                txtEmail.Clear();
                editPatientId = -1;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SetFormState(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected patient's details
                DataGridViewRow selectedRow = dgvPatients.SelectedRows[0];
                editPatientId = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);
                txtFullName.Text = selectedRow.Cells["FullName"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();

                // Set form to edit mode
                SetFormState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter patient's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter patient's email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                bool success;
                
                if (isEditMode)
                {
                    // Update existing patient
                    success = dbHelper.UpdatePatient(
                        editPatientId,
                        txtFullName.Text.Trim(),
                        txtEmail.Text.Trim());

                    if (success)
                    {
                        MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Add new patient
                    int newPatientId = dbHelper.AddPatient(
                        txtFullName.Text.Trim(),
                        txtEmail.Text.Trim());

                    success = newPatientId > 0;

                    if (success)
                    {
                        MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Refresh the list and reset form if successful
                if (success)
                {
                    LoadPatients();
                    SetFormState(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected patient ID
                DataGridViewRow selectedRow = dgvPatients.SelectedRows[0];
                int patientId = Convert.ToInt32(selectedRow.Cells["PatientID"].Value);

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this patient? This will also delete all associated appointments.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = dbHelper.DeletePatient(patientId);

                    if (success)
                    {
                        MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatients();
                        SetFormState(false);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete patient. They may have associated appointments.", 
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
