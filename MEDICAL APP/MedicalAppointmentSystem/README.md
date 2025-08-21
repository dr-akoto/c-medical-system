# Medical Appointment System

A Windows Forms application for managing medical appointments between doctors and patients.

## Features

- View list of available doctors
- Book new appointments
- Manage existing appointments
- Filter appointments by date
- Patient registration
- Manage doctors (add, edit, delete)
- Manage patients (add, edit, delete)

## System Requirements

- Windows 7 or higher
- .NET Framework 4.7.2 or higher
- No database required (uses local XML storage)

## Setup Instructions

### Application Setup

1. Open the solution in Visual Studio
2. Build the solution
3. Run the application

The application uses local XML storage, so no database setup is required. Sample data will be automatically generated on first run.

## Usage Guide

### Main Form

The main form serves as the landing page with navigation buttons to other forms:
- View Doctors - Shows the list of available doctors
- Book Appointment - Opens the appointment booking form
- Manage Appointments - Allows viewing and editing existing appointments
- Manage Doctors - Add, edit, or delete doctors in the system
- Manage Patients - Add, edit, or delete patients in the system

### Doctor List Form

Displays a list of all doctors in the system with their specialization and contact information.

### Appointment Form

Allows patients to:
- Register as a new patient if not already in the system
- Select a doctor from the dropdown
- Choose appointment date and time
- Submit the appointment request

### Manage Appointments Form

Provides functionality to:
- View all appointments
- Filter appointments by date
- Edit appointment details
- Cancel existing appointments

## Project Structure

- **Data/**: Contains database access logic
  - `DatabaseHelper.cs`: Central class for all database operations
- **Forms/**: Contains all application forms
  - `MainForm.cs`: Landing page with navigation
  - `DoctorListForm.cs`: Displays doctors
  - `AppointmentForm.cs`: For booking appointments
  - `ManageAppointmentsForm.cs`: For managing existing appointments
  - `DoctorManagementForm.cs`: For adding, editing, and deleting doctors
  - `PatientManagementForm.cs`: For adding, editing, and deleting patients

## Technical Details

- Language: C#
- UI Framework: Windows Forms
- Data Storage: Local XML files in the application's data directory
- Architecture: 3-tier (Presentation, Business Logic, Data Access)

## Data Storage

The application uses XML files to store data locally:
- `doctors.xml`: Contains information about doctors
- `patients.xml`: Contains information about patients
- `appointments.xml`: Contains information about appointments

These files are automatically created in the application's data folder when the application is first run. Sample data is generated automatically.
