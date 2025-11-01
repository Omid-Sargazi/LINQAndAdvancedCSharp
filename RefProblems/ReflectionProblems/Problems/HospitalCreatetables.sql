-- -- CREATE TABLE Hospital(
-- --     Id INT IDENTITY(1,1) PRIMARY KEY,
-- --     Name NVARCHAR(150) NOT NULL,
-- --     Address NVARCHAR(255),
-- --     City NVARCHAR(100),
-- --     TotalFloors INT,
-- --     Phone NVARCHAR(30)
-- -- );

-- CREATE TABLE Department(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     HospitalId int NOT NULL,
--     Name NVARCHAR(100) NOT NULL,
--     FloorNumber INT,
--     FOREIGN KEY (HospitalId) REFERENCES Hospital(Id)
-- );

-- CREATE TABLE Room(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     DepartmentId INT NOT NULL,
--     RoomNumber NVARCHAR(20) NOT NULL,
--     Capacity INT DEFAULT 1,
--     Status NVARCHAR(20) DEFAULT 'Available',
--     FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
-- );

-- CREATE TABLE Doctor(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     DepartmentId INT NOT NULL,
--     FullName NVARCHAR(100)  NOT NULL,
--     Specialty NVARCHAR(100),
--     RoomNumber NVARCHAR(20),
--     Phone NVARCHAR(20),
--     Email NVARCHAR(100),
--     FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
-- );

-- CREATE TABLE Nurse (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     DepartmentId INT NOT NULL,
--     FullName NVARCHAR(100) NOT NULL,
--     Shift NVARCHAR(20),
--     Phone NVARCHAR(20),
--     FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
-- );

-- CREATE TABLE Patient (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     HospitalId INT NOT NULL,
--     FullName NVARCHAR(100) NOT NULL,
--     NationalId NVARCHAR(20),
--     BirthDate DATE,
--     Gender CHAR(1),
--     Phone NVARCHAR(20),
--     Address NVARCHAR(255),
--     BloodType NVARCHAR(5),
--     FOREIGN KEY (HospitalId) REFERENCES Hospital(Id)
-- );

-- CREATE TABLE Patient(
--      Id INT IDENTITY(1,1) PRIMARY KEY,
--     HospitalId INT NOT NULL,
--     FullName NVARCHAR(100) NOT NULL,
--     NationalId NVARCHAR(20),
--     BirthDate DATE,
--     Gender CHAR(1),
--     Phone NVARCHAR(20),
--     Address NVARCHAR(255),
--     BloodType NVARCHAR(5),
--     FOREIGN KEY (HospitalId) REFERENCES Hospital(Id)
-- );

-- Create TABLE PatientDoctor(
--     PatientId INT NOT NULL,
--     DoctorId INT NOT NULL,
--     StartDate DATE,
--     EndDate DATE null,
--     PRIMARY KEY(PatientId,DoctorId),
--     FOREIGN KEY(PatientId) REFERENCES Patient(Id),
--      FOREIGN KEY (DoctorId) REFERENCES Doctor(Id)

-- );

-- CREATE TABLE PatientNurse (
--     PatientId INT NOT NULL,
--     NurseId INT NOT NULL,
--     AssignedDate DATE,
--     PRIMARY KEY (PatientId, NurseId),
--     FOREIGN KEY (PatientId) REFERENCES Patient(Id),
--     FOREIGN KEY (NurseId) REFERENCES Nurse(Id)
-- );


-- CREATE TABLE Appointment(
--     Id INT IDENTITY(2,1) PRIMARY KEY,
--     PatientId INT NOT NULL,
--     DoctorId INT NOT NULL,
--     AppointmentDate DATETIME NOT NULL,
--     Status NVARCHAR(20) DEFAULT 'Scheduled',
--     Notes NVARCHAR(250),
--     FOREIGN KEY(PatientId) REFERENCES Patient(Id),
--     FOREIGN KEY(DoctorId) REFERENCES Doctor(Id),
-- );

