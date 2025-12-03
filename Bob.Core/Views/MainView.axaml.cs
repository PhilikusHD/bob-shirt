using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bob.Core.Views;
using System;

namespace Bob.Core
{
#nullable enable
    public partial class MainView : UserControl
    {

        public MainView()
        {
            InitializeComponent();
            CurrentContent.Content = new MainPage(this);

        }

        public void NavigateTo(UserControl newView)
        {
            CurrentContent.Content = newView;
        }
    }
}
