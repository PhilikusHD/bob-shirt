-- ADDRESS -----------------------------------------------------------
INSERT INTO ADDRESS (ADDRESSID, STREET, HOUSENUMBER, POSTALCODE, CITY) VALUES
(1, 'Main Street', '123', 12345, 'New York'),
(2, 'Oak Avenue', '456', 67890, 'Los Angeles'),
(3, 'Pine Road', '789', 11223, 'Chicago');

-- CUSTOMER ----------------------------------------------------------
INSERT INTO CUSTOMER (CUSTOMERID, [NAME], SURNAME, EMAIL, ADDRESSID, PHONENR, SIGNUPDATE) VALUES
(1, 'John', 'Smith', 'john.smith@email.com', 1, '555-0101', '2024-01-15'),
(2, 'Emma', 'Johnson', 'emma.j@email.com', 2, '555-0102', '2024-02-20'),
(3, 'Michael', 'Brown', 'michael.b@email.com', 3, '555-0103', '2024-03-10'),
(4, 'Michael', 'Hund', 'michael.h@email.com', 3, '555-2312312', '2024-03-11');

-- PAYMENTPROCESSOR --------------------------------------------------
INSERT INTO PAYMENTPROCESSOR (PROCESSORID, METHOD) VALUES
(1, 'Credit Card'),
(2, 'PayPal'),
(3, 'Bank Transfer');

-- PRODUCT -----------------------------------------------------------
-- Existing items
INSERT INTO PRODUCT (PRODUCTID, PRODUCTNAME, [SIZE], COLOR, PRIZE) VALUES
(1, 'Shirt - ITECH', 'M', 'Blue', 19.99),
(2, 'Cap - ITECH', 'L', 'White', 29.99),
(3, 'Hoodie - ITECH', 'S', 'Black', 14.99),

-- Shirts with meme prints
(10, 'Shirt - Gus', 'M', 'Black', 24.99),
(11, 'Shirt - Sad Hampta', 'L', 'White', 24.99),
(12, 'Shirt - Rat Stare', 'XL', 'Gray', 24.99),
(13, 'Shirt - Buggy', 'M', 'Red', 24.99),
(14, 'Shirt - Rati', 'S', 'Black', 24.99),
(15, 'Shirt - Hampta Stare', 'L', 'Blue', 24.99),

-- Hoodies with meme prints
(20, 'Hoodie - Gus', 'L', 'Black', 39.99),
(21, 'Hoodie - Sad Hampta', 'XL', 'Gray', 39.99),
(22, 'Hoodie - Rat Stare', 'M', 'White', 39.99),
(23, 'Hoodie - Buggy', 'L', 'Green', 39.99),
(24, 'Hoodie - Rati', 'M', 'Black', 39.99),
(25, 'Hoodie - Hampta Stare', 'S', 'Navy', 39.99),

-- Caps with meme prints
(30, 'Cap - Gus', 'One Size', 'Black', 14.99),
(31, 'Cap - Sad Hampta', 'One Size', 'White', 14.99),
(32, 'Cap - Rat Stare', 'One Size', 'Blue', 14.99),
(33, 'Cap - Buggy', 'One Size', 'Red', 14.99),
(34, 'Cap - Rati', 'One Size', 'Gray', 14.99),
(35, 'Cap - Hampta Stare', 'One Size', 'Black', 14.99);
