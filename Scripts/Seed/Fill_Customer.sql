INSERT INTO ADDRESS (ADDRESSID, STREET, HOUSENUMBER, POSTALCODE, CITY) VALUES
(1, 'Main Street', '123', 12345, 'New York'),
(2, 'Oak Avenue', '456', 67890, 'Los Angeles'),
(3, 'Pine Road', '789', 11223, 'Chicago');

INSERT INTO CUSTOMER (CUSTOMERID, [NAME], SURNAME, EMAIL, ADDRESSID, PHONENR, SIGNUPDATE) VALUES
(1, 'John', 'Smith', 'john.smith@email.com', 1, '555-0101', '2024-01-15'),
(2, 'Emma', 'Johnson', 'emma.j@email.com', 2, '555-0102', '2024-02-20'),
(3, 'Michael', 'Brown', 'michael.b@email.com', 3, '555-0103', '2024-03-10'),
(4, 'Michael', 'Hund', 'michael.h@email.com', 3, '555-2312312', '2024-03-11');

INSERT INTO PAYMENTPROCESSOR (PROCESSORID, METHOD) VALUES
(1, 'Credit Card'),
(2, 'PayPal'),
(3, 'Bank Transfer');

INSERT INTO PRODUCT (PRODUCTID, PRODUCTNAME, [SIZE], COLOR, PRIZE) VALUES
(1, 'T-Shirt', 'M', 'Blue', 19.99),
(2, 'Polo Shirt', 'L', 'White', 29.99),
(3, 'Tank Top', 'S', 'Black', 14.99);