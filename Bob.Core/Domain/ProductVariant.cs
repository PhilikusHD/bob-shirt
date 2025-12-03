using LinqToDB.Mapping;
namespace Bob.Core.Domain
{


    [Table("PRODUCT_VARIANT")]
    public sealed class ProductVariant
    {
        public ProductVariant() { }

        [PrimaryKey]
        [Column("VARIANTID")]
        public int VariantId { get; set; }

        [Column("PRODUCTID")]
        public int ProductId { get; set; }

        [Column("COLORID")]
        public int ColorId { get; set; }

        [Column("SIZEID")]
        public int SizeId { get; set; }

        [Column("STOCK")]
        public int Stock { get; set; }

        public ProductVariant(int variantId, int productId, int colorId, int sizeId, int stock)
        {

            VariantId = variantId;
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
            Stock = stock;

        }
    }
}
