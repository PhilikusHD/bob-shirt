using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.ViewModels;

public partial class TShirtWindowViewModel : ViewModelBase
{
    private List<string> m_AllShirts = new();

    [ObservableProperty]
    private ObservableCollection<string> m_TshirtNames = [];

    [ObservableProperty]
    private string m_SearchText = "";

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

            m_AllShirts = allProducts
                .Where(p => p.TypeId == 1) // Filter T-shirts
                .Select(p => p.Name).ToList();

            UpdateFilteredShirts();
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to load T-shirts", ex);
            Console.WriteLine($"Exception caught: {ex.Message}");
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        UpdateFilteredShirts();
    }

    private void UpdateFilteredShirts()
    {
        if (m_AllShirts == null || m_AllShirts.Count == 0)
            return;

        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? m_AllShirts
            : m_AllShirts.Where(c => c.Contains(SearchText, StringComparison.OrdinalIgnoreCase));


        TshirtNames = new ObservableCollection<string>(filtered);
    }
}