
-- =========================================
-- 🎬 Cinema Management System - FULL SCHEMA
-- =========================================

-- TẠO DATABASE
CREATE DATABASE CinemaSystem;
GO

USE CinemaSystem;
GO

-- ================================
-- 1. ROLE
-- ================================
CREATE TABLE Role (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleDesc NVARCHAR(50) UNIQUE NOT NULL CHECK (RoleDesc IN ('admin', 'manager', 'staff', 'customer'))
);
GO

-- ================================
-- 2. USER
-- ================================
CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female')),
    VerificationCode NVARCHAR(100),
    AccStatus BIT DEFAULT 0,
    RoleId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    DateOfBirth DATE,
    Address NVARCHAR(255),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);
GO

-- ================================
-- 3. CATEGORY
-- ================================
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryDesc NVARCHAR(100) NOT NULL
);
GO

-- ================================
-- 4. GENRE
-- ================================
CREATE TABLE Genre (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    GenreDesc NVARCHAR(100) NOT NULL
);
GO

-- ================================
-- 5. MOVIE
-- ================================
CREATE TABLE Movie (
    MovieId INT IDENTITY(1,1) PRIMARY KEY,
    MovieName NVARCHAR(255) NOT NULL,
    MovieDesc NVARCHAR(MAX),
    SuggestedPrice DECIMAL(10,2),
    OriginalPrice DECIMAL(10,2),
    MovieImg NVARCHAR(255),
    MovieStatus NVARCHAR(50) NOT NULL DEFAULT 'Archived' CHECK (MovieStatus IN ('Active', 'Inactive', 'Archived')),
    Trailer NVARCHAR(255),
    Rating FLOAT CHECK (Rating BETWEEN 0 AND 10),
    Nation NVARCHAR(100),
    IsSub BIT DEFAULT 0,
    Duration INT NOT NULL,
    CategoryId INT NOT NULL,
    ReleaseDate DATE,
    Language NVARCHAR(50),
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

-- ================================
-- 6. MOVIE-GENRE
-- ================================
CREATE TABLE MovieGenre (
    MovieId INT NOT NULL,
    GenreId INT NOT NULL,
    PRIMARY KEY (MovieId, GenreId),
    FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
    FOREIGN KEY (GenreId) REFERENCES Genre(GenreId)
);
GO

-- ================================
-- 7. ROOM
-- ================================
CREATE TABLE Room (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
    Capacity INT NOT NULL,
    RoomName NVARCHAR(100) NOT NULL,
    RoomType NVARCHAR(50) CHECK (RoomType IN ('2D', '3D', 'IMAX', 'VIP'))
);
GO

-- ================================
-- 8. SEAT
-- ================================
CREATE TABLE Seat (
    SeatId INT IDENTITY(1,1) PRIMARY KEY,
    RoomId INT NOT NULL,
    RowNo INT NOT NULL,
    ColNo INT NOT NULL,
    SeatType NVARCHAR(50) DEFAULT 'Single' CHECK (SeatType IN ('Single', 'Couple', 'VIP')),
    SeatStatus NVARCHAR(50) DEFAULT 'Available' CHECK (SeatStatus IN ('Available', 'Maintenance', 'Blocked')),
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId)
);
GO

-- ================================
-- 9. SCHEDULE (UPDATED)
-- ================================
CREATE TABLE Schedule (
    ScheduleId INT IDENTITY(1,1) PRIMARY KEY,
    StartDatetime DATETIME NOT NULL,
    EndDatetime DATETIME NOT NULL,
    Is3D BIT DEFAULT 0,
    IsSubtitle BIT DEFAULT 0,
    MovieId INT NOT NULL,
    RoomId INT NOT NULL,
    CHECK (EndDatetime > StartDatetime),
    FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId)
);
GO

-- ================================
-- 10. VOUCHER
-- ================================
CREATE TABLE Voucher (
    VoucherId INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) UNIQUE NOT NULL,
    VoucherDesc NVARCHAR(255),
    DiscountPercent DECIMAL(5,2) CHECK (DiscountPercent BETWEEN 0 AND 100),
    ExpiredDate DATE NOT NULL,
    IsActive BIT DEFAULT 1,
    MinPurchaseAmount DECIMAL(10,2) DEFAULT 0,
    Quantity INT DEFAULT 0
);
GO

-- ================================
-- 11. BILL
-- ================================
CREATE TABLE Bill (
    BillId INT IDENTITY(1,1) PRIMARY KEY,
    BillStatus NVARCHAR(50) DEFAULT 'Pending' CHECK (BillStatus IN ('Pending', 'Paid', 'Refunded', 'Cancelled')),
    TotalCost DECIMAL(10,2) NOT NULL,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    FinalCost AS (TotalCost - DiscountAmount),
    BillDateTime DATETIME DEFAULT GETDATE(),
    BillType NVARCHAR(50) CHECK (BillType IN ('Online Payment', 'Cash', 'Gift Card', 'Voucher Redemption')),
    PaymentMethod NVARCHAR(50),
    IsPaid BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UserId INT NOT NULL,
    VoucherId INT,
    FOREIGN KEY (UserId) REFERENCES [User](UserID),
    FOREIGN KEY (VoucherId) REFERENCES Voucher(VoucherId)
);
GO

-- ================================
-- 12. ITEM
-- ================================
CREATE TABLE Item (
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    ItemDesc NVARCHAR(255),
    SuggestedPrice DECIMAL(10,2),
    OriginalPrice DECIMAL(10,2),
    Type NVARCHAR(50),
    ItemCategory NVARCHAR(50) CHECK (ItemCategory IN ('Food', 'Drink', 'Service')),
    Amount INT
);
GO

