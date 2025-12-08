using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Domain;
using Bob.Core.Services;
using Bob.Core.Sytems;
using Bob.Core.Utils;

namespace Bob.Core;

#nullable enable
public partial class SignUpWindow : UserControl
{
    public SignUpWindow()
    {
        InitializeComponent();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        ViewManager.TransitionTo(nameof(MainPage));
    }

    private async void SignUpButtonClick(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(VornameTextbox.Text) ||
            string.IsNullOrWhiteSpace(NameTextbox.Text) ||
            string.IsNullOrWhiteSpace(EmailTextbox.Text) ||
            string.IsNullOrWhiteSpace(StrasseTextbox.Text) ||
            string.IsNullOrWhiteSpace(HausnrTextbox.Text) ||
            string.IsNullOrWhiteSpace(StadtTextbox.Text) ||
            string.IsNullOrWhiteSpace(PlzTextbox.Text) ||
            string.IsNullOrWhiteSpace(TelefonTextbox.Text) ||
            string.IsNullOrWhiteSpace(PasswortTextbox.Text))
        {
            ErrorTextBlock.Text = "Bitte alle Felder ausfüllen.";
            return;
        }

        ErrorTextBlock.Text = "";

        Address address = new Address
        {
            Street = StrasseTextbox.Text,
            HouseNumber = HausnrTextbox.Text,
            City = StadtTextbox.Text,
            PostalCode = PlzTextbox.Text
        };

        var newAddress = await AddressService.AddressExists(address);
        address.Id = newAddress.Item2;

        if (!newAddress.Item1)
            await AddressService.AddAddressAsync(address);

        bool exists = await LoginSystem.RegisterCustomerAsync(
            VornameTextbox.Text,
            NameTextbox.Text,
            EmailTextbox.Text,
            address.Id,
            TelefonTextbox.Text,
            PasswortTextbox.Text
        );

        if (exists)
        {
            ErrorTextBlock.Text = "Ein Konto mit dieser E-Mail-Adresse existiert bereits.";
            return;
        }

        VornameTextbox.Text = "";
        NameTextbox.Text = "";
        EmailTextbox.Text = "";
        StrasseTextbox.Text = "";
        HausnrTextbox.Text = "";
        StadtTextbox.Text = "";
        PlzTextbox.Text = "";
        TelefonTextbox.Text = "";
        PasswortTextbox.Text = "";

        ViewManager.TransitionTo(nameof(MainPage));
    }
}