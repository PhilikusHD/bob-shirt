using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    public readonly struct CartId
    {
        public int Value { get; }
        public CartId(int value) => Value = value;
        public override string ToString() => Value.ToString();

        public static implicit operator CartId(int id) => new CartId(id);
        public static implicit operator int(CartId id) => id.Value;

    }

    [Table("CART")]
    public sealed class CartLine
    {
        public CartLine() { }

        [PrimaryKey]
        [Column("CARTID")]
        public CartId CartId { get; set; }

        [Column("ITEMID")]
        public ItemId ItemId { get; set; }

        [Column("ORDERID")]
        public OrderId? OrderId { get; set; } // null until checkout

        public CartLine(CartId cartId, ItemId itemId, OrderId? orderId)
        {
            CartId = cartId;
            ItemId = itemId;
            OrderId = orderId;
        }
    }
}
