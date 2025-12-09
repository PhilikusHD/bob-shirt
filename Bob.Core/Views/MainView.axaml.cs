using Avalonia.Controls;
using Bob.Core.Utils;

namespace Bob.Core
{
#nullable enable
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            ViewManager.Host = this;
            ViewManager.AddToLookup(nameof(MainPage), new MainPage());
            ViewManager.AddToLookup(nameof(CapsWindow), new CapsWindow());
            ViewManager.AddToLookup(nameof(TShirtWindow), new TShirtWindow());
            ViewManager.AddToLookup(nameof(HoodieWindow), new HoodieWindow());
            ViewManager.AddToLookup(nameof(CartWindow), new CartWindow());
            ViewManager.AddToLookup(nameof(SignInWindow), new SignInWindow());
            ViewManager.AddToLookup(nameof(SignUpWindow), new SignUpWindow());



            ViewManager.TransitionTo(nameof(MainPage));
        }

        public void NavigateTo(UserControl newView)
        {
            CurrentContent.Content = newView;
        }
    }
}
