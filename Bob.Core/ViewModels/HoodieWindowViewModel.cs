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
    public partial class HoodieWindowViewModel : ViewModelBase
    {
        private List<string> m_AllHoodies = new();

        [ObservableProperty]
        private ObservableCollection<string> m_HoodieNames = [];

        [ObservableProperty]
        private string m_SearchText = "";

        public HoodieWindowViewModel()
        {

            // Load Hoodies asynchronously
            _ = LoadHoodiesAsync();
        }

        private async Task LoadHoodiesAsync()
        {
            try
            {
                var allProducts = await ProductService.GetAllProductsAsync();

                m_AllHoodies = allProducts
                    .Where(p => p.TypeId == 2) // Filter Hoodies
                    .Select(p => p.Name).ToList();

                UpdateFilteredHoodies();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load Hoodies", ex);
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
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
                : m_AllHoodies.Where(c => c.Contains(SearchText, StringComparison.OrdinalIgnoreCase));


            HoodieNames = new ObservableCollection<string>(filtered);
        }
    }
}