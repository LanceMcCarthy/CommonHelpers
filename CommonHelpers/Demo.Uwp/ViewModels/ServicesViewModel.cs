using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using CommonHelpers.Collections;
using CommonHelpers.Common;
using CommonHelpers.Models;
using CommonHelpers.Services;
using CommonHelpers.Services.DataModels;
using Demo.Uwp.Helpers;
using Demo.Uwp.Models;

namespace Demo.Uwp.ViewModels
{
    // Note the ViewModelBase is from CommonHelpers
    public class ServicesViewModel : ViewModelBase
    {
        private readonly BingImageService bingImageService;
        private readonly XkcdApiService xkcdService;
        private readonly SampleDataService sampleDataService;
        private readonly ComicVineApiService comicVineService;

        private Uri bingImageOfTheDayUri;
        
        private int lastXkcdComicNumber;

        private int currentCharactersCount;
        private int totalCharactersCount;
        private bool isCharactersLoadOnDemandActive;
        
        public ServicesViewModel()
        {
            // Not loading the onine API data while in design mode (although this does work in the deisgner in most cases)
            if (!DesignMode.DesignModeEnabled || !DesignMode.DesignMode2Enabled)
            {
                comicVineService = new ComicVineApiService(ApiKeys.ComicVineApiKey, ApiKeys.UniqueUserAgentString);
                Characters = new IncrementalLoadingCollection<Character>((cancellationToken, count) => Task.Run(FetchMoreCharacters, cancellationToken));

                bingImageService = new BingImageService();
                xkcdService = new XkcdApiService();
            }

            sampleDataService = new SampleDataService();
            LoadSampleData();
        }

        #region Bing Image Service Related

        public Uri BingImageOfTheDayUri
        {
            get => bingImageOfTheDayUri;
            set => SetProperty(ref bingImageOfTheDayUri, value);
        }

        public async void GetBingImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var url = await bingImageService.GetBingImageOfTheDayAsync();
            BingImageOfTheDayUri = new Uri(url);
        }

        #endregion

        #region Sample Data Service Related

        public ObservableRangeCollection<ChartDataPoint> BarSeriesData { get; set; } = new ObservableRangeCollection<ChartDataPoint>();

        public ObservableRangeCollection<ChartDataPoint> ScatterSeriesData { get; set; } = new ObservableRangeCollection<ChartDataPoint>();

        public ObservableRangeCollection<ChartDataPoint> LineSeriesData { get; set; } = new ObservableRangeCollection<ChartDataPoint>();

        public ObservableRangeCollection<ChartDataPoint> SplineAreaSeriesData { get; set; } = new ObservableRangeCollection<ChartDataPoint>();

        public ObservableRangeCollection<Person> People { get; set; } = new ObservableRangeCollection<Person>();

        public ObservableRangeCollection<Product> Products { get; set; } = new ObservableRangeCollection<Product>();

        private void LoadSampleData()
        {
            BarSeriesData.AddRange(sampleDataService.GenerateCategoricalData());

            ScatterSeriesData.AddRange(sampleDataService.GenerateScatterPointData());

            LineSeriesData.AddRange(sampleDataService.GenerateDateTimeMinuteData());

            SplineAreaSeriesData.AddRange(sampleDataService.GenerateDateTimeDayData());

            People.AddRange(sampleDataService.GeneratePeopleData(true));

            Products.AddRange(sampleDataService.GenerateProductData());
        }
        
        #endregion

        #region xkcd Related

        public ObservableQueue<XkcdComic> XkcdComics { get; set; } = new ObservableQueue<XkcdComic>();

        public async void LoadXkcdComicButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                XkcdComic xkcdComic;

                if (lastXkcdComicNumber == 0)
                {
                    xkcdComic = await xkcdService.GetNewestComicAsync();
                }
                else
                {
                    xkcdComic = await xkcdService.GetComicAsync(lastXkcdComicNumber - 1);
                }

                lastXkcdComicNumber = xkcdComic.Num;
                
                XkcdComics.Enqueue(xkcdComic);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadXkcdComic Exception\r\n{ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region ComicVine Related

        public IncrementalLoadingCollection<Character> Characters { get; set; }
        
        public int CurrentCharactersCount
        {
            get => currentCharactersCount;
            set => SetProperty(ref currentCharactersCount, value);
        }

        public int TotalCharactersCount
        {
            get => totalCharactersCount;
            set => SetProperty(ref totalCharactersCount, value);
        }

        public bool IsCharactersLoadOnDemandActive
        {
            get => isCharactersLoadOnDemandActive;
            set => SetProperty(ref isCharactersLoadOnDemandActive, value);
        }
        
        private async Task<ObservableCollection<Character>> FetchMoreCharacters()
        {
            try
            {
                await DispatcherExtensions.CallOnMainViewUiThreadAsync(() =>
                {
                    IsCharactersLoadOnDemandActive = true;
                    IsBusyMessage = "Getting Characters from ComicVine...";
                });
                
                var apiResult = await comicVineService.GetCharactersAsync(CurrentCharactersCount);

                if (apiResult != null)
                {
                    await DispatcherExtensions.CallOnMainViewUiThreadAsync(() =>
                    {
                        CurrentCharactersCount = apiResult.Offset + apiResult.NumberOfPageResults;
                        TotalCharactersCount = apiResult.NumberOfTotalResults;
                    });
                    
                    return new ObservableCollection<Character>(apiResult.Results);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"FetchMoreCharacters Exception: {exception}");
            }
            finally
            {
                await DispatcherExtensions.CallOnMainViewUiThreadAsync(() =>
                {
                    IsCharactersLoadOnDemandActive = false;
                    IsBusyMessage = "";
                });
            }

            return null;
        }

        #endregion
    }
}
