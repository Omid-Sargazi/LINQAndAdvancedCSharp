-- CREATE TABLE Category(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100),
--     ParentId INT null,
--     FOREIGN KEY(ParentId) REFERENCES Category(Id)
-- );

-- CREATE TABLE Member
-- (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     FullName NVARCHAR(100) NOT NULL,
--     Email NVARCHAR(100),
--     Phone NVARCHAR(50),
--     JoinDate DATETIME DEFAULT GETDATE(),
-- );

-- CREATE TABLE Book(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Title NVARCHAR(200) NOT NULL,
--     Author NVARCHAR(100),
--     ISBN NVARCHAR(50),
--     PublishYear INT,
--     CategoryId INT null,
--     FOREIGN KEY(CategoryId) REFERENCES Category(Id)
-- );

CREATE TABLE BookCopy (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    CopyNumber INT NOT NULL,
    ShelfLocation NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Available', -- Available, Borrowed, Lost
    FOREIGN KEY (BookId) REFERENCES Book(Id)
);

CREATE TABLE Borrow(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MemberId INT NOT NULL,
    BookCopyId INT NOT NULL,
    BorrowDate DATETIME DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    FineAmount DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (MemberId) REFERENCES Member(Id),
    FOREIGN KEY (BookCopyId) REFERENCES BookCopy(Id)
);