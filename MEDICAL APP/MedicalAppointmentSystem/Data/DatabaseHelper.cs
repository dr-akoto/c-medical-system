using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MedicalAppointmentSystem.Data
{
    // Model classes for reference
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public bool Availability { get; set; }
        
        // Properties for UI
        public string FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(FullName)) return string.Empty;
                return FullName.Split(' ')[0];
            }
        }
        
        public string LastName
        {
            get
            {
                if (string.IsNullOrEmpty(FullName)) return string.Empty;
                string[] parts = FullName.Split(' ');
                return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty;
            }
        }
        
        public string PhoneNumber { get; set; } = "";
    }
    
    public class Patient
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
    
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
        private readonly string connectionString;
        private readonly string dbPath;
        
        public DatabaseHelper()
        {
            // Set up database path in the application's directory
            string appDataPath = Path.Combine(Application.StartupPath, "Data");
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            
            dbPath = Path.Combine(appDataPath, "MedicalSystem.db");
            connectionString = $"Data Source={dbPath};Version=3;";
            
            // Create database and tables if they don't exist
            InitializeDatabase();
        }
        
        // Initialize the SQLite database and create tables if they don't exist
        private void InitializeDatabase()
        {
            bool dbExists = File.Exists(dbPath);
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                // Create tables if they don't exist
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // Create Doctors table
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Doctors (
                        DoctorID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        Specialty TEXT NOT NULL,
                        Availability INTEGER NOT NULL,
                        PhoneNumber TEXT
                    )";
                    command.ExecuteNonQuery();
                    
                    // Create Patients table
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Patients (
                        PatientID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        Email TEXT NOT NULL
                    )";
                    command.ExecuteNonQuery();
                    
                    // Create Appointments table
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Appointments (
                        AppointmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        DoctorID INTEGER NOT NULL,
                        PatientID INTEGER NOT NULL,
                        AppointmentDate TEXT NOT NULL,
                        Notes TEXT,
                        FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
                        FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
                    )";
                    command.ExecuteNonQuery();
                }
                
                // Insert sample data if this is a new database
                if (!dbExists)
                {
                    InsertSampleData(connection);
                }
            }
        }
        
        // Insert sample data into the database
        private void InsertSampleData(SQLiteConnection connection)
        {
            // Insert sample doctors
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = @"
                INSERT INTO Doctors (FullName, Specialty, Availability, PhoneNumber) VALUES 
                ('Dr. John Smith', 'Cardiology', 1, '555-123-4567'),
                ('Dr. Sarah Johnson', 'Pediatrics', 1, '555-234-5678'),
                ('Dr. Michael Brown', 'Orthopedics', 1, '555-345-6789'),
                ('Dr. Emily Davis', 'Dermatology', 1, '555-456-7890'),
                ('Dr. Robert Wilson', 'Neurology', 0, '555-567-8901')";
                command.ExecuteNonQuery();
                
                // Insert sample patients
                command.CommandText = @"
                INSERT INTO Patients (FullName, Email) VALUES 
                ('James Anderson', 'james.anderson@email.com'),
                ('Emma Thompson', 'emma.t@email.com'),
                ('Daniel Mitchell', 'dan.mitchell@email.com'),
                ('Olivia Parker', 'olivia.p@email.com'),
                ('William Scott', 'will.scott@email.com')";
                command.ExecuteNonQuery();
                
                // Insert sample appointments
                DateTime today = DateTime.Now;
                command.CommandText = $@"
                INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) VALUES 
                (1, 2, '{today.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss")}', 'Regular checkup'),
                (3, 4, '{today.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss")}', 'Follow-up appointment'),
                (2, 1, '{today.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss")}', 'Consultation')";
                command.ExecuteNonQuery();
            }
        }

        #region Doctor Methods

        // Get all doctors
        public DataTable GetAllDoctors()
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DoctorID, FullName, Specialty, Availability, PhoneNumber FROM Doctors";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        // Add FirstName and LastName columns for use in the DoctorManagementForm
                        if (!dataTable.Columns.Contains("FirstName"))
                        {
                            dataTable.Columns.Add("FirstName", typeof(string));
                        }
                        if (!dataTable.Columns.Contains("LastName"))
                        {
                            dataTable.Columns.Add("LastName", typeof(string));
                        }
                        if (!dataTable.Columns.Contains("PhoneNumber"))
                        {
                            dataTable.Columns.Add("PhoneNumber", typeof(string));
                        }
                        
                        // Extract FirstName and LastName from FullName
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string fullName = row["FullName"].ToString();
                            string[] nameParts = fullName.Split(new char[] { ' ' }, 2);
                            
                            if (nameParts.Length > 0)
                            {
                                row["FirstName"] = nameParts[0];
                                row["LastName"] = nameParts.Length > 1 ? nameParts[1] : "";
                                row["PhoneNumber"] = ""; // Default empty phone number
                            }
                        }
                    }
                }
            }
            
            return dataTable;
        }

        // Get available doctors
        public DataTable GetAvailableDoctors()
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DoctorID, FullName, Specialty FROM Doctors WHERE Availability = 1";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            
            return dataTable;
        }

        // Get doctor by ID
        public DataRow GetDoctorById(int doctorId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DoctorID, FullName, Specialty, Availability, PhoneNumber FROM Doctors WHERE DoctorID = @DoctorID";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
            }
            
            return null;
        }

        // Add a new doctor with separate first and last name
        public int AddDoctor(string firstName, string lastName, string specialty, string phoneNumber)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            int newDoctorId = 0;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Add new doctor
                        string query = @"INSERT INTO Doctors (FullName, Specialty, Availability, PhoneNumber) 
                                       VALUES (@FullName, @Specialty, @Availability, @PhoneNumber);
                                       SELECT last_insert_rowid();";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Specialty", specialty);
                            command.Parameters.AddWithValue("@Availability", 1); // Default to available
                            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber ?? "");
                            
                            // Get the new doctor ID
                            newDoctorId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return newDoctorId;
        }

        // Update doctor with separate first and last name
        public bool UpdateDoctor(int doctorId, string firstName, string lastName, string specialty, string phoneNumber)
        {
            string fullName = $"{firstName} {lastName}".Trim();
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update existing doctor
                        string query = @"UPDATE Doctors SET 
                                       FullName = @FullName, 
                                       Specialty = @Specialty, 
                                       PhoneNumber = @PhoneNumber 
                                       WHERE DoctorID = @DoctorID";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@DoctorID", doctorId);
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Specialty", specialty);
                            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber ?? "");
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        // Delete doctor
        public bool DeleteDoctor(int doctorId)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Check if doctor has appointments
                        string checkQuery = "SELECT COUNT(*) FROM Appointments WHERE DoctorID = @DoctorID";
                        
                        using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                            int appointmentCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                            
                            if (appointmentCount > 0)
                            {
                                // Delete all appointments for this doctor
                                string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE DoctorID = @DoctorID";
                                using (SQLiteCommand deleteAppointmentsCommand = new SQLiteCommand(deleteAppointmentsQuery, connection, transaction))
                                {
                                    deleteAppointmentsCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                                    deleteAppointmentsCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        
                        // Delete the doctor
                        string deleteQuery = "DELETE FROM Doctors WHERE DoctorID = @DoctorID";
                        
                        using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@DoctorID", doctorId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        // Update doctor availability
        public bool UpdateDoctorAvailability(int doctorId, bool isAvailable)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update doctor availability
                        string query = "UPDATE Doctors SET Availability = @Availability WHERE DoctorID = @DoctorID";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@DoctorID", doctorId);
                            command.Parameters.AddWithValue("@Availability", isAvailable ? 1 : 0);
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        #endregion

        #region Patient Methods

        // Get all patients
        public DataTable GetAllPatients()
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PatientID, FullName, Email FROM Patients";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            
            return dataTable;
        }

        // Get patient by ID
        public DataRow GetPatientById(int patientId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PatientID, FullName, Email FROM Patients WHERE PatientID = @PatientID";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
            }
            
            return null;
        }
        
        // Add a new patient
        public int AddPatient(string fullName, string email)
        {
            int newPatientId = 0;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Add new patient
                        string query = @"INSERT INTO Patients (FullName, Email) 
                                       VALUES (@FullName, @Email);
                                       SELECT last_insert_rowid();";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Email", email);
                            
                            // Get the new patient ID
                            newPatientId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return newPatientId;
        }

        // Update patient
        public bool UpdatePatient(int patientId, string fullName, string email)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update existing patient
                        string query = @"UPDATE Patients SET 
                                       FullName = @FullName, 
                                       Email = @Email 
                                       WHERE PatientID = @PatientID";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@PatientID", patientId);
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Email", email);
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        // Delete patient
        public bool DeletePatient(int patientId)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Check if patient has appointments
                        string checkQuery = "SELECT COUNT(*) FROM Appointments WHERE PatientID = @PatientID";
                        
                        using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@PatientID", patientId);
                            int appointmentCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                            
                            if (appointmentCount > 0)
                            {
                                // Delete all appointments for this patient
                                string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE PatientID = @PatientID";
                                using (SQLiteCommand deleteAppointmentsCommand = new SQLiteCommand(deleteAppointmentsQuery, connection, transaction))
                                {
                                    deleteAppointmentsCommand.Parameters.AddWithValue("@PatientID", patientId);
                                    deleteAppointmentsCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        
                        // Delete the patient
                        string deleteQuery = "DELETE FROM Patients WHERE PatientID = @PatientID";
                        
                        using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@PatientID", patientId);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();
                            
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        #endregion

        #region Appointment Methods

        // Create a new appointment
        public int CreateAppointment(int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            int newAppointmentId = 0;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Add new appointment
                        string query = @"INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) 
                                       VALUES (@DoctorID, @PatientID, @AppointmentDate, @Notes);
                                       SELECT last_insert_rowid();";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@DoctorID", doctorId);
                            command.Parameters.AddWithValue("@PatientID", patientId);
                            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Notes", notes ?? "");
                            
                            // Get the new appointment ID
                            newAppointmentId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return newAppointmentId;
        }

        // Get all appointments
        public DataTable GetAllAppointments()
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        a.AppointmentID, 
                        a.AppointmentDate, 
                        a.Notes, 
                        d.FullName as DoctorName, 
                        d.Specialty, 
                        p.FullName as PatientName, 
                        p.Email as PatientEmail, 
                        a.DoctorID, 
                        a.PatientID 
                    FROM Appointments a
                    JOIN Doctors d ON a.DoctorID = d.DoctorID
                    JOIN Patients p ON a.PatientID = p.PatientID
                    ORDER BY a.AppointmentDate DESC";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        // Convert AppointmentDate from string to DateTime
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["AppointmentDate"] != DBNull.Value)
                            {
                                string dateStr = row["AppointmentDate"].ToString();
                                if (DateTime.TryParse(dateStr, out DateTime date))
                                {
                                    row["AppointmentDate"] = date;
                                }
                            }
                        }
                    }
                }
            }
            
            return dataTable;
        }

        // Get appointments by doctor ID (simplified version for checking if doctor has appointments)
        public DataTable GetAppointmentsByDoctorId(int doctorId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT AppointmentID FROM Appointments WHERE DoctorID = @DoctorID";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            
            return dataTable;
        }

        // Get appointments by doctor ID with details
        public DataTable GetAppointmentsByDoctor(int doctorId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        a.AppointmentID, 
                        a.AppointmentDate, 
                        a.Notes, 
                        d.FullName as DoctorName, 
                        d.Specialty, 
                        p.FullName as PatientName, 
                        p.Email as PatientEmail, 
                        a.DoctorID, 
                        a.PatientID 
                    FROM Appointments a
                    JOIN Doctors d ON a.DoctorID = d.DoctorID
                    JOIN Patients p ON a.PatientID = p.PatientID
                    WHERE a.DoctorID = @DoctorID
                    ORDER BY a.AppointmentDate DESC";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        // Convert AppointmentDate from string to DateTime
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["AppointmentDate"] != DBNull.Value)
                            {
                                string dateStr = row["AppointmentDate"].ToString();
                                if (DateTime.TryParse(dateStr, out DateTime date))
                                {
                                    row["AppointmentDate"] = date;
                                }
                            }
                        }
                    }
                }
            }
            
            return dataTable;
        }

        // Get appointments by patient ID
        public DataTable GetAppointmentsByPatient(int patientId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        a.AppointmentID, 
                        a.AppointmentDate, 
                        a.Notes, 
                        d.FullName as DoctorName, 
                        d.Specialty, 
                        p.FullName as PatientName, 
                        p.Email as PatientEmail, 
                        a.DoctorID, 
                        a.PatientID 
                    FROM Appointments a
                    JOIN Doctors d ON a.DoctorID = d.DoctorID
                    JOIN Patients p ON a.PatientID = p.PatientID
                    WHERE a.PatientID = @PatientID
                    ORDER BY a.AppointmentDate DESC";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        // Convert AppointmentDate from string to DateTime
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row["AppointmentDate"] != DBNull.Value)
                            {
                                string dateStr = row["AppointmentDate"].ToString();
                                if (DateTime.TryParse(dateStr, out DateTime date))
                                {
                                    row["AppointmentDate"] = date;
                                }
                            }
                        }
                    }
                }
            }
            
            return dataTable;
        }

        // Get appointment by ID
        public DataRow GetAppointmentById(int appointmentId)
        {
            DataTable dataTable = new DataTable();
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        a.AppointmentID, 
                        a.AppointmentDate, 
                        a.Notes, 
                        d.FullName as DoctorName, 
                        d.Specialty, 
                        p.FullName as PatientName, 
                        p.Email as PatientEmail, 
                        a.DoctorID, 
                        a.PatientID 
                    FROM Appointments a
                    JOIN Doctors d ON a.DoctorID = d.DoctorID
                    JOIN Patients p ON a.PatientID = p.PatientID
                    WHERE a.AppointmentID = @AppointmentID";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            // Convert AppointmentDate from string to DateTime
                            if (dataTable.Rows[0]["AppointmentDate"] != DBNull.Value)
                            {
                                string dateStr = dataTable.Rows[0]["AppointmentDate"].ToString();
                                if (DateTime.TryParse(dateStr, out DateTime date))
                                {
                                    dataTable.Rows[0]["AppointmentDate"] = date;
                                }
                            }
                            
                            return dataTable.Rows[0];
                        }
                    }
                }
            }
            
            return null;
        }

        // Update appointment
        public bool UpdateAppointment(int appointmentId, int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update existing appointment
                        string query = @"UPDATE Appointments SET 
                                       DoctorID = @DoctorID, 
                                       PatientID = @PatientID, 
                                       AppointmentDate = @AppointmentDate, 
                                       Notes = @Notes 
                                       WHERE AppointmentID = @AppointmentID";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                            command.Parameters.AddWithValue("@DoctorID", doctorId);
                            command.Parameters.AddWithValue("@PatientID", patientId);
                            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Notes", notes ?? "");
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        // Delete appointment
        public bool DeleteAppointment(int appointmentId)
        {
            bool success = false;
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete the appointment
                        string query = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                        
                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                            
                            int rowsAffected = command.ExecuteNonQuery();
                            success = rowsAffected > 0;
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return success;
        }

        #endregion
    }
}
