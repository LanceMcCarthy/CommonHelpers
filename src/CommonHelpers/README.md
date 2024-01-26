# CommonHelpers

This is a cross platform `netstandard2.0` helper library containing a wide range of helper classes and data generation code that I used to write over and over, every day, while creating sample apps for developers.

Due to the number of "File > New" projects I create every day, this package has saved me a *significant* amount of time (the `SampleDataService` alone is a game changer).

## Releases

| NuGet.org (recommended) | GitHub Releases (all releases) |
|-----------|----------------------|
| [![#](https://img.shields.io/nuget/v/CommonHelpers.svg)](https://www.nuget.org/packages/CommonHelpers/) | [Releases Page](https://github.com/LanceMcCarthy/CommonHelpers/releases/) |

## Features

### Extensions

Helpful extensions for `string`, `DateTime`, and `enum`... as well as common methods for `ObservableCollection`, `File`, `Exception`, `Color` and `HttpClient`. See the [CommonHelpers/Extensions](https://github.com/LanceMcCarthy/CommonHelpers/tree/main/src/CommonHelpers/Extensions) folder for details.

For example, you can quickly get easing function values using the `EasingExtensions`, such as `myFloatValue.BackEaseIn()`:

![easing demo](https://user-images.githubusercontent.com/3520532/163871294-9be04ecd-8dcb-49ca-967b-7f1ff8e091b1.png)

### Collections

Special collection types that help in special scenarios (e.g. `ObservableQueue` and `ObservableRangeCollection`).

### Common

This folder has some of the most used classes. For example, `BindableBase`, `ViewModelBase`, `JsonHelper` to name a few.

### MVVM

Platform agnostic MVVM classes like `DelegateCommand` and `RelayCommand`.

### Services

To make testing UI controls easier by quickly providing well formatted data from offline sample data and online API endpoints. This is really useful for quickly testing *Load On Demand* scenarios.

* `BingImageService`
* `ComicVineApiService`
* `SampleDataService`
* `XkcdApiService`

> The **ComicVineService** requires an API Key, but a key is free. All other services do not require an API key, new-up the class and start using it right away.

## Examples

### Sample Data Service
This is my most frequently used service. You can easily spin up data for Lists, Charts, DataGrids and more with a single method.

```C#
BarSeriesData.AddRange(SampleDataService.Current.GenerateCategoricalData());
ScatterSeriesData.AddRange(SampleDataService.Current.GenerateScatterPointData());
LineSeriesData.AddRange(SampleDataService.Current.GenerateDateTimeMinuteData());
SplineAreaSeriesData.AddRange(SampleDataService.Current.GenerateDateTimeDayData());

People.AddRange(SampleDataService.Current.GeneratePeopleData());
Employees.AddRange(SampleDataService.Current.GenerateEmployeeData(true));

Products.AddRange(SampleDataService.Current.GenerateProductData());
Categories.AddRange(SampleDataService.Current.GenerateCategoryData());
```
![Sample Data Service](https://user-images.githubusercontent.com/3520532/41983551-7254db84-79fc-11e8-89b0-347b25054fb3.png)

### BingImageService

Easily fetch the Bing Image of Day.

```C#
using (var bingImageService = new BingImageService())
{
    var result = await bingImageService.GetBingImageOfTheDayAsync();
    image.Source = new UriImageSource{Uri = result};
}
```
![Bing ImageService](https://user-images.githubusercontent.com/3520532/41982158-b3ffeea6-79f8-11e8-81a5-abe23142cd75.png)

### xkcd API Service

Getting today's comic by calling `GetNewestComic` or get any comic by using `GetComicAsync(comicNumber)`.

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
![xkcd Image Service](https://user-images.githubusercontent.com/3520532/41982114-99259568-79f8-11e8-8eaa-f76695130b55.png)


### ComicVineApiService

The ComicVine API is the only service in which you'll need an API key. You pass the key and a unique string name for your app to the constructor. Now you have access to thousands of items like Characters, Videos and more from the service.

```C#
var comicVineService = new ComicVineApiService(ApiKeys.ComicVineApiKey, ApiKeys.UniqueUserAgentString);

var apiResult = await comicVineService.GetCharactersAsync(CurrentCharactersCount);
CurrentCharactersCount = apiResult.Offset + apiResult.NumberOfPageResults;
TotalCharactersCount = apiResult.NumberOfTotalResults;

var characters = apiResult.Results;
```
![ComicVine API Service](https://user-images.githubusercontent.com/3520532/41982141-a83cb3e2-79f8-11e8-8207-e6bbbe590d25.png)
