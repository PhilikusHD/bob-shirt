namespace Bob.Core.Models
{
    public class ProductVariantDisplay 
    { 
        public int VariantId { get; set; } 
        public string Color { get; set; } 
        public string Size { get; set; } 
        public decimal FinalPrice { get; set; } 
        public int Stock { get; set; } 
    }
}
