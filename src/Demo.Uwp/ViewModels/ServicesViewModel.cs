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
        //private readonly BingImageService bingImageService;
        //private readonly XkcdApiService xkcdService;
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
            }

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
            var url = await BingImageService.Current.GetBingImageOfTheDayAsync();

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

        public ObservableRangeCollection<Employee> Employees { get; set; } = new ObservableRangeCollection<Employee>();

        private void LoadSampleData()
        {
            BarSeriesData.AddRange(SampleDataService.Current.GenerateCategoricalData());

            ScatterSeriesData.AddRange(SampleDataService.Current.GenerateScatterPointData());

            LineSeriesData.AddRange(SampleDataService.Current.GenerateDateTimeMinuteData());

            SplineAreaSeriesData.AddRange(SampleDataService.Current.GenerateDateTimeDayData());

            People.AddRange(SampleDataService.Current.GeneratePeopleData(true));

            Products.AddRange(SampleDataService.Current.GenerateProductData());

            Employees.AddRange(SampleDataService.Current.GenerateEmployeeData());
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
                    xkcdComic = await XkcdApiService.Current.GetNewestComicAsync();
                }
                else
                {
                    xkcdComic = await XkcdApiService.Current.GetComicAsync(lastXkcdComicNumber - 1);
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