-- ================================
-- 13. SERVICE (UPDATED)
-- ================================
CREATE TABLE Service (
    ServiceId INT IDENTITY(1,1) PRIMARY KEY,
    ServiceDesc NVARCHAR(255) NOT NULL,
    CreatedBy INT NOT NULL,
    ApprovedBy INT NULL,
    IsApproved BIT DEFAULT 0,
    Note NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES [User](UserID),
    FOREIGN KEY (ApprovedBy) REFERENCES [User](UserID)
);
GO

-- ================================
-- 14. TICKET (UPDATED)
-- ================================
CREATE TABLE Ticket (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    TicketType NVARCHAR(50) CHECK (TicketType IN ('Standard', 'VIP', 'Student Discount')),
    TicketStatus NVARCHAR(50) DEFAULT 'Active' CHECK (TicketStatus IN ('Active', 'Cancelled', 'Used')),
    Noting NVARCHAR(255),
    TicketDateTime DATETIME DEFAULT GETDATE(),
    TicketPrice DECIMAL(10,2) NOT NULL,
    SeatId INT NOT NULL,
    ScheduleId INT NOT NULL,
    BillId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (SeatId) REFERENCES Seat(SeatId),
    FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    FOREIGN KEY (BillId) REFERENCES Bill(BillId),
    FOREIGN KEY (UserId) REFERENCES [User](UserID)
);
GO

-- ================================
-- 15. TICKET ADDON
-- ================================
CREATE TABLE TicketAddon (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TicketId INT NOT NULL,
    ServiceId INT NOT NULL,
    ItemId INT NOT NULL,
    Quantity INT DEFAULT 1,
    FOREIGN KEY (TicketId) REFERENCES Ticket(TicketId),
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId),
    FOREIGN KEY (ItemId) REFERENCES Item(ItemId)
);
GO

-- ================================
-- 16. AUDIT LOG
-- ================================
CREATE TABLE AuditLog (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    Action NVARCHAR(100),
    TableName NVARCHAR(100),
    RecordId INT,
    UserId INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(MAX),
    FOREIGN KEY (UserId) REFERENCES [User](UserID)
);
GO

-- ================================
-- ADDITIONAL TABLES
-- ================================
CREATE TABLE Notification (
    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Message NVARCHAR(MAX),
    IsRead BIT DEFAULT 0,
    SentAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [User](UserID)
);
GO

CREATE TABLE TransactionLog (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    BillId INT NOT NULL,
    PaymentGateway NVARCHAR(100),
    TransactionCode NVARCHAR(100),
    Amount DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(50) CHECK (Status IN ('Pending', 'Success', 'Failed', 'Refunded')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BillId) REFERENCES Bill(BillId)
);
GO

CREATE TABLE Inventory (
    InventoryId INT IDENTITY(1,1) PRIMARY KEY,
    ItemId INT NOT NULL,
    LastUpdated DATETIME DEFAULT GETDATE(),
    CurrentStock INT NOT NULL,
    MinStockLevel INT DEFAULT 10,
    FOREIGN KEY (ItemId) REFERENCES Item(ItemId)
);
GO

CREATE TABLE MovieDetail (
    MovieDetailId INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT NOT NULL,
    Director NVARCHAR(100),
    Actors NVARCHAR(500),
    AgeLimit NVARCHAR(10),
    Language NVARCHAR(50),
    FOREIGN KEY (MovieId) REFERENCES Movie(MovieId)
);
GO

CREATE TABLE ScheduleSeatTypePrice (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ScheduleId INT NOT NULL,
    SeatType NVARCHAR(50) CHECK (SeatType IN ('Single', 'Couple', 'VIP')),
    Price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId)
);
GO

-- ================================
-- INSERT SEED DATA
-- ================================
INSERT INTO Role (RoleDesc) VALUES
('admin'), ('manager'), ('staff'), ('customer');

INSERT INTO Room (Capacity, RoomName, RoomType) VALUES
(80, 'Room A1', '2D'),
(100, 'Room B1', '3D'),
(50, 'Room VIP1', 'VIP'),
(120, 'Room C1', 'IMAX');

INSERT INTO Item (ItemDesc, SuggestedPrice, OriginalPrice, Type, ItemCategory, Amount)
VALUES 
('Popcorn', 35.00, 30.00, 'Snack', 'Food', 100),
('Coke', 25.00, 20.00, 'Beverage', 'Drink', 150),
('Blanket', 50.00, 40.00, 'Comfort', 'Service', 20);

INSERT INTO Voucher (Code, VoucherDesc, DiscountPercent, ExpiredDate, Quantity)
VALUES 
('DISCOUNT10', '10% off all tickets', 10.00, '2025-12-31', 100),
('VIPONLY', '20% VIP customer discount', 20.00, '2025-10-01', 50);

INSERT INTO Category (CategoryDesc) VALUES
('Now Showing'), ('Kid Special'), ('Comming Soon'), ('Special Holidays'), ('Adult Only');

INSERT INTO Genre (GenreDesc) VALUES
('Action'), ('Comedy'), ('Horror'), ('Romance'), ('Sci-Fi'), ('Animation'),
('Drama'), ('Adventure'), ('Mystery'), ('Fantasy');




-- USE master;
-- GO

-- -- Force disconnect all users
-- ALTER DATABASE CinemaSystem SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- DROP DATABASE CinemaSystem;
