using CommonHelpers.Common;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using CommonHelpers.Mvvm;
using System;
using System.Net.Http;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

namespace Demo.Uwp.ViewModels
{
    public class ExtensionsViewModel : ViewModelBase
    {
        private double downloadProgress;
        private bool isDownloading;
        private string hexColor;
        private SolidColorBrush rectangleBackground;

        public ExtensionsViewModel()
        {
            DownloadStringCommand = new DelegateCommand(StartDownload);

            HexColor = "#7289da";
        }

        public double DownloadProgress
        {
            get => downloadProgress;
            set => SetProperty(ref downloadProgress, value);
        }

        public bool IsDownloading
        {
            get => isDownloading;
            set => SetProperty(ref isDownloading, value);
        }

        public DelegateCommand DownloadStringCommand { get; set; }

        public string HexColor
        {
            get => hexColor;
            set => SetProperty(ref hexColor, value, onChanged: OnHexChanged);
        }

        public SolidColorBrush RectangleBackground
        {
            get => rectangleBackground;
            set => SetProperty(ref rectangleBackground, value);
        }

        private async void OnHexChanged()
        {
            try
            {
                System.Drawing.Color dColor = ColorExtensions.ConvertHexStringToColor(HexColor);

                RectangleBackground = new SolidColorBrush(Color.FromArgb(dColor.A, dColor.R, dColor.G, dColor.B));
            }
            catch (Exception ex)
            {
                await new MessageDialog($"Please enter a valid hex value (6 or 8 digits, with '#' prefix). \r\n\nError: {ex.Message}", "Invalid").ShowAsync();
            }
        }
        
        private async void StartDownload()
        {
            if (IsDownloading)
                return;

            if (DownloadProgress > 0)
            {
                DownloadProgress = 0;
            }
            
            IsDownloading = true;
            
            var progressReporter = new Progress<DownloadProgressArgs>();

            progressReporter.ProgressChanged += (s,e) =>
            {
                DownloadProgress = e.PercentComplete;
            };

            await new HttpClient().DownloadStringWithProgressAsync("https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt", progressReporter);
            
            IsDownloading = false;
        }
    }
}
