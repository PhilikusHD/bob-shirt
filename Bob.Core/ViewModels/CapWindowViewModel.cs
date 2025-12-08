using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Bob.Core.Logging;
namespace Bob.Core.ViewModels
{
    public partial class CapWindowViewModel : ViewModelBase
    {

        private List<string> m_AllCaps = new();

        [ObservableProperty]
        private ObservableCollection<string> m_CapNames = new();

        [ObservableProperty]
        private string m_SearchText = "";

        public CapWindowViewModel()
        {

            // Load Caps asynchronously
            _ = LoadCapsAsync();
        }

        private async Task LoadCapsAsync()
        {
            try
            {
                var allProducts = await ProductService.GetAllProductsAsync();

                m_AllCaps = allProducts
                    .Where(p => p.TypeId == 3) // Filter Caps
                    .Select(p => p.Name)
                    .ToList();

                UpdateFilteredCaps();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load Caps", ex);
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            UpdateFilteredCaps();
        }

        private void UpdateFilteredCaps()
        {
            if (m_AllCaps == null || m_AllCaps.Count == 0)
                return;

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? m_AllCaps
                : m_AllCaps.Where(c => c.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            CapNames = new ObservableCollection<string>(filtered);
        }
    }
}