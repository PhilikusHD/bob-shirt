#nullable enable
using Avalonia.Controls;
using Avalonia.Interactivity;
using Bob.Core.Services;
using Bob.Core.ViewModels;
using Bob.Core.Utils;

namespace Bob.Core;

public partial class TShirtWindow : UserControl
{
    public TShirtWindow()
    {
        InitializeComponent();
        DataContext = new TShirtWindowViewModel();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }

    private void OnItemSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {

    }
}