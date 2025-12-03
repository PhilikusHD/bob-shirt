using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Bob.Core
{


#nullable enable
    public partial class MainPage : UserControl
    {
        private readonly MainView m_Host;

        public MainPage()
        {
            InitializeComponent();
            m_Host = new MainView();
        }

        public MainPage(MainView host)
        {
            m_Host = host;
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
            m_Host.NavigateTo(new HoodieWindow(m_Host));
        }

        private void OnTshirtClick(object? sender, RoutedEventArgs e)
        {
            m_Host.NavigateTo(new TshirtWindow(m_Host));
        }

        private void OnCapsClick(object? sender, RoutedEventArgs e)
        {
            m_Host.NavigateTo(new CapsWindow(m_Host));
        }
    }
}
