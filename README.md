# CommonHelpers
This is a cross platform **NET Standard 2.0** helper library containing a bunch of helper code that I used to rewrite several times a day while creating sample apps for developers.

By publishing this is as a signed NuGet package I save myself a lot of time, but more importantly, I get to share it with you. Enjoy!

### Releases

| NuGet.org (stable) | Azure Artifacts Feed (preview) |
|-----------|----------------------|
| [![#](https://img.shields.io/nuget/v/CommonHelpers.svg)](https://www.nuget.org/packages/CommonHelpers/) | [![CommonHelpers package in MainFeed feed in Azure Artifacts](https://feeds.dev.azure.com/lance/_apis/public/Packaging/Feeds/a9cb29f3-008d-418f-a057-1c2925dbbaf2/Packages/9452e54a-48d2-409b-8644-3fa7ed784d85/Badge)](https://dev.azure.com/lance/CommonHelpers/_packaging?_a=package&feed=a9cb29f3-008d-418f-a057-1c2925dbbaf2&package=9452e54a-48d2-409b-8644-3fa7ed784d85&preferRelease=true) |
 
### Pipelines


| Branch                           | Status                                   |
|----------------------------------|------------------------------------------|
| dev                              | [![dev](https://dev.azure.com/lance/CommonHelpers/_apis/build/status/CommonHelpers%20-%20Dev)](https://dev.azure.com/lance/CommonHelpers/_build/latest?definitionId=9) |
| main (default)            | [![main](https://dev.azure.com/lance/CommonHelpers/_apis/build/status/CommonHelpers%20-%20Main)](https://dev.azure.com/lance/CommonHelpers/_build/latest?definitionId=10) |
| release | [![Build status](https://dev.azure.com/lance/CommonHelpers/_apis/build/status/CommonHelpers%20-%20Release)](https://dev.azure.com/lance/CommonHelpers/_build/latest?definitionId=4) |

## Features

### Extensions
Extensions for commonly used objects like `string`, `DateTime`, `enum`. There are also some special extensions for `File`, `Exception` and `Color`. Finally, there's a unqiue helper, `HttpClientExtensions` which provides download progress insights and a helper method to POST image data.

### Collections

Special collection types that help in special scenarios (e.g. `ObservableQueue` and `ObservableRangeCollection`).

### Common
This folder has some of the most frequently used classes (i.e. `BindableBase`, `ViewModelBase`, `JsonHelper`).

### MVVM
Platform agnostic MVVM classes like `DelegateCommand` and `RelayCommand`.

### Services
To make testing UI controls easier by quickly providing well formatted data from offline sample data and online API endpoints. This is really useful for quickly testing *Load On Demand* scenarios.

* BingImageService
* ComicVineApiService
* SampleDataService
* XkcdApiService

> **ComicVineService** requires an API Key (it's free). All other services do not require an API key, just new up the class and start using it.

## Examples

### Sample Data Service
This is my most frequently used service. You can easily spin up data for Lists, Charts, DataGrids and more with a single method.

```C#
var sampleDataService = new SampleDataService();

BarSeriesData.AddRange(sampleDataService.GenerateCategoricalData());
ScatterSeriesData.AddRange(sampleDataService.GenerateScatterPointData());
LineSeriesData.AddRange(sampleDataService.GenerateDateTimeMinuteData());
SplineAreaSeriesData.AddRange(sampleDataService.GenerateDateTimeDayData());
People.AddRange(sampleDataService.GeneratePeopleData());
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

* Updating to be included in the Arctic Code Vault

