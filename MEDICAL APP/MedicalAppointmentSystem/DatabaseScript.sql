-- SQL Script for Medical Appointment Booking System

-- Create Database
CREATE DATABASE MedicalDB;
GO

USE MedicalDB;
GO

-- Create Doctors Table
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    FullName VARCHAR(100) NOT NULL,
    Specialty VARCHAR(100) NOT NULL,
    Availability BIT NOT NULL DEFAULT 1
);
GO

-- Create Patients Table
CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);
GO

-- Create Appointments Table
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    DoctorID INT NOT NULL,
    PatientID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Notes VARCHAR(500),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);
GO

-- Insert Sample Data for Doctors
INSERT INTO Doctors (FullName, Specialty, Availability)
VALUES 
    ('Dr. John Smith', 'Cardiology', 1),
    ('Dr. Sarah Johnson', 'Pediatrics', 1),
    ('Dr. Michael Brown', 'Orthopedics', 1),
    ('Dr. Emily Davis', 'Dermatology', 1),
    ('Dr. Robert Wilson', 'Neurology', 0);
GO

-- Insert Sample Data for Patients
INSERT INTO Patients (FullName, Email)
VALUES 
    ('James Anderson', 'james.anderson@email.com'),
    ('Emma Thompson', 'emma.t@email.com'),
    ('Daniel Mitchell', 'dan.mitchell@email.com'),
    ('Olivia Parker', 'olivia.p@email.com'),
    ('William Scott', 'will.scott@email.com');
GO
