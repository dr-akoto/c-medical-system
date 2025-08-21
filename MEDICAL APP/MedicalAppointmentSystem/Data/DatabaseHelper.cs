using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MedicalAppointmentSystem.Data
{
    // Model classes for serialization
    [Serializable]
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public bool Availability { get; set; }
        
        // Properties for UI that are not serialized
        [XmlIgnore]
        public string FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(FullName)) return string.Empty;
                return FullName.Split(' ')[0];
            }
        }
        
        [XmlIgnore]
        public string LastName
        {
            get
            {
                if (string.IsNullOrEmpty(FullName)) return string.Empty;
                string[] parts = FullName.Split(' ');
                return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty;
            }
        }
        
        [XmlIgnore]
        public string PhoneNumber { get; set; } = "";
    }
    
    [Serializable]
    public class Patient
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
    
    [Serializable]
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
    }

    public class DatabaseHelper
    {
        // File paths for local storage
        private readonly string doctorsFilePath;
        private readonly string patientsFilePath;
        private readonly string appointmentsFilePath;
        
        // In-memory data storage
        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<Appointment> appointments;

        public DatabaseHelper()
        {
            // Set up file paths in the application's directory
            string appDataPath = Path.Combine(Application.StartupPath, "Data");
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            doctorsFilePath = Path.Combine(appDataPath, "doctors.xml");
            patientsFilePath = Path.Combine(appDataPath, "patients.xml");
            appointmentsFilePath = Path.Combine(appDataPath, "appointments.xml");
            
            // Initialize data
            LoadData();
        }
        
        // Load data from XML files or create sample data if files don't exist
        private void LoadData()
        {
            // Load or create doctors
            if (File.Exists(doctorsFilePath))
            {
                doctors = DeserializeFromXml<List<Doctor>>(doctorsFilePath);
            }
            else
            {
                doctors = CreateSampleDoctors();
                SerializeToXml(doctors, doctorsFilePath);
            }
            
            // Load or create patients
            if (File.Exists(patientsFilePath))
            {
                patients = DeserializeFromXml<List<Patient>>(patientsFilePath);
            }
            else
            {
                patients = CreateSamplePatients();
                SerializeToXml(patients, patientsFilePath);
            }
            
            // Load or create appointments
            if (File.Exists(appointmentsFilePath))
            {
                appointments = DeserializeFromXml<List<Appointment>>(appointmentsFilePath);
            }
            else
            {
                appointments = CreateSampleAppointments();
                SerializeToXml(appointments, appointmentsFilePath);
            }
        }
        
        // Create sample doctors
        private List<Doctor> CreateSampleDoctors()
        {
            return new List<Doctor>
            {
                new Doctor { DoctorID = 1, FullName = "Dr. John Smith", Specialty = "Cardiology", Availability = true },
                new Doctor { DoctorID = 2, FullName = "Dr. Sarah Johnson", Specialty = "Pediatrics", Availability = true },
                new Doctor { DoctorID = 3, FullName = "Dr. Michael Brown", Specialty = "Orthopedics", Availability = true },
                new Doctor { DoctorID = 4, FullName = "Dr. Emily Davis", Specialty = "Dermatology", Availability = true },
                new Doctor { DoctorID = 5, FullName = "Dr. Robert Wilson", Specialty = "Neurology", Availability = false }
            };
        }
        
        // Create sample patients
        private List<Patient> CreateSamplePatients()
        {
            return new List<Patient>
            {
                new Patient { PatientID = 1, FullName = "James Anderson", Email = "james.anderson@email.com" },
                new Patient { PatientID = 2, FullName = "Emma Thompson", Email = "emma.t@email.com" },
                new Patient { PatientID = 3, FullName = "Daniel Mitchell", Email = "dan.mitchell@email.com" },
                new Patient { PatientID = 4, FullName = "Olivia Parker", Email = "olivia.p@email.com" },
                new Patient { PatientID = 5, FullName = "William Scott", Email = "will.scott@email.com" }
            };
        }
        
        // Create sample appointments
        private List<Appointment> CreateSampleAppointments()
        {
            return new List<Appointment>
            {
                new Appointment 
                { 
                    AppointmentID = 1, 
                    DoctorID = 1, 
                    PatientID = 2, 
                    AppointmentDate = DateTime.Now.AddDays(3), 
                    Notes = "Regular checkup" 
                },
                new Appointment 
                { 
                    AppointmentID = 2, 
                    DoctorID = 3, 
                    PatientID = 4, 
                    AppointmentDate = DateTime.Now.AddDays(5), 
                    Notes = "Follow-up appointment" 
                },
                new Appointment 
                { 
                    AppointmentID = 3, 
                    DoctorID = 2, 
                    PatientID = 1, 
                    AppointmentDate = DateTime.Now.AddDays(7), 
                    Notes = "Consultation" 
                }
            };
        }
        
        // Save data back to XML files
        private void SaveData()
        {
            SerializeToXml(doctors, doctorsFilePath);
            SerializeToXml(patients, patientsFilePath);
            SerializeToXml(appointments, appointmentsFilePath);
        }
        
        // Helper methods for XML serialization
        private void SerializeToXml<T>(T data, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error serializing data: {ex.Message}");
            }
        }
        
        private T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing data: {ex.Message}");
                return default;
            }
        }

        #region Doctor Methods

        // Get all doctors
        public DataTable GetAllDoctors()
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("Availability", typeof(bool));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            
            // Fill the DataTable with doctors
            foreach (var doctor in doctors)
            {
                DataRow row = dataTable.NewRow();
                row["DoctorID"] = doctor.DoctorID;
                row["FullName"] = doctor.FullName;
                row["Specialty"] = doctor.Specialty;
                row["Availability"] = doctor.Availability;
                
                // Extract FirstName and LastName from FullName
                string[] nameParts = doctor.FullName.Split(new char[] { ' ' }, 2);
                row["FirstName"] = nameParts[0];
                row["LastName"] = nameParts.Length > 1 ? nameParts[1] : "";
                row["PhoneNumber"] = doctor.PhoneNumber ?? "";
                
                dataTable.Rows.Add(row);
            }
            
            return dataTable;
        }

        // Get available doctors
        public DataTable GetAvailableDoctors()
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            
            // Fill the DataTable with available doctors
            foreach (var doctor in doctors.Where(d => d.Availability))
            {
                DataRow row = dataTable.NewRow();
                row["DoctorID"] = doctor.DoctorID;
                row["FullName"] = doctor.FullName;
                row["Specialty"] = doctor.Specialty;
                
                dataTable.Rows.Add(row);
            }
            
            return dataTable;
        }

        // Get doctor by ID
        public DataRow GetDoctorById(int doctorId)
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("Availability", typeof(bool));
            
            // Find the doctor
            var doctor = doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            
            if (doctor != null)
            {
                DataRow row = dataTable.NewRow();
                row["DoctorID"] = doctor.DoctorID;
                row["FullName"] = doctor.FullName;
                row["Specialty"] = doctor.Specialty;
                row["Availability"] = doctor.Availability;
                
                dataTable.Rows.Add(row);
                return dataTable.Rows[0];
            }
            
            return null;
        }

        // Add a new doctor with separate first and last name
        public int AddDoctor(string firstName, string lastName, string specialty, string phoneNumber)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            
            // Get the next available ID
            int newDoctorId = doctors.Count > 0 ? doctors.Max(d => d.DoctorID) + 1 : 1;
            
            // Create new doctor
            Doctor newDoctor = new Doctor
            {
                DoctorID = newDoctorId,
                FullName = fullName,
                Specialty = specialty,
                Availability = true,
                PhoneNumber = phoneNumber
            };
            
            // Add to collection
            doctors.Add(newDoctor);
            
            // Save changes
            SaveData();
            
            return newDoctorId;
        }

        // Update doctor with separate first and last name
        public bool UpdateDoctor(int doctorId, string firstName, string lastName, string specialty, string phoneNumber)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            
            // Find the doctor
            var doctor = doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            
            if (doctor != null)
            {
                // Update properties
                doctor.FullName = fullName;
                doctor.Specialty = specialty;
                doctor.PhoneNumber = phoneNumber;
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        // Delete doctor
        public bool DeleteDoctor(int doctorId)
        {
            // Check if doctor has appointments
            var doctorAppointments = appointments.Where(a => a.DoctorID == doctorId).ToList();
            
            if (doctorAppointments.Any())
            {
                // Remove all appointments for this doctor
                foreach (var appointment in doctorAppointments)
                {
                    appointments.Remove(appointment);
                }
            }
            
            // Find and remove the doctor
            var doctor = doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            
            if (doctor != null)
            {
                doctors.Remove(doctor);
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        // Update doctor availability
        public bool UpdateDoctorAvailability(int doctorId, bool isAvailable)
        {
            // Find the doctor
            var doctor = doctors.FirstOrDefault(d => d.DoctorID == doctorId);
            
            if (doctor != null)
            {
                // Update availability
                doctor.Availability = isAvailable;
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        #endregion

        #region Patient Methods

        // Get all patients
        public DataTable GetAllPatients()
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("PatientID", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            
            // Fill the DataTable with patients
            foreach (var patient in patients)
            {
                DataRow row = dataTable.NewRow();
                row["PatientID"] = patient.PatientID;
                row["FullName"] = patient.FullName;
                row["Email"] = patient.Email;
                
                dataTable.Rows.Add(row);
            }
            
            return dataTable;
        }

        // Get patient by ID
        public DataRow GetPatientById(int patientId)
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("PatientID", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            
            // Find the patient
            var patient = patients.FirstOrDefault(p => p.PatientID == patientId);
            
            if (patient != null)
            {
                DataRow row = dataTable.NewRow();
                row["PatientID"] = patient.PatientID;
                row["FullName"] = patient.FullName;
                row["Email"] = patient.Email;
                
                dataTable.Rows.Add(row);
                return dataTable.Rows[0];
            }
            
            return null;
        }
        
        // Add a new patient
        public int AddPatient(string fullName, string email)
        {
            // Get the next available ID
            int newPatientId = patients.Count > 0 ? patients.Max(p => p.PatientID) + 1 : 1;
            
            // Create new patient
            Patient newPatient = new Patient
            {
                PatientID = newPatientId,
                FullName = fullName,
                Email = email
            };
            
            // Add to collection
            patients.Add(newPatient);
            
            // Save changes
            SaveData();
            
            return newPatientId;
        }

        // Update patient
        public bool UpdatePatient(int patientId, string fullName, string email)
        {
            // Find the patient
            var patient = patients.FirstOrDefault(p => p.PatientID == patientId);
            
            if (patient != null)
            {
                // Update properties
                patient.FullName = fullName;
                patient.Email = email;
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        // Delete patient
        public bool DeletePatient(int patientId)
        {
            // Check if patient has appointments
            var patientAppointments = appointments.Where(a => a.PatientID == patientId).ToList();
            
            if (patientAppointments.Any())
            {
                // Remove all appointments for this patient
                foreach (var appointment in patientAppointments)
                {
                    appointments.Remove(appointment);
                }
            }
            
            // Find and remove the patient
            var patient = patients.FirstOrDefault(p => p.PatientID == patientId);
            
            if (patient != null)
            {
                patients.Remove(patient);
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        #endregion

        #region Appointment Methods

        // Create a new appointment
        public int CreateAppointment(int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            // Get the next available ID
            int newAppointmentId = appointments.Count > 0 ? appointments.Max(a => a.AppointmentID) + 1 : 1;
            
            // Create new appointment
            Appointment newAppointment = new Appointment
            {
                AppointmentID = newAppointmentId,
                DoctorID = doctorId,
                PatientID = patientId,
                AppointmentDate = appointmentDate,
                Notes = notes
            };
            
            // Add to collection
            appointments.Add(newAppointment);
            
            // Save changes
            SaveData();
            
            return newAppointmentId;
        }

        // Get all appointments
        public DataTable GetAllAppointments()
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("AppointmentID", typeof(int));
            dataTable.Columns.Add("AppointmentDate", typeof(DateTime));
            dataTable.Columns.Add("Notes", typeof(string));
            dataTable.Columns.Add("DoctorName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("PatientName", typeof(string));
            dataTable.Columns.Add("PatientEmail", typeof(string));
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("PatientID", typeof(int));
            
            // Fill the DataTable with appointments
            foreach (var appointment in appointments.OrderByDescending(a => a.AppointmentDate))
            {
                var doctor = doctors.FirstOrDefault(d => d.DoctorID == appointment.DoctorID);
                var patient = patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                
                if (doctor != null && patient != null)
                {
                    DataRow row = dataTable.NewRow();
                    row["AppointmentID"] = appointment.AppointmentID;
                    row["AppointmentDate"] = appointment.AppointmentDate;
                    row["Notes"] = appointment.Notes ?? "";
                    row["DoctorName"] = doctor.FullName;
                    row["Specialty"] = doctor.Specialty;
                    row["PatientName"] = patient.FullName;
                    row["PatientEmail"] = patient.Email;
                    row["DoctorID"] = appointment.DoctorID;
                    row["PatientID"] = appointment.PatientID;
                    
                    dataTable.Rows.Add(row);
                }
            }
            
            return dataTable;
        }

        // Get appointments by doctor ID (simplified version for checking if doctor has appointments)
        public DataTable GetAppointmentsByDoctorId(int doctorId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AppointmentID FROM Appointments WHERE DoctorID = @DoctorID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", doctorId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting appointments by doctor ID: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get appointments by doctor ID with details
        public DataTable GetAppointmentsByDoctor(int doctorId)
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("AppointmentID", typeof(int));
            dataTable.Columns.Add("AppointmentDate", typeof(DateTime));
            dataTable.Columns.Add("Notes", typeof(string));
            dataTable.Columns.Add("DoctorName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("PatientName", typeof(string));
            dataTable.Columns.Add("PatientEmail", typeof(string));
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("PatientID", typeof(int));
            
            // Fill the DataTable with appointments for this doctor
            var doctorAppointments = appointments
                .Where(a => a.DoctorID == doctorId)
                .OrderByDescending(a => a.AppointmentDate);
                
            foreach (var appointment in doctorAppointments)
            {
                var doctor = doctors.FirstOrDefault(d => d.DoctorID == appointment.DoctorID);
                var patient = patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                
                if (doctor != null && patient != null)
                {
                    DataRow row = dataTable.NewRow();
                    row["AppointmentID"] = appointment.AppointmentID;
                    row["AppointmentDate"] = appointment.AppointmentDate;
                    row["Notes"] = appointment.Notes ?? "";
                    row["DoctorName"] = doctor.FullName;
                    row["Specialty"] = doctor.Specialty;
                    row["PatientName"] = patient.FullName;
                    row["PatientEmail"] = patient.Email;
                    row["DoctorID"] = appointment.DoctorID;
                    row["PatientID"] = appointment.PatientID;
                    
                    dataTable.Rows.Add(row);
                }
            }
            
            return dataTable;
        }

        // Get appointments by patient ID
        public DataTable GetAppointmentsByPatient(int patientId)
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("AppointmentID", typeof(int));
            dataTable.Columns.Add("AppointmentDate", typeof(DateTime));
            dataTable.Columns.Add("Notes", typeof(string));
            dataTable.Columns.Add("DoctorName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("PatientName", typeof(string));
            dataTable.Columns.Add("PatientEmail", typeof(string));
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("PatientID", typeof(int));
            
            // Fill the DataTable with appointments for this patient
            var patientAppointments = appointments
                .Where(a => a.PatientID == patientId)
                .OrderByDescending(a => a.AppointmentDate);
                
            foreach (var appointment in patientAppointments)
            {
                var doctor = doctors.FirstOrDefault(d => d.DoctorID == appointment.DoctorID);
                var patient = patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                
                if (doctor != null && patient != null)
                {
                    DataRow row = dataTable.NewRow();
                    row["AppointmentID"] = appointment.AppointmentID;
                    row["AppointmentDate"] = appointment.AppointmentDate;
                    row["Notes"] = appointment.Notes ?? "";
                    row["DoctorName"] = doctor.FullName;
                    row["Specialty"] = doctor.Specialty;
                    row["PatientName"] = patient.FullName;
                    row["PatientEmail"] = patient.Email;
                    row["DoctorID"] = appointment.DoctorID;
                    row["PatientID"] = appointment.PatientID;
                    
                    dataTable.Rows.Add(row);
                }
            }
            
            return dataTable;
        }

        // Get appointment by ID
        public DataRow GetAppointmentById(int appointmentId)
        {
            DataTable dataTable = new DataTable();
            
            // Add columns to the DataTable
            dataTable.Columns.Add("AppointmentID", typeof(int));
            dataTable.Columns.Add("AppointmentDate", typeof(DateTime));
            dataTable.Columns.Add("Notes", typeof(string));
            dataTable.Columns.Add("DoctorName", typeof(string));
            dataTable.Columns.Add("Specialty", typeof(string));
            dataTable.Columns.Add("PatientName", typeof(string));
            dataTable.Columns.Add("PatientEmail", typeof(string));
            dataTable.Columns.Add("DoctorID", typeof(int));
            dataTable.Columns.Add("PatientID", typeof(int));
            
            // Find the appointment
            var appointment = appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            
            if (appointment != null)
            {
                var doctor = doctors.FirstOrDefault(d => d.DoctorID == appointment.DoctorID);
                var patient = patients.FirstOrDefault(p => p.PatientID == appointment.PatientID);
                
                if (doctor != null && patient != null)
                {
                    DataRow row = dataTable.NewRow();
                    row["AppointmentID"] = appointment.AppointmentID;
                    row["AppointmentDate"] = appointment.AppointmentDate;
                    row["Notes"] = appointment.Notes ?? "";
                    row["DoctorName"] = doctor.FullName;
                    row["Specialty"] = doctor.Specialty;
                    row["PatientName"] = patient.FullName;
                    row["PatientEmail"] = patient.Email;
                    row["DoctorID"] = appointment.DoctorID;
                    row["PatientID"] = appointment.PatientID;
                    
                    dataTable.Rows.Add(row);
                    return dataTable.Rows[0];
                }
            }
            
            return null;
        }

        // Update appointment
        public bool UpdateAppointment(int appointmentId, int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            // Find the appointment
            var appointment = appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            
            if (appointment != null)
            {
                // Update properties
                appointment.DoctorID = doctorId;
                appointment.PatientID = patientId;
                appointment.AppointmentDate = appointmentDate;
                appointment.Notes = notes;
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        // Delete appointment
        public bool DeleteAppointment(int appointmentId)
        {
            // Find and remove the appointment
            var appointment = appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            
            if (appointment != null)
            {
                appointments.Remove(appointment);
                
                // Save changes
                SaveData();
                
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
