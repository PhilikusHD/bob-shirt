using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Utils;
using Bob.Core.Views;

namespace Bob.Core;

#nullable enable
public partial class SignInWindow : UserControl
{
    public SignInWindow()
    {
        InitializeComponent();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }

}