using System;

namespace Bob.Core.Domain
{
    public readonly struct OrderId
    {
        public uint Value { get; }
        public OrderId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator OrderId(uint id) => new OrderId(id);
        public static implicit operator uint(OrderId id) => id.Value;

    }

    public sealed class Order
    {
        public OrderId Id { get; }
        public CustomerId CustomerId { get; }
        public CartId CartId { get; }
        public DateTime OrderDate { get; }
        public decimal TotalAmount { get; }

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
