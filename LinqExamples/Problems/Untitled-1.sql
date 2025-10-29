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

-- CREATE TABLE BookCopy (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     BookId INT NOT NULL,
--     CopyNumber INT NOT NULL,
--     ShelfLocation NVARCHAR(50),
--     Status NVARCHAR(20) DEFAULT 'Available', -- Available, Borrowed, Lost
--     FOREIGN KEY (BookId) REFERENCES Book(Id)
-- );

-- CREATE TABLE Borrow(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     MemberId INT NOT NULL,
--     BookCopyId INT NOT NULL,
--     BorrowDate DATETIME DEFAULT GETDATE(),
--     DueDate DATETIME NOT NULL,
--     ReturnDate DATETIME NULL,
--     FineAmount DECIMAL(10,2) DEFAULT 0,
--     FOREIGN KEY (MemberId) REFERENCES Member(Id),
--     FOREIGN KEY (BookCopyId) REFERENCES BookCopy(Id)
-- );

-- CREATE TABLE FineTransaction (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     BorrowId INT NOT NULL,
--     MemberId INT NOT NULL,
--     FineAmount DECIMAL(10,2) NOT NULL,
--     PaidAmount DECIMAL(10,2) DEFAULT 0,
--     TaxAmount DECIMAL(10,2) DEFAULT 0,
--     PaymentDate DATETIME NULL,
--     PaymentMethod NVARCHAR(50),
--     Notes NVARCHAR(255),
--     FOREIGN KEY (BorrowId) REFERENCES Borrow(Id),
--     FOREIGN KEY (MemberId) REFERENCES Member(Id)
-- );


-- INSERT INTO Category (Name, ParentId) VALUES 
-- ('Science', NULL),
-- ('Physics', 1),
-- ('Chemistry', 1),
-- ('History', NULL),
-- ('Literature', NULL);

-- INSERT INTO Member(FullName,Email,Phone) VALUES
-- ('Omid Sargazi', 'omid@example.com', '09121234567'),
-- ('Sara Ahmadi', 'sara@example.com', '09351234567');

-- INSERT INTO Book (Title, Author, ISBN, PublishYear, CategoryId) VALUES
-- ('Quantum Mechanics', 'Richard Feynman', '978-0131133654', 2002, 2),
-- ('Organic Chemistry', 'Paula Yurkanis', '978-0321971371', 2014, 3),
-- ('World History', 'Andrew Marr', '978-1447217171', 2012, 4);

-- INSERT INTO BookCopy (BookId, CopyNumber, ShelfLocation) VALUES
-- (1, 1, 'A1'),
-- (1, 2, 'A2'),
-- (2, 1, 'B1'),
-- (3, 1, 'C1');

-- INSERT INTO Borrow (MemberId, BookCopyId, BorrowDate, DueDate)
-- VALUES
-- (1, 1, GETDATE(), DATEADD(DAY, 7, GETDATE())),
-- (2, 3, GETDATE(), DATEADD(DAY, 10, GETDATE()));

-- SELECT m.FullName, bc.CopyNumber,b.BorrowDate,b.DueDate FROM Borrow as b JOIN Member as m on b.MemberId = m.Id JOIN BookCopy as bc on bc.Id = b.BookCopyId 


-- INSERT INTO Member (FullName, Email, Phone) VALUES
-- ('Ali Mohamadi', 'ali@example.com', '09131234567'),
-- ('Zahra karami', 'zahra@example.com', '09361234567'),
-- ('mohammad rezaee', 'mohammad@example.com', '09151234567'),
-- ('fateme Hosseini', 'fatemeh@example.com', '09371234567');
-- INSERT INTO Book (Title, Author, ISBN, PublishYear, CategoryId) VALUES
-- ('Introduction to Algorithms', 'Thomas Cormen', '978-0262033848', 2009, 1),
-- ('The Great Gatsby', 'F. Scott Fitzgerald', '978-0743273565', 1925, 5),
-- ('Database Systems', 'Raghu Ramakrishnan', '978-0072465631', 2002, 1);

-- INSERT INTO BookCopy (BookId, CopyNumber, ShelfLocation, Status) VALUES
-- (4, 1, 'D1', 'Available'),
-- (5, 1, 'E1', 'Available'),
-- (5, 2, 'E2', 'Borrowed'),
-- (6, 1, 'F1', 'Available'),
-- (6, 2, 'F2', 'Lost');

-- اضافه کردن امانت‌های بیشتر با تاریخ‌های مختلف
-- INSERT INTO Borrow (MemberId, BookCopyId, BorrowDate, DueDate, ReturnDate, FineAmount) VALUES
-- (1, 2, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -13, GETDATE()), DATEADD(DAY, -10, GETDATE()), 15000),
-- (3, 4, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, 2, GETDATE()), NULL, 0),
-- (4, 5, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -8, GETDATE()), NULL, 21000);

-- -- اضافه کردن تراکنش‌های جریمه
-- INSERT INTO FineTransaction (BorrowId, MemberId, FineAmount, PaidAmount, TaxAmount, PaymentDate, PaymentMethod) VALUES
-- (3, 1, 15000, 15000, 1500, DATEADD(DAY, -9, GETDATE()), 'Online'),
-- (6, 4, 21000, 10000, 1000, GETDATE(), 'Cash');