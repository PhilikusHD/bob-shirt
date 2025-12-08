using LinqToDB.Mapping;
namespace Bob.Core.Domain
{

    [Table("ORDER_ITEM")]
    public sealed class OrderItemLine
    {
        public OrderItemLine() { }

        [PrimaryKey]
        [Column("VARIANTID")]
        public int VariantId { get; set; }

        [Column("AMOUNT")]
        public int Amount { get; set; }

        [PrimaryKey]
        [Column("ORDERID")]
        public int OrderId { get; set; } // null until checkout

        public OrderItemLine(int orderid, int productid, int amount)
        {
            OrderId = orderid;
            VariantId = productid;
            Amount = amount;
        }
    }
}
