using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Utils;

namespace Bob.Core
{


#nullable enable
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnUeberUnsClick(object? sender, RoutedEventArgs e)
        {

        }

        private void OnShopClick(object? sender, RoutedEventArgs e)
        {

        }

        private void OnKontaktClick(object? sender, RoutedEventArgs e)
        {

        }

        private void OnHoodieClick(object? sender, RoutedEventArgs e)
        {

            ViewManager.TransitionTo(nameof(HoodieWindow));
        }

        private void OnTshirtClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(TShirtWindow));

        }

        private void OnCapsClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(CapsWindow));
        }
    }
}
