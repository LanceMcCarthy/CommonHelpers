using System;

using Demo.Uwp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo.Uwp.Views
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
        }
    }
}
