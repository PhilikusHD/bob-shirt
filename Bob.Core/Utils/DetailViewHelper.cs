using Bob.Core.Domain;
using Bob.Core.Models;
using Bob.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Utils
{
    public static class DetailViewHelper
    {
        private static List<ProductVariant> m_AllVariants = new();
        private static List<Color> m_AllColors = new();
        private static List<Size> m_AllSizes = new();


        public static async Task InitDetailHelper()
        {
            if (m_AllVariants.Count > 0 &&
               m_AllColors.Count > 0 &&
               m_AllSizes.Count > 0)
            {
                return;
            }

            var allVariants = await ProductService.GetAllVariantsAsync();
            var allColors = await ProductService.GetAllColorsAsync();
            var allSizes = await ProductService.GetAllSizesAsync();

            m_AllVariants = allVariants.ToList();
            m_AllColors = allColors.ToList();
            m_AllSizes = allSizes.ToList();
        }

        public static ProductDisplay MapToDisplay(Product product)
        {
            var display = new ProductDisplay
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ImagePath = "", // placeholder for now
            };

            display.Variants = m_AllVariants
                .Where(v => v.ProductId == product.ProductId)
                .Select(v =>
                {
                    var variant = new ProductVariantDisplay
                    {
                        VariantId = v.VariantId,
                        Color = m_AllColors.FirstOrDefault(c => c.Id == v.ColorId)?.ColorName ?? "Unknown",
                        Size = m_AllSizes.FirstOrDefault(s => s.SizeId == v.SizeId)?.SizeName ?? "Unknown",
                        Stock = v.Stock,
                        FinalPrice = product.Price * (m_AllSizes.FirstOrDefault(s => s.SizeId == v.SizeId)?.PriceMultiplier ?? 1),
                        AvailableColors = m_AllColors.Select(c => c.ColorName).ToList(),
                        AvailableSizes = m_AllSizes.Select(s => s.SizeName).ToList(),
                    };

                    // Callback when user changes color or size
                    variant.OnVariantChanged = (changedVariant) =>
                    {
                        // Lookup the size object for multiplier
                        var sizeObj = m_AllSizes.FirstOrDefault(s => s.SizeName == changedVariant.Size);

                        // Lookup actual DB variant
                        var dbVar = m_AllVariants.FirstOrDefault(v =>
                            v.ProductId == product.ProductId &&
                            m_AllColors.First(c => c.Id == v.ColorId).ColorName == changedVariant.Color &&
                            m_AllSizes.First(s => s.SizeId == v.SizeId).SizeName == changedVariant.Size
                        );

                        // Update SelectedVariant with actual stock and recalculated price
                        display.SelectedVariant = new ProductVariantDisplay
                        {
                            VariantId = dbVar?.VariantId ?? 0,
                            Color = changedVariant.Color,
                            Size = changedVariant.Size,
                            Stock = dbVar?.Stock ?? 0,
                            FinalPrice = product.Price * (sizeObj?.PriceMultiplier ?? 1),
                            AvailableColors = variant.AvailableColors,
                            AvailableSizes = variant.AvailableSizes
                        };
                    };

                    return variant;
                }).ToList();

            // Initialize selected variant
            display.SelectedVariant = display.Variants.FirstOrDefault();

            return display;
        }
    }
}
