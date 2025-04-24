DROP DATABASE IF EXISTS CPRG211Final;
CREATE DATABASE CPRG211Final;
USE CPRG211Final;


CREATE USER IF NOT EXISTS 'app_user'@'localhost' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON CPRG211Final.* TO 'app_user'@'localhost';
FLUSH PRIVILEGES;


CREATE TABLE Memberships (
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    Type VARCHAR(50),
    Price DOUBLE PRECISION
);

CREATE TABLE Customer (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(20)
);

CREATE TABLE CustomerMembership (
    CustomerId INT,
    MembershipId INT,
    PRIMARY KEY (CustomerId, MembershipId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(Id) ON DELETE CASCADE,
    FOREIGN KEY (MembershipId) REFERENCES Memberships(Id) ON DELETE CASCADE
);

CREATE TABLE Equipment (
    SerialNumber VARCHAR(50) PRIMARY KEY,
    ProductNumber VARCHAR(50),
    Description TEXT,
    Location VARCHAR(100)
);



INSERT INTO Memberships VALUES
(1, 'Basic Access', 'Monthly', 30.99),
(2, 'Premium Access', 'Monthly', 60.59),
(3, 'Student Plan', 'Yearly', 300),
(4, 'Family Plan', 'Yearly', 500);


INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber) VALUES
('Alice', 'Johnson', 'alice@example.com', '403-111-2222'),
('Bob', 'Smith', 'bob@example.com', '403-222-3333'),
('Charlie', 'Lee', 'charlie@example.com', '403-333-4444');


INSERT INTO CustomerMembership VALUES
(1, 1),
(1, 4),
(2, 2),
(3, 3);


INSERT INTO Equipment VALUES
('SN001', 'PRD1001', 'Treadmill', 'Cardio Room'),
('SN002', 'PRD1002', 'Elliptical', 'Cardio Room'),
('SN003', 'PRD2001', 'Bench Press', 'Weight Room'),
('SN004', 'PRD3001', 'Rowing Machine', 'Fitness Hall');
