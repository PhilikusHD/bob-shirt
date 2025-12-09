using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Bob.Core.ViewModels;

namespace Bob.Core;

public partial class ProductDetailView : UserControl
{
    public static readonly StyledProperty<bool> ShowCartProperty =
    AvaloniaProperty.Register<ProductDetailView, bool>(nameof(ShowCart), true);

    public bool ShowCart
    {
        get => GetValue(ShowCartProperty);
        set => SetValue(ShowCartProperty, value);
    }
    public ProductDetailView()
    {
        InitializeComponent();

    }
}
