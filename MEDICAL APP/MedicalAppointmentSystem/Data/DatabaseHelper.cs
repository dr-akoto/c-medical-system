using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace MedicalAppointmentSystem.Data
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            // Get connection string from App.config
            connectionString = ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString;
        }

        #region Doctor Methods

        // Get all doctors
        public DataTable GetAllDoctors()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DoctorID, FullName, Specialty, Availability FROM Doctors";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting doctors: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get available doctors
        public DataTable GetAvailableDoctors()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DoctorID, FullName, Specialty FROM Doctors WHERE Availability = 1";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting available doctors: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get doctor by ID
        public DataRow GetDoctorById(int doctorId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DoctorID, FullName, Specialty, Availability FROM Doctors WHERE DoctorID = @DoctorID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", doctorId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting doctor by ID: {ex.Message}");
                    throw;
                }
            }

            return null;
        }

        // Update doctor availability
        public bool UpdateDoctorAvailability(int doctorId, bool isAvailable)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Doctors SET Availability = @Availability WHERE DoctorID = @DoctorID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Availability", isAvailable);
                command.Parameters.AddWithValue("@DoctorID", doctorId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error updating doctor availability: {ex.Message}");
                    throw;
                }
            }
        }

        #endregion

        #region Patient Methods

        // Get all patients
        public DataTable GetAllPatients()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PatientID, FullName, Email FROM Patients";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting patients: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get patient by ID
        public DataRow GetPatientById(int patientId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PatientID, FullName, Email FROM Patients WHERE PatientID = @PatientID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", patientId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting patient by ID: {ex.Message}");
                    throw;
                }
            }

            return null;
        }

        #endregion

        #region Appointment Methods

        // Create a new appointment
        public int CreateAppointment(int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            int appointmentId = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) 
                                VALUES (@DoctorID, @PatientID, @AppointmentDate, @Notes);
                                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                command.Parameters.AddWithValue("@PatientID", patientId);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@Notes", notes ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        appointmentId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error creating appointment: {ex.Message}");
                    throw;
                }
            }

            return appointmentId;
        }

        // Get all appointments
        public DataTable GetAllAppointments()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.AppointmentID, a.AppointmentDate, a.Notes, 
                                d.FullName AS DoctorName, d.Specialty, 
                                p.FullName AS PatientName, p.Email AS PatientEmail,
                                a.DoctorID, a.PatientID
                                FROM Appointments a
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                ORDER BY a.AppointmentDate DESC";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting appointments: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get appointments by doctor ID
        public DataTable GetAppointmentsByDoctor(int doctorId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.AppointmentID, a.AppointmentDate, a.Notes, 
                                d.FullName AS DoctorName, d.Specialty, 
                                p.FullName AS PatientName, p.Email AS PatientEmail,
                                a.DoctorID, a.PatientID
                                FROM Appointments a
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                WHERE a.DoctorID = @DoctorID
                                ORDER BY a.AppointmentDate DESC";

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
                    Console.WriteLine($"Error getting appointments by doctor: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get appointments by patient ID
        public DataTable GetAppointmentsByPatient(int patientId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.AppointmentID, a.AppointmentDate, a.Notes, 
                                d.FullName AS DoctorName, d.Specialty, 
                                p.FullName AS PatientName, p.Email AS PatientEmail,
                                a.DoctorID, a.PatientID
                                FROM Appointments a
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                WHERE a.PatientID = @PatientID
                                ORDER BY a.AppointmentDate DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", patientId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting appointments by patient: {ex.Message}");
                    throw;
                }
            }

            return dataTable;
        }

        // Get appointment by ID
        public DataRow GetAppointmentById(int appointmentId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.AppointmentID, a.AppointmentDate, a.Notes, 
                                d.FullName AS DoctorName, d.Specialty, 
                                p.FullName AS PatientName, p.Email AS PatientEmail,
                                a.DoctorID, a.PatientID
                                FROM Appointments a
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                WHERE a.AppointmentID = @AppointmentID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0];
                    }
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error getting appointment by ID: {ex.Message}");
                    throw;
                }
            }

            return null;
        }

        // Update appointment
        public bool UpdateAppointment(int appointmentId, int doctorId, int patientId, DateTime appointmentDate, string notes)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Appointments 
                                SET DoctorID = @DoctorID, 
                                    PatientID = @PatientID, 
                                    AppointmentDate = @AppointmentDate, 
                                    Notes = @Notes
                                WHERE AppointmentID = @AppointmentID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                command.Parameters.AddWithValue("@PatientID", patientId);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@Notes", notes ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error updating appointment: {ex.Message}");
                    throw;
                }
            }
        }

        // Delete appointment
        public bool DeleteAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Log exception
                    Console.WriteLine($"Error deleting appointment: {ex.Message}");
                    throw;
                }
            }
        }

        #endregion
    }
}
