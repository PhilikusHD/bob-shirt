using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Domain;
using Bob.Core.Services;
using Bob.Core.Systems;
using Bob.Core.Utils;
using Bob.Core.Views;
using System.Threading.Tasks;

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

    private async void SignInButtonClick(object? sender, RoutedEventArgs e)
    {
        if (
            string.IsNullOrWhiteSpace(EmailTextbox.Text) ||
            string.IsNullOrWhiteSpace(PasswortTextbox.Text))
        {
            ErrorTextBlock.Text = "Bitte alle Felder ausfüllen.";
            return;
        }

        ErrorTextBlock.Text = "";


        bool loggedIn = await LoginSystem.ValidateLoginAsync(EmailTextbox.Text, PasswortTextbox.Text);
        if (!loggedIn)
        {
            ErrorTextBlock.Text = "Ungültige E-Mail oder Passwort.";
            return;
        }

        var productVariant = await ProductService.GetVariantAsync(1);
        if (productVariant != null)
            await CartSystem.AddToCart(productVariant);

        ViewManager.TransitionTo(nameof(MainPage));
    }
}