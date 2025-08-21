using System;
using System.Data;
using System.Windows.Forms;
using MedicalAppointmentSystem.Data;

namespace MedicalAppointmentSystem.Forms
{
    public partial class AppointmentForm : Form
    {
        private readonly DatabaseHelper dbHelper;

        public AppointmentForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            // Set minimum date to today
            dtpAppointmentDate.MinDate = DateTime.Today;

            // Load doctors and patients
            LoadDoctors();
            LoadPatients();
        }

        private void LoadDoctors()
        {
            try
            {
                DataTable doctors = dbHelper.GetAvailableDoctors();
                cmbDoctor.DataSource = doctors;
                cmbDoctor.DisplayMember = "FullName";
                cmbDoctor.ValueMember = "DoctorID";

                // Add a default selection prompt
                if (doctors.Rows.Count > 0)
                {
                    cmbDoctor.SelectedIndex = -1;
                    cmbDoctor.Text = "-- Select Doctor --";
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
                    cmbPatient.Text = "-- Select Patient --";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
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
                // Create appointment
                int appointmentId = dbHelper.CreateAppointment(doctorId, patientId, appointmentDate, notes);

                if (appointmentId > 0)
                {
                    MessageBox.Show("Appointment booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear form
                    cmbDoctor.SelectedIndex = -1;
                    cmbPatient.SelectedIndex = -1;
                    dtpAppointmentDate.Value = DateTime.Today;
                    txtNotes.Clear();
                }
                else
                {
                    MessageBox.Show("Failed to book appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
