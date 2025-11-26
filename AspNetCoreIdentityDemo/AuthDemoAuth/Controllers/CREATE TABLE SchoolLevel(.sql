CREATE TABLE SchoolLevel(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
);

CREATE TABLE AcademicYear(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive BIT DEFAULT 0
);

CREATE TABLE Grade(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SchoolLevelId INT NOT NULL,
    NAMES NVARCHAR(50) NOT NULL,
    FOREIGN KEY (SchoolLevelId) REFERENCES SchoolLevel(Id)
);

CREATE TABLE Classroom(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GradeId INT NOT NULL,
    AcademicYearId INT NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Capacity INT DEFAULT 50,
    FOREIGN KEY (GradeId) REFERENCES Grade(Id),
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(Id),
);


Create TABLE Teacher(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) UNIQUE,
    Phone NVARCHAR(50),
    HireDate DATE,
    IsActive BIT DEFAULT 1
);

CREATE TABLE Subject(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GradeId INT NOT NULL,
    TeacherId INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Credit INT DEFAULT 1,
    FOREIGN KEY(GradeId) REFERENCES Grade(Id),
    FOREIGN KEY(TeacherId) REFERENCES Teacher(Id)
);

Create TABLE SubjectClass(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClassroomId INT NOT NULL,
    SubjectId INT NOT NULL,
    FOREIGN KEY(ClassroomId) REFERENCES Classroom(Id),
    FOREIGN KEY(SubjectId) REFERENCES Subject(Id) 
);

CREATE TABLE Student (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    NationalId NVARCHAR(20) UNIQUE,
    BirthDate DATE,
    Gender CHAR(1) CHECK (Gender IN ('M','F')),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    AdmissionDate DATE DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

CREATE TABLE Parent (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    NationalId NVARCHAR(20),
    Phone NVARCHAR(20),
    Email NVARCHAR(150),
    Relation NVARCHAR(20) CHECK (Relation IN ('Father','Mother','Guardian')),
    Occupation NVARCHAR(100)
);

CREATE TABLE StudentParent (
    StudentId INT NOT NULL,
    ParentId INT NOT NULL,
    PRIMARY KEY (StudentId, ParentId),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (ParentId) REFERENCES Parent(Id)
);

CREATE TABLE Enrollment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassroomId INT NOT NULL,
    SubjectId INT NOT NULL,
    AcademicYearId INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (ClassroomId) REFERENCES Classroom(Id),
    FOREIGN KEY (SubjectId) REFERENCES Subject(Id),
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(Id)
);

CREATE TABLE Attendance (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    SubjectId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    EntryTime TIME NULL,
    ExitTime TIME NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Present','Absent','Late')),
    Remarks NVARCHAR(255),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (SubjectId) REFERENCES Subject(Id)
);


CREATE TABLE Exam (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SubjectId INT NOT NULL,
    ExamType NVARCHAR(50) CHECK (ExamType IN ('MidTerm','Final','Quiz')),
    ExamDate DATE NOT NULL,
    MaxScore DECIMAL(5,2) DEFAULT 20,
    FOREIGN KEY (SubjectId) REFERENCES Subject(Id)
);

CREATE TABLE GradeReport (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ExamId INT NOT NULL,
    StudentId INT NOT NULL,
    Score DECIMAL(5,2) CHECK (Score BETWEEN 0 AND 20),
    Remarks NVARCHAR(255),
    FOREIGN KEY (ExamId) REFERENCES Exam(Id),
    FOREIGN KEY (StudentId) REFERENCES Student(Id)
);


CREATE TABLE FeeStructure (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GradeId INT NOT NULL,
    AcademicYearId INT NOT NULL,
    TotalFee DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (GradeId) REFERENCES Grade(Id),
    FOREIGN KEY (AcademicYearId) REFERENCES AcademicYear(Id)
);

CREATE TABLE Payment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    FeeStructureId INT NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    AmountPaid DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(50) CHECK (PaymentMethod IN ('Cash','Card','Transfer')),
    ReferenceNumber NVARCHAR(100),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (FeeStructureId) REFERENCES FeeStructure(Id)
);




