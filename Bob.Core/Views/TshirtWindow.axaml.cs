using Avalonia.Controls;
using Avalonia.Interactivity;
using Bob.Core.Services;
using Bob.Core.ViewModels;
using Bob.Core.Utils;

namespace Bob.Core;

#nullable enable
public partial class TShirtWindow : UserControl
{
    public TShirtWindow()
    {
        InitializeComponent();
        DataContext = new TShirtWindowViewModel(new ProductService(new Repositories.ProductRepository()));
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}