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

        [ObservableProperty]
        private ObservableCollection<string> hoodieNames = [];

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

                var hoodies = allProducts
                    .Where(p => p.TypeId == 2) // Filter Hoodies
                    .Select(p => p.Name);

                HoodieNames = new ObservableCollection<string>(hoodies);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load Hoodies", ex);
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}