using Avalonia.Controls;
using Avalonia.Interactivity;
using Bob.Core.Services;
using Bob.Core.ViewModels;
using Bob.Core.Utils;

namespace Bob.Core;

#nullable enable
public partial class TShirtWindow : UserControl
{
    public TShirtWindow(ProductService productService)
    {
        InitializeComponent();

        // Set DataContext to the ViewModel
        DataContext = new TshirtWindowViewModel(productService);
    }
    public TShirtWindow()
    {
        InitializeComponent();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}