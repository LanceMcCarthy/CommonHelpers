using CommonHelpers.Common;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using CommonHelpers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using CommonHelpers.Models;

namespace Demo.Uwp.ViewModels
{
    public class ExtensionsViewModel : ViewModelBase
    {
        private double downloadProgress;
        private bool isDownloading;
        private string hexColor;
        private SolidColorBrush rectangleBackground;
        private string selectedEasingFunction;

        public ExtensionsViewModel()
        {
            EaseInDataPoints = new ObservableCollection<ChartDataPoint>();
            EaseOutDataPoints = new ObservableCollection<ChartDataPoint>();
            EaseInOutDataPoints = new ObservableCollection<ChartDataPoint>();

            SelectedEasingFunction = EasingFunctions[0];

            DownloadStringCommand = new DelegateCommand(StartDownload);

            HexColor = "#7289da";
        }

        public ObservableCollection<ChartDataPoint> EaseInDataPoints { get; }

        public ObservableCollection<ChartDataPoint> EaseOutDataPoints { get; }

        public ObservableCollection<ChartDataPoint> EaseInOutDataPoints { get; }

        public List<string> EasingFunctions => new List<string>
        {
            "Back Ease",
            "Bounce Ease",
            "Cubic Ease",
            "Circular Ease",
            "Elastic Ease",
            "Exponential Ease",
            "Linear Ease",
            "Quadratic Ease",
            "Quartic Ease",
            "Quintic Ease",
            "Sine Ease"
        };

        public string SelectedEasingFunction
        {
            get => selectedEasingFunction;
            set => SetProperty(ref selectedEasingFunction, value, onChanged: ApplyEasing);
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

        private void ApplyEasing()
        {
            EaseInDataPoints.Clear();
            EaseOutDataPoints.Clear();
            EaseInOutDataPoints.Clear();

            foreach (var i in Enumerable.Range(0, 100))
            {
                float val = (float)i / 100;
                
                switch (SelectedEasingFunction)
                {
                    case "Back Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.BackEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.BackEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue =val.BackEaseInOut()
                        });
                        break;
                    case "Bounce Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.BounceEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.BounceEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.BounceEaseInOut()
                        });
                        break;
                    case "Cubic Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CubicEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CubicEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CubicEaseInOut()
                        });
                        break;
                    case "Circular Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CircularEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CircularEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.CircularEaseInOut()
                        });
                        break;
                    case "Elastic Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ElasticEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ElasticEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ElasticEaseInOut()
                        });
                        break;
                    case "Exponential Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ExponentialEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ExponentialEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.ExponentialEaseInOut()
                        });
                        break;
                    case "Linear Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.Linear()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.Linear()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.Linear()
                        });
                        break;
                    case "Quadratic Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuadraticEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuadraticEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuadraticEaseInOut()
                        });
                        break;
                    case "Quartic Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuarticEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuarticEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuarticEaseInOut()
                        });
                        break;
                    case "Quintic Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuinticEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuinticEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.QuinticEaseInOut()
                        });
                        break;
                    case "Sine Ease":
                        EaseInDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.SineEaseIn()
                        });
                        EaseOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.SineEaseOut()
                        });
                        EaseInOutDataPoints.Add(new ChartDataPoint
                        {
                            XValue = i,
                            YValue = val.SineEaseInOut()
                        });
                        break;
                }
            }
            
        }
    }
}
