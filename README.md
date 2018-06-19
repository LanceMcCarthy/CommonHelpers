# CommonHelpers
This is a small **NET Standard 2.0** helper library to reuse code that I write several times a day in demo projects.

Nuget: [CommonHelpers on NuGet](https://www.nuget.org/packages/CommonHelpers/)

### Highlights

#### Collections

Special collection types that help in special scenarios (e.g. `ObservableQueue` and `ObservableRangeCollection`).

#### Common
This folder has some of the most frequently used classes (i.e. `BindableBase`, `ViewModelBase`, `JsonHelper`).

#### Models
Common sample data model classes used to populate UI elements (Charts, ListViews, DataGrid, etc).

#### Extensions
Extensions for commonly used objects like `string`, `DateTime`, `enum`. There are also some special extensions for `File`, `Exception` and `Color`. Finally, there's a unqiue helper, `HttpClientExtensions` which provides download progress and helper method to POST image data.

#### MVVM
Platform agnostic MVVM classes like `DelegateCommand` and `RelayCommand`

#### Services
To make testing UI controls easier by quickly providing well formatted data from offline sample data and online API endpoints. This is really useful for quickly testing *Load On Demand* scenarios.

* BingImageService
* ComiceVineApiService**
* SampleDataService
* XkcdApiService

** *Note: Most services do not require an API key, just new up the class and go. To prevent any confusion, any services that need an API key will require it in the constructor.**

##### Example 1 - Sample Data Services
`SampleDataService`:
 
```C#
var sampleDataService = new SampleDataService();

listView.ItemsSource = sampleDataService.GeneratePeopleData();
scatterLineSeries.ItemsSource = sampleDataService.GenerateScatterPointData();
barSeries.ItemsSource = sampleDataService.GenerateCategoricalData();
```


##### Example 2 - Online API Services
`BingImageService`:

```C#
using (var bingImageService = new BingImageService())
{
    var result = await bingImageService.GetBingImageOfTheDayAsync();
    image.Source = new UriImageSource{Uri = result};
}
```

![image](https://user-images.githubusercontent.com/3520532/41568781-bbd0c9ee-7335-11e8-89a0-92b404ca1aa2.png)



**Tip:** If you're looking for more public APIs to use for data sources, check out [Todd Motto's Public Apis on GitHub](https://github.com/toddmotto/public-apis). If you see any in there that you'd like me to create a C# service class for? Open an Issue and I'll add it.