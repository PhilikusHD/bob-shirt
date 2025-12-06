-- PRODUCT_TYPE ------------------------------------------------------
INSERT INTO PRODUCT_TYPE (TYPEID, TYPENAME) VALUES
(1, 'Shirt'),
(2, 'Hoodie'),
(3, 'Cap');

-- COLOR -------------------------------------------------------------
INSERT INTO COLOR (COLORID, COLORNAME) VALUES
(1, 'Blue'),
(2, 'White'),
(3, 'Black'),
(4, 'Gray'),
(5, 'Red'),
(6, 'Green'),
(7, 'Navy');

-- SIZE --------------------------------------------------------------
INSERT INTO [SIZE] (SIZEID, SIZENAME, PRICEMULTIPLIER) VALUES
(1, 'S', 0.9),
(2, 'M', 1.0),
(3, 'L', 1.1),
(4, 'XL', 1.2),
(5, 'One Size', 1.0);

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
INSERT INTO PRODUCT (PRODUCTID, PRODUCTNAME, PRICE, TYPEID) VALUES
-- Basic items
(1, 'Shirt - ITECH', 19.99, 1),
(2, 'Cap - ITECH', 29.99, 3),
(3, 'Hoodie - ITECH', 14.99, 2),

-- Shirts with meme prints
(10, 'Shirt - Gus', 24.99, 1),
(11, 'Shirt - Sad Hampta', 24.99, 1),
(12, 'Shirt - Rat Stare', 24.99, 1),
(13, 'Shirt - Buggy', 24.99, 1),
(14, 'Shirt - Rati', 24.99, 1),
(15, 'Shirt - Hampta Stare', 24.99, 1),

-- Hoodies with meme prints
(20, 'Hoodie - Gus', 39.99, 2),
(21, 'Hoodie - Sad Hampta', 39.99, 2),
(22, 'Hoodie - Rat Stare', 39.99, 2),
(23, 'Hoodie - Buggy', 39.99, 2),
(24, 'Hoodie - Rati', 39.99, 2),
(25, 'Hoodie - Hampta Stare', 39.99, 2),

-- Caps with meme prints
(30, 'Cap - Gus', 14.99, 3),
(31, 'Cap - Sad Hampta', 14.99, 3),
(32, 'Cap - Rat Stare', 14.99, 3),
(33, 'Cap - Buggy', 14.99, 3),
(34, 'Cap - Rati', 14.99, 3),
(35, 'Cap - Hampta Stare', 14.99, 3);

-- PRODUCT_VARIANT ---------------------------------------------------
-- ITECH products
INSERT INTO PRODUCT_VARIANT (VARIANTID, PRODUCTID, COLORID, SIZEID, STOCK) VALUES
(1, 1, 1, 2, 50),   -- Shirt - ITECH, Blue, M
(2, 2, 2, 3, 30),   -- Cap - ITECH, White, L
(3, 3, 3, 1, 40),   -- Hoodie - ITECH, Black, S

-- Shirt variants
(10, 10, 3, 2, 25),  -- Shirt - Gus, Black, M
(11, 11, 2, 3, 25),  -- Shirt - Sad Hampta, White, L
(12, 12, 4, 4, 25),  -- Shirt - Rat Stare, Gray, XL
(13, 13, 5, 2, 25),  -- Shirt - Buggy, Red, M
(14, 14, 3, 1, 25),  -- Shirt - Rati, Black, S
(15, 15, 1, 3, 25),  -- Shirt - Hampta Stare, Blue, L

-- Hoodie variants
(20, 20, 3, 3, 20),  -- Hoodie - Gus, Black, L
(21, 21, 4, 4, 20),  -- Hoodie - Sad Hampta, Gray, XL
(22, 22, 2, 2, 20),  -- Hoodie - Rat Stare, White, M
(23, 23, 6, 3, 20),  -- Hoodie - Buggy, Green, L
(24, 24, 3, 2, 20),  -- Hoodie - Rati, Black, M
(25, 25, 7, 1, 20),  -- Hoodie - Hampta Stare, Navy, S

-- Cap variants
(30, 30, 3, 5, 40),  -- Cap - Gus, Black, One Size
(31, 31, 2, 5, 40),  -- Cap - Sad Hampta, White, One Size
(32, 32, 1, 5, 40),  -- Cap - Rat Stare, Blue, One Size
(33, 33, 5, 5, 40),  -- Cap - Buggy, Red, One Size
(34, 34, 4, 5, 40),  -- Cap - Rati, Gray, One Size
(35, 35, 3, 5, 40);  -- Cap - Hampta Stare, Black, One Size

-- [ORDER] -----------------------------------------------------------
INSERT INTO [ORDER] (ORDERID, CUSTOMERID, ORDERDATE, TOTALAMOUNT) VALUES
(1, 1, '2024-03-01', 39.98),
(2, 2, '2024-03-05', 64.98),
(3, 3, '2024-03-10', 89.97);

-- ORDER_ITEM --------------------------------------------------------
INSERT INTO ORDER_ITEM (ORDERID, VARIANTID, AMOUNT) VALUES
(1, 10, 1),  -- Order 1: 1x Shirt - Gus
(1, 30, 1),  -- Order 1: 1x Cap - Gus
(2, 20, 1),  -- Order 2: 1x Hoodie - Gus
(2, 11, 1),  -- Order 2: 1x Shirt - Sad Hampta
(3, 1, 2),   -- Order 3: 2x Shirt - ITECH
(3, 3, 1);   -- Order 3: 1x Hoodie - ITECH

-- PAYMENT -----------------------------------------------------------
INSERT INTO PAYMENT (PAYMENTID, ORDERID, PAYMENTDATE, PROCESSORID) VALUES
(1, 1, '2024-03-01', 1),
(2, 2, '2024-03-05', 2),
(3, 3, '2024-03-10', 3);

-- SHIPMENT ----------------------------------------------------------
INSERT INTO SHIPMENT (SHIPMENTID, ORDERID, ESTIMATEDDELIVERYDATE, SHIPMENTSTATUS, SHIPPINGCOST) VALUES
(1, 1, '2024-03-05', 'Delivered', 4.99),
(2, 2, '2024-03-09', 'In Transit', 4.99),
(3, 3, '2024-03-14', 'Processing', 4.99);