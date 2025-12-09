using Bob.Core.Domain;
using Bob.Core.Models;
using Bob.Core.Services;
using Bob.Core.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.ViewModels;

public partial class TShirtWindowViewModel : ViewModelBase
{
    private List<Product> m_AllShirts = new();

    [ObservableProperty]
    private ObservableCollection<ProductDisplay> m_TshirtProducts = new();

    [ObservableProperty]
    private string m_SearchText = "";

    [ObservableProperty]
    private ProductDisplay m_SelectedProduct;

    public TShirtWindowViewModel()
    {
        _ = LoadTshirtsAsync();
    }

    private async Task LoadTshirtsAsync()
    {
        await DetailViewHelper.InitDetailHelper();

        var allProducts = await ProductService.GetAllProductsAsync();
        m_AllShirts = allProducts.Where(p => p.TypeId == 1).ToList();
        UpdateFilteredShirts();
    }

    partial void OnSearchTextChanged(string value)
    {
        UpdateFilteredShirts();
    }

    private void UpdateFilteredShirts()
    {
        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? m_AllShirts
            : m_AllShirts.Where(p => p.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

        TshirtProducts = new ObservableCollection<ProductDisplay>(
            filtered.Select(p => DetailViewHelper.MapToDisplay(p))
        );
    }
}
