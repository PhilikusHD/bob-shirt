namespace Bob.Core.Domain
{
    public readonly struct CartId
    {
        public uint Value { get; }
        public CartId(uint value) => Value = value;
        public override string ToString() => Value.ToString();

        public static implicit operator CartId(uint id) => new CartId(id);
        public static implicit operator uint(CartId id) => id.Value;

    }

    public sealed class CartLine
    {
        public CartId CartId { get; }
        public ItemId ItemId { get; }
        public OrderId? OrderId { get; } // null until checkout

        public CartLine(CartId cartId, ItemId itemId, OrderId? orderId)
        {
            CartId = cartId;
            ItemId = itemId;
            OrderId = orderId;
        }
    }
}
