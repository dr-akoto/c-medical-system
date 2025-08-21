using System;
using System.Data;
using System.Windows.Forms;
using MedicalAppointmentSystem.Data;

namespace MedicalAppointmentSystem.Forms
{
    public partial class ManageAppointmentsForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private int selectedAppointmentId = -1;

        public ManageAppointmentsForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void ManageAppointmentsForm_Load(object sender, EventArgs e)
        {
            LoadAppointments();
            LoadDoctors();
            LoadPatients();

            // Set minimum date to today
            dtpAppointmentDate.MinDate = DateTime.Today;

            // Hide update panel initially
            pnlUpdateAppointment.Visible = false;
        }

        private void LoadAppointments()
        {
            try
            {
                DataTable appointments = dbHelper.GetAllAppointments();
                dgvAppointments.DataSource = appointments;

                // Format DataGridView columns
                dgvAppointments.Columns["AppointmentID"].HeaderText = "ID";
                dgvAppointments.Columns["AppointmentDate"].HeaderText = "Date & Time";
                dgvAppointments.Columns["DoctorName"].HeaderText = "Doctor";
                dgvAppointments.Columns["Specialty"].HeaderText = "Specialty";
                dgvAppointments.Columns["PatientName"].HeaderText = "Patient";
                dgvAppointments.Columns["PatientEmail"].HeaderText = "Email";
                dgvAppointments.Columns["Notes"].HeaderText = "Notes";

                // Hide IDs from view
                dgvAppointments.Columns["DoctorID"].Visible = false;
                dgvAppointments.Columns["PatientID"].Visible = false;

                // Resize columns
                dgvAppointments.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDoctors()
        {
            try
            {
                DataTable doctors = dbHelper.GetAllDoctors();
                cmbDoctor.DataSource = doctors;
                cmbDoctor.DisplayMember = "FullName";
                cmbDoctor.ValueMember = "DoctorID";

                // Add a default selection prompt
                if (doctors.Rows.Count > 0)
                {
                    cmbDoctor.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPatients()
        {
            try
            {
                DataTable patients = dbHelper.GetAllPatients();
                cmbPatient.DataSource = patients;
                cmbPatient.DisplayMember = "FullName";
                cmbPatient.ValueMember = "PatientID";

                // Add a default selection prompt
                if (patients.Rows.Count > 0)
                {
                    cmbPatient.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterAppointments();
        }

        private void cmbFilterDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAppointments();
        }

        private void FilterAppointments()
        {
            try
            {
                // Get the DataTable from DataGridView
                DataTable dt = (DataTable)dgvAppointments.DataSource;
                if (dt == null) return;

                // Create a copy to avoid "Collection is modified" errors
                DataTable filteredDt = dt.Clone();

                string searchText = txtSearch.Text.ToLower();

                // Apply filters
                foreach (DataRow row in dt.Rows)
                {
                    bool includeRow = true;

                    // Apply search filter
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        string patientName = row["PatientName"].ToString().ToLower();
                        string doctorName = row["DoctorName"].ToString().ToLower();
                        string notes = row["Notes"].ToString().ToLower();

                        includeRow = patientName.Contains(searchText) ||
                                    doctorName.Contains(searchText) ||
                                    notes.Contains(searchText);
                    }

                    // Include row if it passes all filters
                    if (includeRow)
                    {
                        filteredDt.ImportRow(row);
                    }
                }

                // Set as DataSource
                dgvAppointments.DataSource = filteredDt;

                // Resize columns
                dgvAppointments.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected appointment ID
                DataGridViewRow selectedRow = dgvAppointments.SelectedRows[0];
                selectedAppointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);

                // Populate edit form with appointment details
                cmbDoctor.SelectedValue = selectedRow.Cells["DoctorID"].Value;
                cmbPatient.SelectedValue = selectedRow.Cells["PatientID"].Value;
                dtpAppointmentDate.Value = Convert.ToDateTime(selectedRow.Cells["AppointmentDate"].Value);
                txtNotes.Text = selectedRow.Cells["Notes"].Value.ToString();

                // Show update panel
                pnlUpdateAppointment.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected appointment ID
                DataGridViewRow selectedRow = dgvAppointments.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);

                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete appointment
                    bool success = dbHelper.DeleteAppointment(appointmentId);

                    if (success)
                    {
                        MessageBox.Show("Appointment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reload appointments
                        LoadAppointments();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (cmbDoctor.SelectedValue == null)
            {
                MessageBox.Show("Please select a doctor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDoctor.Focus();
                return;
            }

            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPatient.Focus();
                return;
            }

            // Get selected values
            int doctorId = Convert.ToInt32(cmbDoctor.SelectedValue);
            int patientId = Convert.ToInt32(cmbPatient.SelectedValue);
            DateTime appointmentDate = dtpAppointmentDate.Value;
            string notes = txtNotes.Text.Trim();

            try
            {
                // Update appointment
                bool success = dbHelper.UpdateAppointment(selectedAppointmentId, doctorId, patientId, appointmentDate, notes);

                if (success)
                {
                    MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hide update panel
                    pnlUpdateAppointment.Visible = false;

                    // Reset selected appointment ID
                    selectedAppointmentId = -1;

                    // Clear form
                    cmbDoctor.SelectedIndex = -1;
                    cmbPatient.SelectedIndex = -1;
                    dtpAppointmentDate.Value = DateTime.Today;
                    txtNotes.Clear();

                    // Reload appointments
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show("Failed to update appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Hide update panel
            pnlUpdateAppointment.Visible = false;

            // Reset selected appointment ID
            selectedAppointmentId = -1;

            // Clear form
            cmbDoctor.SelectedIndex = -1;
            cmbPatient.SelectedIndex = -1;
            dtpAppointmentDate.Value = DateTime.Today;
            txtNotes.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointments();
            txtSearch.Clear();
        }
    }
}
