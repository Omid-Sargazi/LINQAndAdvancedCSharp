-- CREATE TABLE Category (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100) NOT NULL,
--     ParentId INT NULL,
--     FOREIGN KEY (ParentId) REFERENCES Category(Id)
-- );

-- CREATE TABLE Brand(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(100) NOT NULL,
--     Description NVARCHAR(250),
-- );

-- CREATE TABLE Product(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     Name NVARCHAR(200) NOT NULL,
--     Description NVARCHAR(MAX),
--     CategoryId INT NOT NULL,
--     BrandId INT NOT NULL,
--     Price DECIMAL(10,2) NOT NULL,
--     SKU NVARCHAR(50) UNIQUE,
--     IsActive BIT DEFAULT 1,
--     CreatedAt DATETIME DEFAULT GETDATE(),
--     FOREIGN KEY (CategoryId) REFERENCES Category(Id),
--     FOREIGN KEY (BrandId) REFERENCES Brand(Id)
-- );

-- SELECT * from dbo.Brand