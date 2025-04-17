-- =========================================
-- 🎬 Cinema System - Slim with Detail + Seed
-- =========================================

CREATE DATABASE CinemaSystemMini;
GO
USE CinemaSystemMini;
GO

-- 1. ROLE
CREATE TABLE Role (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleDesc NVARCHAR(50) UNIQUE NOT NULL CHECK (RoleDesc IN ('admin', 'manager', 'staff', 'customer'))
);
GO

-- 2. USER
CREATE TABLE [User] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    RoleId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);
GO

-- 3. CATEGORY
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryDesc NVARCHAR(100) NOT NULL
);
GO

-- 4. GENRE
CREATE TABLE Genre (
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    GenreDesc NVARCHAR(100) NOT NULL
);
GO

-- 5. MOVIE
CREATE TABLE Movie (
    MovieId INT IDENTITY(1,1) PRIMARY KEY,
    MovieName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Duration INT NOT NULL,
    ReleaseDate DATE,
    Status NVARCHAR(50) DEFAULT 'Active' CHECK (Status IN ('Active', 'Archived')),
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

-- 6. MOVIE DETAIL
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

-- 7. MOVIE GENRE
CREATE TABLE MovieGenre (
    MovieId INT NOT NULL,
    GenreId INT NOT NULL,
    PRIMARY KEY (MovieId, GenreId),
    FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
    FOREIGN KEY (GenreId) REFERENCES Genre(GenreId)
);
GO

-- 8. ROOM
CREATE TABLE Room (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(100) NOT NULL,
    Capacity INT NOT NULL
);
GO

-- 9. SEAT
CREATE TABLE Seat (
    SeatId INT IDENTITY(1,1) PRIMARY KEY,
    RoomId INT NOT NULL,
    RowNo INT NOT NULL,
    ColNo INT NOT NULL,
    SeatType NVARCHAR(50) DEFAULT 'Single' CHECK (SeatType IN ('Single', 'Couple', 'VIP')),
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId)
);
GO

-- 10. SCHEDULE
CREATE TABLE Schedule (
    ScheduleId INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT NOT NULL,
    RoomId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
    FOREIGN KEY (RoomId) REFERENCES Room(RoomId),
    CHECK (EndTime > StartTime)
);
GO

-- 11. BILL
CREATE TABLE Bill (
    BillId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
GO

-- 12. TICKET
CREATE TABLE Ticket (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    ScheduleId INT NOT NULL,
    SeatId INT NOT NULL,
    BillId INT NOT NULL,
    TicketPrice DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Active' CHECK (Status IN ('Active', 'Cancelled')),
    FOREIGN KEY (ScheduleId) REFERENCES Schedule(ScheduleId),
    FOREIGN KEY (SeatId) REFERENCES Seat(SeatId),
    FOREIGN KEY (BillId) REFERENCES Bill(BillId)
);
GO

-- ================================
-- 🌱 INSERT SEED DATA
-- ================================

-- ROLE
INSERT INTO Role (RoleDesc) VALUES 
('admin'), ('manager'), ('staff'), ('customer');

-- CATEGORY
INSERT INTO Category (CategoryDesc) VALUES
('Now Showing'), ('Kid Special'), ('Comming Soon'), 
('Special Holidays'), ('Adult Only');

-- GENRE
INSERT INTO Genre (GenreDesc) VALUES
('Action'), ('Comedy'), ('Horror'), ('Romance'), ('Sci-Fi'), ('Animation'),
('Drama'), ('Adventure'), ('Mystery'), ('Fantasy');
