#nullable enable
using Avalonia.Controls;
using Avalonia.Interactivity;
using Bob.Core.Domain;
using Bob.Core.Services;
using Bob.Core.Systems;
using Bob.Core.Utils;
using Bob.Core.ViewModels;

namespace Bob.Core;

public partial class CartWindow : UserControl
{
    public CartWindow()
    {
        InitializeComponent();
        DataContext = new CartWindowViewModel();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }

    private void OnWarenkorbLeerenClick(object? sender, RoutedEventArgs e)
    {
        CartSystem.Clear();
        DataContext = new CartWindowViewModel();
    }

    private void OnEntfernenClick(object? sender, RoutedEventArgs e)
    {
        // CartSystem.RemoveFromCart();
        DataContext = new CartWindowViewModel();
    }

    private async void OnBestellenClick(object? sender, RoutedEventArgs e)
    {
        var customer = LoginSystem.GetCurrentCustomer();

        if (customer == null)
        {
            ViewManager.TransitionTo(nameof(SignInWindow));
            return;
        }

        Order order = await CartSystem.CreateOrderDraft(customer.Id);
        int rc = await OrderService.CreateOrderAsync(order);

        if (rc == -1)
        {
            ErrorTextBlock.Text = "Es ist ein Fehler bei der Bestellung aufgetreten.";
        }
    }
}