using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Bob.Core;

#nullable enable
public partial class TshirtWindow : UserControl
{
    private readonly MainView m_Host;

    public TshirtWindow(MainView host)
    {
        InitializeComponent();
        m_Host = host;
    }

    public TshirtWindow()
    {
        InitializeComponent();
        m_Host = new MainView();
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        m_Host.NavigateTo(new MainPage(m_Host));
    }
}