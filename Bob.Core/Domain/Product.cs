using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    [Table("PRODUCT")]
    public sealed class Product
    {
        public Product() { }

        [PrimaryKey]
        [Column("PRODUCTID")]
        public int ProductId { get; set; }

        [Column("PRODUCTNAME")]
        public string Name { get; set; }

        [Column("TYPEID")]
        public int TypeId { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; }

        public Product(int id, string name, int typeId, decimal price)
        {
            ProductId = id;
            Name = name;
            TypeId = typeId;
            Price = price;
        }
    }
}
