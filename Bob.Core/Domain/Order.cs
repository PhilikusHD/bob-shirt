using System;

namespace Bob.Core.Domain
{
    public readonly struct OrderId
    {
        public uint Value { get; }
        public OrderId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public sealed class Order
    {
        public OrderId Id { get; }
        public CustomerId CustomerId { get; }
        public CartId CartId { get; }
        public DateTime OrderDate { get; }
        public decimal TotalAmount { get; }

        public Order(OrderId id, DateTime orderDate, decimal totalAmount)
        {
            Id = id;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
        }
    }
}
