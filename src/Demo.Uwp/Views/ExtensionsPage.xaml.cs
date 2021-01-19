using System;

using Demo.Uwp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Demo.Uwp.Views
{
    public sealed partial class ExtensionsPage : Page
    {
        public ExtensionsViewModel ViewModel { get; } = new ExtensionsViewModel();

        public ExtensionsPage()
        {
            InitializeComponent();
        }
    }
}
