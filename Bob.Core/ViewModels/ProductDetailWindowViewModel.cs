using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        private ObservableCollection<ProductVariantDisplay> m_Variants = [];
        public ProductDetailWindowViewModel(Product product)
        {

            // Load Caps asynchronously
            CurrentProduct = product;
            _ = LoadProductDetailAsync();
        }

        private async Task LoadProductDetailAsync()
        {
            try
            {
                var variantEntities = await ProductService.GetVariantsForProductAsync(CurrentProduct.ProductId);

                var list = new List<ProductVariantDisplay>();

                foreach (var v in variantEntities)
                {
                    // Please do not make database calls inside a loop. 
                    // Prefetch all sizes and colors, and the prices outside the loop and simply perform a lookup.
                    // Check CalculateTotalPrice in System/CartSytem.cs for an example of this pattern.
                    // k thx byeeeeeeeeeeeeeeeeeeeeeee.
                    // Also... how the heck do you intend to wire this up to Caps, Hoodies and Tshirts? I'm very confused.
                    // But I trust you have a plan here :D
                    var size = await ProductService.GetSizeAsync(v.SizeId);
                    var color = await ProductService.GetColorAsync(v.ColorId);
                    var finalPrice = await ProductService.GetPriceAdjustedForSize(v.SizeId, CurrentProduct.Price);

                    list.Add(new ProductVariantDisplay
                    {
                        VariantId = v.VariantId,
                        Color = color.ColorName,
                        Size = size.SizeName,
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