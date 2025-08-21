using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnViewDoctors_Click(object sender, EventArgs e)
        {
            DoctorListForm doctorListForm = new DoctorListForm();
            doctorListForm.ShowDialog();
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            AppointmentForm appointmentForm = new AppointmentForm();
            appointmentForm.ShowDialog();
        }

        private void btnManageAppointments_Click(object sender, EventArgs e)
        {
            ManageAppointmentsForm manageAppointmentsForm = new ManageAppointmentsForm();
            manageAppointmentsForm.ShowDialog();
        }

        private void btnManageDoctors_Click(object sender, EventArgs e)
        {
            DoctorManagementForm doctorManagementForm = new DoctorManagementForm();
            doctorManagementForm.ShowDialog();
        }

        private void btnManagePatients_Click(object sender, EventArgs e)
        {
            PatientManagementForm patientManagementForm = new PatientManagementForm();
            patientManagementForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
}
