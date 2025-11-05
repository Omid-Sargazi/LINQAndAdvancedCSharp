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

-- CREATE TABLE ProductImage(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     ProductId INT NOT NULL,
--     ImageUrl NVARCHAR(255) NOT NULL,
--     IsMain BIT DEFAULT 0,
--     FOREIGN KEY(ProductId) REFERENCES Product(Id)
-- );

-- CREATE TABLE Attribute(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     CategoryId INT NOT NULL,
--     Name NVARCHAR(100) NOT NULL,
--     DataType NVARCHAR(50) CHECK(DataType IN('text','number','boolean','date')),
--     FOREIGN KEY (CategoryId) REFERENCES Category(Id), 
-- );

-- CREATE TABLE ProductAttribute(
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     ProductId INT NOT NULL,
--     AttributeId INT NOT NULL,
--     Value NVARCHAR(255) NOT NULL,
--     FOREIGN KEY (ProductId) REFERENCES Product(Id),
--     FOREIGN KEY (AttributeId) REFERENCES Attribute(Id)
-- );

-- CREATE TABLE Customer (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     FullName NVARCHAR(100),
--     Email NVARCHAR(150) UNIQUE,
--     PasswordHash NVARCHAR(255),
--     Phone NVARCHAR(20),
--     CreatedAt DATETIME DEFAULT GETDATE()
-- );
-- GO


-- CREATE TABLE CustomerAddress (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     CustomerId INT NOT NULL,
--     AddressLine NVARCHAR(255),
--     City NVARCHAR(100),
--     Province NVARCHAR(100),
--     PostalCode NVARCHAR(20),
--     IsDefault BIT DEFAULT 0,
--     FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
-- );
-- GO


-- CREATE TABLE Cart (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     CustomerId INT UNIQUE,
--     CreatedAt DATETIME DEFAULT GETDATE(),
--     FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
-- );
-- GO

-- CREATE TABLE CartItem (
--     Id INT IDENTITY(1,1) PRIMARY KEY,
--     CartId INT NOT NULL,
--     ProductId INT NOT NULL,
--     Quantity INT CHECK (Quantity > 0),
--     AddedAt DATETIME DEFAULT GETDATE(),
--     FOREIGN KEY (CartId) REFERENCES Cart(Id),
--     FOREIGN KEY (ProductId) REFERENCES Product(Id)
-- );
-- GO
