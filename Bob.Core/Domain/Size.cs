using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    [Table("[SIZE]")]
    public class Size
    {
        public Size() { }

        [PrimaryKey]
        [Column("SIZEID")]
        public int SizeId { get; set; }

        [Column("SIZENAME")]
        public string SizeName { get; set; }

        [Column("PRICEMULTIPLIER")]
        public decimal PriceMultiplier { get; set; }

        public Size(int sizeId, string sizeName, decimal priceMultiplier)
        {
            SizeId = sizeId;
            SizeName = sizeName;
            PriceMultiplier = priceMultiplier;
        }
    }
}
