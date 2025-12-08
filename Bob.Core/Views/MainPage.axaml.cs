using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Database;
using Bob.Core.Repositories;
using Bob.Core.Services;
using Bob.Core.Utils;
using System.Threading.Tasks;


namespace Bob.Core
{


#nullable enable
    public partial class MainPage : UserControl
    {
        private readonly ProductService _productService;
        public MainPage()
        {
            InitializeComponent();
            _productService = new ProductService(new ProductRepository());
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
            var tshirtWindow = new TShirtWindow(_productService);
            ViewManager.TransitionTo(nameof(tshirtWindow));
        }

        private void TestDatabase_Click(object? sender, RoutedEventArgs e)
        {
            TestDB testDatabase = new TestDB();
            testDatabase.RunAsync();
        }

        private void OnCartClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(CartWindow));
        }
    }
}
