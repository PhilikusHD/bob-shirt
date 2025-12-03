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

### 1.5 ITEM
Product catalog for shirts/merchandise.

- **ItemID (PK)**: Unique identifier for each product
- **Name**: Product name/description
- **Price**: Current selling price
- **Size**: Available sizes (S, M, L, XL, etc.)
- **Color**: Available colors

### 1.6 PAYMENT
Records payment transactions for orders.

- **PaymentID (PK)**: Unique identifier for each payment
- **OrderID (FK)**: References the associated order
- **ProcessorID (FK)**: References the payment method/processor
- **PaymentDate**: Date and time of payment

### 1.7 PAYMENT_PROCESSOR
Defines available payment methods/gateways.

- **ProcessorID (PK)**: Unique identifier for payment processor
- **Method**: Payment method (e.g., Credit Card, PayPal, Bank Transfer)

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

**ITEM → ORDER_ITEM (1:many)**
- One item can appear in multiple orders
- Each order item references exactly one item

**ORDER → PAYMENT (1:1)**
- One order can have multiple payment attempts/records
- Each payment references exactly one order

**PAYMENT_PROCESSOR → PAYMENT (1:many)**
- One payment processor can process multiple payments
- Each payment uses exactly one payment processor

## 3. Entity Relationship Diagram

┌─────────────────┐      ┌─────────────────┐      ┌─────────────────┐
│    CUSTOMER     │      │     ADDRESS     │      │      ORDER      │
├─────────────────┤      ├─────────────────┤      ├─────────────────┤
│ • CustomerID(PK)│─────>│ • AddressID(PK) │      │ • OrderID(PK)   │
│ • Name          │      │ • Street        │      │ • CustomerID(FK)│
│ • Surname       │      │ • HouseNumber   │      │ • OrderDate     │
│ • Email         │      │ • PostalCode    │      │ • TotalAmount   │
│ • PhoneNumber   │      │ • City          │      │                 │
│ • SignupDate    │      │                 │      └─────────────────┘
│ • AddressID(FK) │      └─────────────────┘                │
└────────┬────────┘                                         │
         │                                                  │
         │                                                  │
         │                                         ┌────────▼────────┐      ┌──────────────────────┐
         │                                         │   ORDER_ITEM    │      │        ITEM          │
         │                                         ├─────────────────┤      ├──────────────────────┤
         │                                         │ • OrderID(FK)   │─────>│ • ItemID(PK)         │
         │                                         │ • ItemID(FK)    │      │ • Name               │
         │                                         │ • Quantity?     │      │ • Price              │
         │                                         │ • PriceAtOrder? │      │ • Size               │
         └─────────────────────────────────────────│                 │      │ • Color              │
                                                   └─────────────────┘      └──────────────────────┘
                                                            │
                                                            │
                                                   ┌────────▼─────────┐      ┌──────────────────────┐
                                                   │     PAYMENT      │      │ PAYMENT_PROCESSOR    │
                                                   ├──────────────────┤      ├──────────────────────┤
                                                   │ • PaymentID(PK)  │      │ • ProcessorID(PK)    │
                                                   │ • OrderID(FK)    │─────>│ • Method             │
                                                   │ • ProcessorID(FK)│      │                      │
                                                   │ • PaymentDate    │      └──────────────────────┘
                                                   │                  │
                                                   └──────────────────┘