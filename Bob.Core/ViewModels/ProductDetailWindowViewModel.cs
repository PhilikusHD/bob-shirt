using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Bob.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Bob.Core.Logging;
using Bob.Core.Domain;
using Bob.Core.Models;

namespace Bob.Core.ViewModels
{
    public partial class ProductDetailWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Product m_CurrentProduct;

        [ObservableProperty]
        private ObservableCollection<ProductVariantDisplay> m_Variants = new();

        public ProductDetailWindowViewModel(Product product)
        {
            CurrentProduct = product;
            _ = LoadProductDetailAsync();
        }

        private async Task LoadProductDetailAsync()
        {
            try
            {
                var variantEntities = await ProductService.GetVariantsForProductAsync(CurrentProduct.ProductId);

                if (!variantEntities.Any())
                {
                    Variants.Clear();
                    return;
                }

                var sizeIds = variantEntities.Select(v => v.SizeId).Distinct().ToList();
                var colorIds = variantEntities.Select(v => v.ColorId).Distinct().ToList();

                var sizeTasks = sizeIds.Select(id => ProductService.GetSizeAsync(id));
                var colorTasks = colorIds.Select(id => ProductService.GetColorAsync(id));

                await Task.WhenAll(
                    Task.WhenAll(sizeTasks),
                    Task.WhenAll(colorTasks)
                );

                var sizeDictionary = (await Task.WhenAll(sizeTasks))
                    .Where(s => s != null)
                    .ToDictionary(s => s.SizeId, s => s.SizeName);

                var colorDictionary = (await Task.WhenAll(colorTasks))
                    .Where(c => c != null)
                    .ToDictionary(c => c.Id, c => c.ColorName);

                var priceTasks = sizeIds.Select(async sizeId =>
                {
                    var price = await ProductService.GetPriceAdjustedForSize(sizeId, CurrentProduct.Price);
                    return (SizeId: sizeId, Price: price);
                }).ToList();

                var priceResults = await Task.WhenAll(priceTasks);
                var priceDictionary = priceResults.ToDictionary(p => p.SizeId, p => p.Price);

                var list = new List<ProductVariantDisplay>();

                foreach (var v in variantEntities)
                {
                    var sizeName = sizeDictionary.TryGetValue(v.SizeId, out var s) ? s : "Unknown";
                    var colorName = colorDictionary.TryGetValue(v.ColorId, out var c) ? c : "Unknown";
                    var finalPrice = priceDictionary.TryGetValue(v.SizeId, out var p) ? p : CurrentProduct.Price;

                    list.Add(new ProductVariantDisplay
                    {
                        VariantId = v.VariantId,
                        Color = colorName,
                        Size = sizeName,
                        FinalPrice = finalPrice,
                        Stock = v.Stock
                    });
                }

                Variants = new ObservableCollection<ProductVariantDisplay>(list);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load Product Details", ex);
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}