-- CREATE TABLE Admission(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     PatientId INT NOT NULL,
--     RoomId int NOT NULL, 
--     DoctorId int NOT NULL,
--     AdmissionDate DATETIME NOT NULL DEFAULT GETDATE(),
--     DischargeDate DATETIME NULL,
--     Status NVARCHAR(20) DEFAULT 'Active',
--     Notes NVARCHAR(255),
--     FOREIGN KEY (PatientId) REFERENCES Patient(Id),
--     FOREIGN KEY (RoomId) REFERENCES Room(Id),
--     FOREIGN KEY (DoctorId) REFERENCES Doctor(Id)
-- );

-- CREATE TABLE TreatmentInstruction (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     PatientId INT NOT NULL,
--     DoctorId INT NOT NULL,
--     InstructionType NVARCHAR(50),
--     Description NVARCHAR(255),
--     DateIssued DATETIME DEFAULT GETDATE(),
--     FOREIGN KEY (PatientId) REFERENCES Patient(Id),
--     FOREIGN KEY (DoctorId) REFERENCES Doctor(Id)
-- );

-- CREATE TABLE Insurance (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     PatientId INT NOT NULL,
--     CompanyName NVARCHAR(100),
--     PolicyNumber NVARCHAR(50),
--     CoveragePercent DECIMAL(5,2),
--     ExpiryDate DATE,
--     FOREIGN KEY (PatientId) REFERENCES Patient(Id)
-- );

-- CREATE TABLE Invoice (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--      PatientId INT NOT NULL,
--     AdmissionId INT NOT NULL,
--     InvoiceDate DATETIME DEFAULT GETDATE(),
--     TotalAmount DECIMAL(10,2) NOT NULL,
--     TaxAmount DECIMAL(10,2) DEFAULT 0,
--     InsuranceCoverage DECIMAL(10,2) DEFAULT 0,
--     PayableAmount AS (TotalAmount + TaxAmount - InsuranceCoverage) PERSISTED,
--     Status NVARCHAR(20) DEFAULT 'Pending', -- Paid, Pending, Cancelled
--     FOREIGN KEY (PatientId) REFERENCES Patient(Id),
--     FOREIGN KEY (AdmissionId) REFERENCES Admission(Id)
-- );

-- CREATE TABLE Payment (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     InvoiceId INT NOT NULL,
--     PaymentDate DATETIME DEFAULT GETDATE(),
--     AmountPaid DECIMAL(10,2) NOT NULL,
--     PaymentMethod NVARCHAR(50), -- Cash, Card, Transfer
--     ReferenceNo NVARCHAR(50),
--     FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id)
-- );

-- بیمارستان و بخش‌ها
INSERT INTO Hospital (Name, Address, City, TotalFloors, Phone)
VALUES ('Omid Hospital', 'Tehran - Vanak Sq.', 'Tehran', 6, '021-22222222');

-- INSERT INTO Department (HospitalId, Name, FloorNumber)
-- VALUES (1, 'Cardiology', 2), (1, 'Neurology', 3), (1, 'Emergency', 1);

-- -- پزشکان
-- INSERT INTO Doctor (DepartmentId, FullName, Specialty, RoomNumber, Phone)
-- VALUES 
-- (1, 'Dr. Sara Rahimi', 'Cardiology', '201', '0912123456'),
-- (2, 'Dr. Ali Moradi', 'Neurology', '301', '0912123000');

-- -- پرستاران
-- INSERT INTO Nurse (DepartmentId, FullName, Shift)
-- VALUES 
-- (1, 'Nurse Mina Ramezani', 'Morning'),
-- (2, 'Nurse Maryam Hosseini', 'Night');

-- -- بیماران
-- INSERT INTO Patient (HospitalId, FullName, NationalId, BirthDate, Gender, Phone)
-- VALUES 
-- (1, 'Omid Sargazi', '1234567890', '1983-07-18', 'M', '0912000000'),
-- (1, 'Sara Ahmadi', '9876543210', '1990-02-25', 'F', '0935000000');

-- -- اتاق‌ها
-- INSERT INTO Room (DepartmentId, RoomNumber, Capacity)
-- VALUES 
-- (1, 'C201', 2),
-- (2, 'N301', 1);





