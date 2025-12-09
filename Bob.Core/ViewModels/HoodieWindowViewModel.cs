using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Models;
using Bob.Core.Services;
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
    public partial class HoodieWindowViewModel : ViewModelBase
    {
        private List<Product> m_AllHoodies = new();

        [ObservableProperty]
        private ObservableCollection<ProductDisplay> m_HoodieProducts = [];

        [ObservableProperty]
        private string m_SearchText = "";

        [ObservableProperty]
        private ProductDisplay m_SelectedProduct;

        public HoodieWindowViewModel()
        {
            _ = LoadHoodiesAsync();
        }

        private async Task LoadHoodiesAsync()
        {
            await DetailViewHelper.InitDetailHelper();
            var allProducts = await ProductService.GetAllProductsAsync();
            m_AllHoodies = allProducts.Where(p => p.TypeId == 2).ToList();
            UpdateFilteredHoodies();

        }

        partial void OnSearchTextChanged(string value)
        {
            UpdateFilteredHoodies();
        }

        private void UpdateFilteredHoodies()
        {
            if (m_AllHoodies == null || m_AllHoodies.Count == 0)
                return;

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? m_AllHoodies
                : m_AllHoodies.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));


            HoodieProducts = new ObservableCollection<ProductDisplay>(
                filtered.Select(p => DetailViewHelper.MapToDisplay(p))
            );
        }
    }
}