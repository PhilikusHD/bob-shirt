using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Models;
using Bob.Core.Services;
using Bob.Core.Systems;
using Bob.Core.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.ViewModels
{
    public partial class CartWindowViewModel : ViewModelBase
    {
        private List<Product> m_AllItems = new();

        [ObservableProperty]
        private ObservableCollection<ProductDisplay> m_CartItems = [];

        [ObservableProperty]
        private string m_SearchText = "";

        [ObservableProperty]
        private ProductDisplay m_SelectedProduct;

        public CartWindowViewModel()
        {
            _ = LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            await DetailViewHelper.InitDetailHelper();

            var allProducts = await ProductService.GetAllProductsAsync();
            var cartItems = CartSystem.GetCartState();

            foreach (var variant in cartItems.ProductVariants)
            {
                var product = allProducts.FirstOrDefault(p => p.ProductId == variant.ProductId);
                if (product != null)
                {
                    m_AllItems.Add(product);
                }
            }

            UpdateFilteredCartItems();
        }

        public async void RemoveSelected()
        {
            if (SelectedProduct == null)
                return;

            await CartSystem.RemoveFromCart(SelectedProduct.SelectedVariant.VariantId);
        }

        partial void OnSearchTextChanged(string value)
        {
            UpdateFilteredCartItems();
        }

        private void UpdateFilteredCartItems()
        {
            if (m_AllItems == null || m_AllItems.Count == 0)
                return;

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? m_AllItems
                : m_AllItems.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            CartItems = new ObservableCollection<ProductDisplay>(
                filtered.Select(p => DetailViewHelper.MapToDisplay(p))
            );
        }
    }
}
