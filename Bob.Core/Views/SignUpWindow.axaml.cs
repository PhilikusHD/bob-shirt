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
        Address address = new Address();
        address.Street = StrasseTextbox.Text;
        address.HouseNumber = HausnrTextbox.Text;
        address.City = StadtTextbox.Text;
        address.PostalCode = PlzTextbox.Text;

        int addressId = await AddressService.AddressExists(address);
        await LoginSystem.RegisterCustomerAsync(VornameTextbox.Text, NameTextbox.Text, EmailTextbox.Text, addressId, TelefonTextbox.Text, PasswortTextbox.Text);
    }
}