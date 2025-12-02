namespace Bob.Core.Domain
{
    public readonly struct ItemId
    {
        public uint Value { get; }
        public ItemId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator ItemId(uint id) => new ItemId(id);
        public static implicit operator uint(ItemId id) => id.Value;
    }

    public sealed class Item
    {
        public ItemId Id { get; }
        public string Name { get; }
        public string Size { get; }
        public string Color { get; }
        public decimal Price { get; }

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
