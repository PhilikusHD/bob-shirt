using Bob.Core.Logging;
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
        private List<string> m_AllItems = new();

        [ObservableProperty]
        private ObservableCollection<string> m_CartItemNames = [];

        [ObservableProperty]
        private string m_SearchText = "";

        public CartWindowViewModel()
        {
            _ = LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            await DetailViewHelper.InitDetailHelper();

            try
            {
                var allProducts = await ProductService.GetAllProductsAsync();
                var cartItems = CartSystem.GetCartState();

                foreach (var variant in cartItems.ProductVariants)
                {
                    var product = allProducts.FirstOrDefault(p => p.ProductId == variant.ProductId);
                    if (product != null)
                    {
                        m_AllItems.Add(product.Name);
                    }
                }

                UpdateFilteredCartItems();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to load CartItems {ex.Message}");
            }
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
                : m_AllItems.Where(c => c.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            CartItemNames = new ObservableCollection<string>(filtered);
        }
    }
}
