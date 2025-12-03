using LinqToDB.Mapping;
namespace Bob.Core.Domain
{


    [Table("PRODUCT")]
    public sealed class Product
    {
        public Product() { }

        [PrimaryKey]
        [Column("PRODUCTID")]
        public int Id { get; set; }

        [Column("PRODUCTNAME")]
        public string Name { get; set; }

        [Column("[SIZE]")]
        public string Size { get; set; }

        [Column("COLOR")]
        public string Color { get; set; }

        [Column("PRIZE")]
        public decimal Price { get; set; }

        public Product(int id, string name, string size, string color, decimal price)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
            Price = price;
        }
    }
}
