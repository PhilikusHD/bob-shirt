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

        [ObservableProperty]
        private ObservableCollection<string> capNames = [];

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

                var caps = allProducts
                    .Where(p => p.TypeId == 3) // Filter Caps
                    .Select(p => p.Name);

                CapNames = new ObservableCollection<string>(caps);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load Caps", ex);
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}