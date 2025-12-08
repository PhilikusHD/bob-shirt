using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Utils;
using Bob.Core.ViewModels;

namespace Bob.Core;
#nullable enable

public partial class HoodieWindow : UserControl
{

    public HoodieWindow()
    {
        InitializeComponent();
        DataContext = new HoodieWindowViewModel();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }
}