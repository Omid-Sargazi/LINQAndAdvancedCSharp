-- CREATE TABLE Category(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100),
--     ParentId INT null,
--     FOREIGN KEY(ParentId) REFERENCES Category(Id)
-- );

CREATE TABLE Member
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Phone NVARCHAR(50),
    JoinDate DATETIME DEFAULT GETDATE(),
);