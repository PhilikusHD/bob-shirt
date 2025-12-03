using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    public readonly struct OrderId
    {
        public int Value { get; }
        public OrderId(int value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator OrderId(int id) => new OrderId(id);
        public static implicit operator int(OrderId id) => id.Value;

    }

    [Table("[ORDER]")]
    public sealed class Order
    {
        public Order() { }

        [PrimaryKey]
        [Column("ORDERID")]
        public OrderId Id { get; set; }

        [Column("CUSTOMERID")]
        public CustomerId CustomerId { get; set; }

        [Column("CARTID")]
        public CartId CartId { get; set; }

        [Column("ORDERDATE")]
        public DateTime OrderDate { get; set; }

        [Column("TOTALAMOUNT")]
        public decimal TotalAmount { get; set; }

        public Order(OrderId id, CustomerId customerId, CartId cartId, DateTime orderDate, decimal totalAmount)
        {
            Id = id;
            CustomerId = customerId;
            CartId = cartId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
        }
    }
}
