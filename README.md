# CommonHelpers
This is a small **NET Standard 2.0** helper library to reuse code that I write several times a day in demo projects.

Nuget: [CommonHelpers on NuGet](https://www.nuget.org/packages/CommonHelpers/)

### Highlights

#### Collections

Special collection types (e.g. `ObservableQueue` and `ObservableRangeCollection`)

#### Common
This folder has the frequently used base classe (i.e. `BindableBase`, `ViewModelBase`, `JsonHelper`).

#### Models
Common sample data model classes used to populate UI elements (Charts, ListViews, DataGrid, etc).

#### Extensions
Extensions for commonly used objects like `string`, `DateTime`, `enum`. There are also some special extensions for `File`, `Exception` and `Color`. Finally, there's a unqiue helper, `HttpClientExtensions` which provides download progress and helper method to POST image data.

#### MVVM
Platform agnostic MVVM classes like `DelegateCommand` and `RelayCommand`

#### Services
To make testing UI controls with real-life API calls easier, I've added a few of my commonly used web endpoints (e.g. `XkcdApiService`). Most do not require an API key, just new up the service class and go. This is really useful for testing Load On Demand scenarios.

Example: `BingImageService`:

```C#
using (var bingImageService = new BingImageService())
{
    var result = await bingImageService.GetBingImageOfTheDayAsync();
    image.Source = new UriImageSource{Uri = result};
}
```

![image](https://user-images.githubusercontent.com/3520532/41568781-bbd0c9ee-7335-11e8-89a0-92b404ca1aa2.png)