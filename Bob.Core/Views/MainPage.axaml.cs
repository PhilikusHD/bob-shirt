using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Bob.Core.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Bob.Core.Database;
using Bob.Core.Repositories;
using Bob.Core.Services;
using Bob.Core.Systems;
using Bob.Core.Utils;
using System;
using System.Threading.Tasks;


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

        private async void TestDatabase_Click(object? sender, RoutedEventArgs e)
        {
            TestDB testDatabase = new TestDB();
            await testDatabase.RunAsync();
        }

        private void OnCartClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(CartWindow));
        }

        private void OnSignInClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(SignInWindow));
        }
        private void OnSignUpClick(object? sender, RoutedEventArgs e)
        {
            ViewManager.TransitionTo(nameof(SignUpWindow));
        }

        public void UpdateLoginDisplay()
        {
            if (LoginSystem.CurrentUserIsLoggedIn())
            {
                var customer = LoginSystem.GetCurrentCustomer();
                string email = customer?.Email ?? "Benutzer";

                SignInButton.Content = new TextBlock
                {
                    Text = email,
                    Foreground = Brushes.Black,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(8, 4), // gives some padding inside the button
                };

                SignUpButton.IsVisible = false;
            }
            else
            {
                SignInButton.Content = new Image
                {
                    Source = new Bitmap("assets/sign_in.png"),
                    Stretch = Stretch.Uniform
                };
                SignUpButton.IsVisible = true;
            }
        }

    }
}
