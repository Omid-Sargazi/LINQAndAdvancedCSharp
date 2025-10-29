CREATE TABLE Category(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    ParentId INT null,
    FOREIGN KEY(ParentId) REFERENCES Category(Id)
);