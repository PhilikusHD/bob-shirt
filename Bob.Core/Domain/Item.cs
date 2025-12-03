using LinqToDB.Mapping;
namespace Bob.Core.Domain
{
    public readonly struct ItemId
    {
        public int Value { get; }
        public ItemId(int value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator ItemId(int id) => new ItemId(id);
        public static implicit operator int(ItemId id) => id.Value;
    }

    [Table("ITEM")]
    public sealed class Item
    {
        public Item() { }

        [PrimaryKey]
        [Column("ITEMID")]
        public ItemId Id { get; set; }

        [Column("ITEMNAME")]
        public string Name { get; set; }

        [Column("SIZE")]
        public string Size { get; set; }

        [Column("COLOR")]
        public string Color { get; set; }

        [Column("PRIZE")]
        public decimal Price { get; set; }

        public Item(ItemId id, string name, string size, string color, decimal price)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
            Price = price;
        }
    }
}
