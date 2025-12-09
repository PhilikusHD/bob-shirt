using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Domain;
using Bob.Core.Utils;
using Bob.Core.ViewModels;

namespace Bob.Core;

public partial class ProductDetailWindow : UserControl
{
    public ProductDetailWindow()
    {
        InitializeComponent();
    }

    public ProductDetailWindow(Product product) : this()
    {
        DataContext = new ProductDetailWindowViewModel(product);
    }

    private void OnBackButtonClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}
