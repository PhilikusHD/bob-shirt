using LinqToDB.Mapping;
namespace Bob.Core.Domain
{

    [Table("ORDER_ITEM")]
    public sealed class OrderItemLine
    {
        public OrderItemLine() { }

        [PrimaryKey]
        [Column("PRODUCTID")]
        public int ProductId { get; set; }
        
        [Column("AMOUNT")]
        public string Amount { get; set; }
        
        [PrimaryKey]
        [Column("ORDERID")]
        public int OrderId { get; set; } // null until checkout

        public OrderItemLine(int orderid, int productid, string amount)
        {
            OrderId = orderid;
            ProductId = productid;
            Amount = amount;
        }
    }
}
