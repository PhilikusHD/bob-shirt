namespace Bob.Core.Domain
{
    public readonly struct CartId
    {
        public uint Value { get; }
        public CartId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
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
