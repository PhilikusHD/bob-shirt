using Bob.Core.Domain;
using Bob.Core.Models;
using Bob.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.ViewModels;

public partial class TShirtWindowViewModel : ViewModelBase
{
    private List<Product> m_AllShirts = new();
    private List<ProductVariant> m_AllVariants = new();

    private List<Color> m_AllColors = new();
    private List<Size> m_AllSizes = new();

    [ObservableProperty]
    private ObservableCollection<ProductDisplay> m_TshirtProducts = new();

    [ObservableProperty]
    private string m_SearchText = "";

    [ObservableProperty]
    private ProductDisplay m_SelectedProduct;

    public TShirtWindowViewModel()
    {
        _ = LoadTshirtsAsync();
    }

    private async Task LoadTshirtsAsync()
    {
        var allProducts = await ProductService.GetAllProductsAsync();
        var allVariants = await ProductService.GetAllVariantsAsync();
        var allColors = await ProductService.GetAllColorsAsync();
        var allSizes = await ProductService.GetAllSizesAsync();

        m_AllShirts = allProducts.Where(p => p.TypeId == 1).ToList();
        m_AllVariants = allVariants.ToList();
        m_AllColors = allColors.ToList();
        m_AllSizes = allSizes.ToList();

        UpdateFilteredShirts();
    }

    partial void OnSearchTextChanged(string value)
    {
        UpdateFilteredShirts();
    }

    private void UpdateFilteredShirts()
    {
        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? m_AllShirts
            : m_AllShirts.Where(p => p.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

        TshirtProducts = new ObservableCollection<ProductDisplay>(
            filtered.Select(p => MapToDisplay(p))
        );
    }

    private ProductDisplay MapToDisplay(Product product)
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
                    Stock = "In Stock: " + v.Stock,
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
                        Stock = "In Stock: " + (dbVar?.Stock ?? 0),
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
