using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Domain;
using Bob.Core.Services;
using Bob.Core.Systems;
using Bob.Core.Utils;
using Bob.Core.ViewModels;
using System;

namespace Bob.Core;

#nullable enable
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
    }

    private void OnEntfernenClick(object? sender, RoutedEventArgs e)
    {
        // CartSystem.RemoveFromCart();
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

    private void OnTestClick(object? sender, RoutedEventArgs e)
    {
        DataContext = new CartWindowViewModel();
    }
}