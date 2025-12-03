using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Bob.Core;
#nullable enable

public partial class CapsWindow : UserControl
{
    private readonly MainView m_Host;

    public CapsWindow()
    {
        InitializeComponent();
        m_Host = new MainView();
    }

    public CapsWindow(MainView host)
    {
        InitializeComponent();
        m_Host = host;
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        m_Host.NavigateTo(new MainPage(m_Host));
    }
}