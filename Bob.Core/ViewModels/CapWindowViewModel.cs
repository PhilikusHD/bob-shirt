using Bob.Core.Domain;
using Bob.Core.Models;
using Bob.Core.Services;
using Bob.Core.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.ViewModels
{
    public partial class CapWindowViewModel : ViewModelBase
    {

        private List<Product> m_AllCaps = new();

        [ObservableProperty]
        private ObservableCollection<ProductDisplay> m_CapProducts = new();

        [ObservableProperty]
        private string m_SearchText = "";

        [ObservableProperty]
        private ProductDisplay m_SelectedProduct;

        public CapWindowViewModel()
        {

            // Load Caps asynchronously
            _ = LoadCapsAsync();
        }

        private async Task LoadCapsAsync()
        {
            await DetailViewHelper.InitDetailHelper();

            await DetailViewHelper.InitDetailHelper();

            var allProducts = await ProductService.GetAllProductsAsync();
            m_AllCaps = allProducts.Where(p => p.TypeId == 3).ToList();
            UpdateFilteredCaps();
        }

        partial void OnSearchTextChanged(string value)
        {
            UpdateFilteredCaps();
        }

        private void UpdateFilteredCaps()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? m_AllCaps
                : m_AllCaps.Where(p => p.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

            CapProducts = new ObservableCollection<ProductDisplay>(
                filtered.Select(p => DetailViewHelper.MapToDisplay(p))
            );
        }
    }
}