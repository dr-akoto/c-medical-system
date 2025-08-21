# Medical Appointment System

A Windows Forms application for managing medical appointments between doctors and patients.

## Features

- View list of available doctors
- Book new appointments
- Manage existing appointments
- Filter appointments by date
- Patient registration

## System Requirements

- Windows 7 or higher
- .NET Framework 4.7.2 or higher
- SQL Server (Express or higher)

## Setup Instructions

### Database Setup

1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Execute the `DatabaseScript.sql` file to create the database and populate it with sample data

### Application Setup

1. Open the solution in Visual Studio
2. Ensure the connection string in `App.config` is configured correctly for your SQL Server instance
3. Build the solution
4. Run the application

## Usage Guide

### Main Form

The main form serves as the landing page with navigation buttons to other forms:
- View Doctors - Shows the list of available doctors
- Book Appointment - Opens the appointment booking form
- Manage Appointments - Allows viewing and editing existing appointments

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

## Technical Details

- Language: C#
- UI Framework: Windows Forms
- Database Access: ADO.NET with SQL Server
- Architecture: 3-tier (Presentation, Business Logic, Data Access)
