-- PRODUCT_TYPE ------------------------------------------------------
INSERT INTO PRODUCT_TYPE (TYPEID, TYPENAME) VALUES
(1, 'Shirt'),
(2, 'Hoodie'),
(3, 'Cap');

-- COLOR -------------------------------------------------------------
INSERT INTO COLOR (COLORID, COLORNAME) VALUES
(1, 'White'),
(2, 'Black');

-- SIZE --------------------------------------------------------------
INSERT INTO [SIZE] (SIZEID, SIZENAME, PRICEMULTIPLIER) VALUES
(1, 'S', 0.99),
(2, 'M', 1.0),
(3, 'L', 1.01),
(4, 'XL', 1.02),
(5, 'One Size', 1.0);

-- ADDRESS -----------------------------------------------------------
INSERT INTO ADDRESS (ADDRESSID, STREET, HOUSENUMBER, POSTALCODE, CITY) VALUES
(1, 'Main Street', '123', '12345', 'New York'),
(2, 'Oak Avenue', '456', '67890', 'Los Angeles'),
(3, 'Pine Road', '789', '11223', 'Chicago');

-- CUSTOMER ----------------------------------------------------------
INSERT INTO CUSTOMER (CUSTOMERID, [NAME], SURNAME, EMAIL, ADDRESSID, PHONENR, SIGNUPDATE, PASSWORDHASH, ISADMIN) VALUES
(1, 'John', 'Smith', 'john.smith@email.com', 1, '555-0101', '2024-01-15', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1),
(2, 'Emma', 'Johnson', 'emma.j@email.com', 2, '555-0102', '2024-02-20', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 0),
(3, 'Michael', 'Brown', 'michael.b@email.com', 3, '555-0103', '2024-03-10', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 0),
(4, 'Michael', 'Hund', 'michael.h@email.com', 3, '555-2312312', '2024-03-11', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 0);

-- PAYMENTPROCESSOR --------------------------------------------------
INSERT INTO PAYMENTPROCESSOR (PROCESSORID, METHOD) VALUES
(1, 'Credit Card'),
(2, 'PayPal'),
(3, 'Bank Transfer');

-- PRODUCT -----------------------------------------------------------
INSERT INTO PRODUCT (PRODUCTID, PRODUCTNAME, PRICE, TYPEID) VALUES
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
-- Shirt variants
-- PRODUCT_VARIANT ---------------------------------------------------
-- Shirt variants
INSERT INTO PRODUCT_VARIANT (VARIANTID, PRODUCTID, COLORID, SIZEID, STOCK) VALUES
(10, 10, 2, 2, 25),  -- Shirt - Gus, Black, M
(11, 11, 1, 3, 25),  -- Shirt - Sad Hampta, White, L
(12, 12, 2, 4, 25),  -- Shirt - Rat Stare, Black, XL
(13, 13, 1, 2, 25),  -- Shirt - Buggy, White, M
(14, 14, 2, 1, 25),  -- Shirt - Rati, Black, S
(15, 15, 1, 3, 25),  -- Shirt - Hampta Stare, White, L

-- Hoodie variants
(20, 20, 2, 3, 20),  -- Hoodie - Gus, Black, L
(21, 21, 1, 4, 20),  -- Hoodie - Sad Hampta, White, XL
(22, 22, 2, 2, 20),  -- Hoodie - Rat Stare, Black, M
(23, 23, 1, 3, 20),  -- Hoodie - Buggy, White, L
(24, 24, 2, 2, 20),  -- Hoodie - Rati, Black, M
(25, 25, 1, 1, 20),  -- Hoodie - Hampta Stare, White, S

-- Cap variants
(30, 30, 2, 5, 40),  -- Cap - Gus, Black, One Size
(31, 31, 1, 5, 40),  -- Cap - Sad Hampta, White, One Size
(32, 32, 2, 5, 40),  -- Cap - Rat Stare, Black, One Size
(33, 33, 1, 5, 40),  -- Cap - Buggy, White, One Size
(34, 34, 2, 5, 40),  -- Cap - Rati, Black, One Size
(35, 35, 1, 5, 40);  -- Cap - Hampta Stare, White, One Size

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
(3, 25, 1);  -- Order 3: 1x Hoodie - Hampta Stare

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