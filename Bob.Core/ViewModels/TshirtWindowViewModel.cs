using Bob.Core.Domain;
using Bob.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using Bob.Core.Logging;

namespace Bob.Core.ViewModels;

public partial class TShirtWindowViewModel : ViewModelBase
{

    [ObservableProperty]
    private ObservableCollection<string> tshirtNames = [];

    public TShirtWindowViewModel()
    {

        // Load T-shirts asynchronously
        _ = LoadTshirtsAsync();
    }

    private async Task LoadTshirtsAsync()
    {
        try
        {
            var allProducts = await ProductService.GetAllProductsAsync();

            var tshirts = allProducts
                .Where(p => p.TypeId == 1) // Filter T-shirts
                .Select(p => p.Name);

            TshirtNames = new ObservableCollection<string>(tshirts);
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to load T-shirts", ex);
        }
    }
}