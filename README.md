# CommonHelpers
This is a small **NET Standard 2.0** helper library to reuse code that I write several times a day in demo projects.

Nuget: [CommonHelpers on NuGet](https://www.nuget.org/packages/CommonHelpers/)

### Highlights

##### Collections

Special collection types that help in special scenarios (e.g. `ObservableQueue` and `ObservableRangeCollection`).

##### Common
This folder has some of the most frequently used classes (i.e. `BindableBase`, `ViewModelBase`, `JsonHelper`).

##### Extensions
Extensions for commonly used objects like `string`, `DateTime`, `enum`. There are also some special extensions for `File`, `Exception` and `Color`. Finally, there's a unqiue helper, `HttpClientExtensions` which provides download progress and helper method to POST image data.

##### MVVM
Platform agnostic MVVM classes like `DelegateCommand` and `RelayCommand`

##### Services
To make testing UI controls easier by quickly providing well formatted data from offline sample data and online API endpoints. This is really useful for quickly testing *Load On Demand* scenarios.

* BingImageService
* ComicVineApiService**
* SampleDataService
* XkcdApiService


#### Examples

##### Sample Data Service
![Sample Data Service](https://user-images.githubusercontent.com/3520532/41983551-7254db84-79fc-11e8-89b0-347b25054fb3.png)

```C#
var sampleDataService = new SampleDataService();

BarSeriesData.AddRange(sampleDataService.GenerateCategoricalData());
ScatterSeriesData.AddRange(sampleDataService.GenerateScatterPointData());
LineSeriesData.AddRange(sampleDataService.GenerateDateTimeMinuteData());
SplineAreaSeriesData.AddRange(sampleDataService.GenerateDateTimeDayData());
People.AddRange(sampleDataService.GeneratePeopleData());
```
---

##### BingImageService
![Bing ImageService](https://user-images.githubusercontent.com/3520532/41982158-b3ffeea6-79f8-11e8-81a5-abe23142cd75.png)

```C#
using (var bingImageService = new BingImageService())
{
    var result = await bingImageService.GetBingImageOfTheDayAsync();
    image.Source = new UriImageSource{Uri = result};
}
```
---

##### ComicVineApiService
![ComicVine API Service](https://user-images.githubusercontent.com/3520532/41982141-a83cb3e2-79f8-11e8-8207-e6bbbe590d25.png)

```C#
var comicVineService = new ComicVineApiService(ApiKeys.ComicVineApiKey, ApiKeys.UniqueUserAgentString);

var apiResult = await comicVineService.GetCharactersAsync(CurrentCharactersCount);
CurrentCharactersCount = apiResult.Offset + apiResult.NumberOfPageResults;
TotalCharactersCount = apiResult.NumberOfTotalResults;

var characters = apiResult.Results;
```

---


##### xkcd API Service
![xkcd Image Service](https://user-images.githubusercontent.com/3520532/41982114-99259568-79f8-11e8-8eaa-f76695130b55.png)

```C#
var xkcdService = new XkcdApiService();

// to fetch a comic, get the latest or pass a specific comic ID
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

// This is an `ObservableQueue` (located in CommonHelpers.Collections)      
XkcdComics.Enqueue(xkcdComic);

```
---




** *Note: Most services do not require an API key, just new up the class and go. To prevent any confusion, any services that need an API key will require it in the constructor.**
