using System.Collections.Generic;

namespace Bob.Core.Models
{
    public class ProductVariantDisplay
    {
        public int VariantId { get; set; }
        public string Color { get; set; } = "";
        public string Size { get; set; } = "";

        public decimal FinalPrice { get; set; }
        public int Stock { get; set; }

        public List<string> AvailableColors { get; set; } = new();
        public List<string> AvailableSizes { get; set; } = new();
    }

    public class ProductDisplay
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public List<ProductVariantDisplay> Variants { get; set; } = new();
    }
}
