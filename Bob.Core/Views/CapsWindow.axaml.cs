using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Utils;
using Bob.Core.ViewModels;

namespace Bob.Core;
#nullable enable

public partial class CapsWindow : UserControl
{

    public CapsWindow()
    {
        InitializeComponent();
        DataContext = new CapWindowViewModel();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}