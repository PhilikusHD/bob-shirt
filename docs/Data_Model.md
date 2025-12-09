# Bob-Shirt Data Model

## 1. Entity Descriptions

### 1.1 CUSTOMER
Stores customer personal and contact information.

- **CustomerID (PK)**: Unique identifier for each customer
- **Name**: Customer's first name
- **Surname**: Customer's last name
- **Email**: Customer email address (for communication and login)
- **PhoneNumber**: Contact phone number
- **SignupDate**: Date when customer registered
- **AddressID (FK)**: References customer's primary shipping/billing address

### 1.2 ADDRESS
Manages physical address information for customers.

- **AddressID (PK)**: Unique identifier for each address
- **Street**: Street name
- **HouseNumber**: Building/house number
- **PostalCode**: ZIP/postal code
- **City**: City name

### 1.3 ORDER
Tracks customer orders and transaction details.

- **OrderID (PK)**: Unique identifier for each order
- **CustomerID (FK)**: References the customer who placed the order
- **OrderDate**: Date and time when order was placed
- **TotalAmount**: Final order total (calculated sum)

### 1.4 ORDER_ITEM
Junction table linking orders to items with order-specific details.

- **OrderID (FK)**: References the parent order
- **ItemID (FK)**: References the ordered item
- **Quantity**: Number of units ordered (marked with "?" - may be nullable or require validation)
- **PriceAtOrder**: Price of item at time of order (marked with "?" - likely captures historical pricing)

### 1.5 PRODUCT
Product catalog for merchandise.

- **ProductID (PK)**: Unique identifier for each product
- **TypeId (FK): Reference to the Product type(Hoodie, T-Shirt, Cap)
- **Price**: Current selling price
- **Name**: The Product Name

### 1.6 PRODUCT_VARIANT
Product Variants of merchandise.

- **VariantId (PK): Unique identifier for each variant
- **ProductID** (FK)**: Reference to the Base Product
- **ColorId** (FK): Reference to the size
- **SizeId** (FK): Reference to the color
- **Stock**: Current Variant stock

### 1.7 COLOR
All Product Colors

- **ColorId (PK): Unique identifier for each Color
- **Name**: The Color name

### 1.8 SIZE
All Available sizes

- **SizeId (PK): Unique identifier for each Color
- **Name**: The Size name
- **Price Multiplier**: How much the size changes the price

### 1.9 PRODUCT_TYPE
The Products Type

- **TypeId (PK): Unique identifier for each Type
- **Type Name**: The Type name


### 1.10 PAYMENT
Records payment transactions for orders.

- **PaymentID (PK)**: Unique identifier for each payment
- **OrderID (FK)**: References the associated order
- **ProcessorID (FK)**: References the payment method/processor
- **PaymentDate**: Date and time of payment

### 1.11 PAYMENT_PROCESSOR
Defines available payment methods/gateways.

- **ProcessorID (PK)**: Unique identifier for payment processor
- **Method**: Payment method (e.g., Credit Card, PayPal, Bank Transfer)

### 1.12 SHIPMENT
Records shipment Data for orders.

- **ShipmentID (PK)**: Unique identifier for each Shipment
- **OrderID (FK)**: References the associated order
- **EsitmatedDeliveryDate**: When the purchase probably arrives
- **ShippingCost**: How much the shipping costs
- **ShipmentStatus**: Sent, Delivered

## 2. Relationships

### 2.1 Primary Relationships

**CUSTOMER → ADDRESS (1:1)**
- Each customer has at least one address (shipping/billing)
- Address can belong to only one customer (implied by FK in CUSTOMER)

**CUSTOMER → ORDER (1:many)**
- One customer can place multiple orders
- Each order belongs to exactly one customer

**ORDER → ORDER_ITEM (1:many)**
- One order contains multiple items
- Each order item belongs to exactly one order

**PRODUCT → PRODUCT_VARIANT (1:many)**
- One Product can have multiple Product Variants
- Each Product Variant can only have one Product

**PRODUCT_TYPE → PRODUCT (1:many)**
- One Product has One Product Type
- A Product Type can belong to multiple Products

**PRODUCT_VARIANT → ORDER_ITEM (1:many)**
- One Order Item has One Product variant
- A Product variant can have multiple Order Items

**COLOR → PRODUCT_VARIANT (1:many)**
- One Color can have multiple Product Variants
- Each Product Variant can only have one Color

**SIZE → PRODUCT_VARIANT (1:many)**
- One Size can belong to multiple Product Variants
- Each Product Variant can only have one Size

**ORDER → PAYMENT (1:1)**
- One order can have multiple payment attempts/records
- Each payment references exactly one order

**PAYMENT_PROCESSOR → PAYMENT (1:many)**
- One payment processor can process multiple payments
- Each payment uses exactly one payment processor

**ORDER → SHIPMENT (1:1)**
- One Order has one Shipment and for each Shipment one Order is placed

**ORDER → SHIPMENT (1:1)**
- One Order has one Shipment and for each Shipment one Order is placed

## 3. Entity Relationship Diagram

CUSTOMER (1:1) ADDRESS
├── CustomerID (PK)                ┌── AddressID (PK)
├── Name                           ├── Street
├── Surname                        ├── HouseNumber
├── Email                          ├── PostalCode
├── PhoneNumber                    └── City
├── SignupDate
└── AddressID (FK) ──────────────────┘

          │ (1:N)
          ▼
        ORDER (1:1) SHIPMENT
        ├── OrderID (PK)            ┌── ShipmentID (PK)
        ├── CustomerID (FK) ────────┼── OrderID (FK)
        ├── OrderDate               ├── EstimatedDeliveryDate
        └── TotalAmount             ├── ShippingCost
                                    └── ShipmentStatus

          │ (1:N)                         │ (1:N)
          ▼                               ▼
   ORDER_ITEM (N:1) PRODUCT_VARIANT ── COLOR
   ├── OrderID (FK)   ├── VariantId (PK)  ├── ColorId (PK)
   ├── ItemID (FK) ───┤ ├── ProductID (FK)├── Name
   ├── Quantity       │ ├── ColorId (FK) ─┘
   └── PriceAtOrder  └─┤ ├── SizeId (FK) ───┐
                       │ └── Stock          │
                       │                    ▼
                       │                  SIZE
                       │                  ├── SizeId (PK)
                       │                  ├── Name
                       └── ProductID (FK) └── PriceMultiplier
                                  │
                                  ▼
                              PRODUCT (N:1) PRODUCT_TYPE
                              ├── ProductID (PK)      ├── TypeId (PK)
                              ├── TypeId (FK) ────────┤ └── TypeName
                              ├── Price               │
                              └── Name                │
                                                      │ (1:N)
                                                      ▼
                                                    PAYMENT
                                                    ├── PaymentID (PK)
                                                    ├── OrderID (FK) ───┐
                                                    ├── ProcessorID (FK)│
                                                    └── PaymentDate    │
                                                                       │
                                                            PAYMENT_PROCESSOR
                                                            ├── ProcessorID (PK)
                                                            └── Method

KEY:
(PK) = Primary Key      (1:1) = One-to-One
(FK) = Foreign Key      (1:N) = One-to-Many
                        (N:1) = Many-to-One