using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Utils;

namespace Bob.Core;
#nullable enable

public partial class CapsWindow : UserControl
{

    public CapsWindow()
    {
        InitializeComponent();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}