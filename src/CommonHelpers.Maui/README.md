# CommonHelpers.Maui

This is a library containing some commonly needed helpers for .NET MAUI C#/XAML projects. The goal is to build on top of the more popular [CommonHelpers package](https://github.com/LanceMcCarthy/CommonHelpers/tree/main/src/CommonHelpers), but with .NET MAUI-specialized functionality.

| Package | NuGet.org | Features & Docs |
|---------|-----------|-----------------|
| `CommonHelpers.Maui` | [![#](https://img.shields.io/nuget/v/CommonHelpers.Maui.svg)](https://www.nuget.org/packages/CommonHelpers.Maui/) | [README](https://github.com/LanceMcCarthy/CommonHelpers/tree/main/src/CommonHelpers.Maui) |

## Features

### Behaviors

A `BehaviorBase<T>` class that lets you create your own, or use the out-of-the-box `EventToCommandBehavior` implementation. To get started, Add the XML namespace to the view:

```xaml
xmlns:behaviors="clr-namespace:CommonHelpers.Maui.Behaviors;assembly=CommonHelpers.Maui"
```

```xaml
<SomeControl x:Name="MyControl">
    <SomeControl.Behaviors>
        <behaviors:EventToCommandBehavior EventName="Clicked" Command="{Binding MyCommand}">
    </SomeControl.Behaviors>
</SomeControl>
```

### MVVM Helpers

The `ContentPageBase` and `PageViewModelBase` allows you to use view lifecycle events safely in your view model!

Inherit your page from `ContentPageBase` instead of `ContentPage`, like this:

```csharp
public partial class MainPage : ContentPageBase
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}
```

Inherit your viewmodel from the `PageViewModelBase` 

```csharp
public class public class MainPageViewModel : PageViewModelBase
{
}
```

and you can now override any of these view methods:

```csharp
public class public class MainPageViewModel : PageViewModelBase
{
    public override void OnAppearing()
    {
    }

    public override void OnDisappearing()
    {
    }

    public override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
    }

    public override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }

    public override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    public override bool OnBackButtonRequested()
    {
        return base.OnBackButtonRequested();
    }
}
```

### Converters

The library has some commonly needed value converters. To get started, Add the XML namespace to the view:

```xaml
xmlns:converters="clr-namespace:CommonHelpers.Maui.Converters;assembly=CommonHelpers.Maui"
```
Now you can use any of these IValueConverter implementations:

- `IntToDoubleConverter`
- `InvertBoolConverter`
- `NullToBoolConverter`
- `StringToUriConverter`

As well as some boutique ones:

- `ConfigurableBoolConverter`

```xaml
<Grid>
    <Grid.Resources>
        <converters:ConfigurableBoolConverter x:Key="InvertBoolConv"
                                              x:TypeArguments="x:Boolean"
                                              TrueResult="False"
                                              FalseResult="True" />
    </Grid.Resources>

    <Label IsVisible="{Binding HasItems, Converter={StaticResource InvertBoolConv}}"
           Text="No Items!" />
<Grid>
```

- `NameToInitialsConverter`

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:converters="clr-namespace:CommonHelpers.Maui.Converters;assembly=CommonHelpers.Maui"
             x:Class="YourApp.MainPage">
    <Grid>
        <ContentPage.Resources>
            <converters:NameToInitialsConverter x:Key="NameToInitialsConv">
        </ContentPage.Resources>

        <Label Text="{Binding FullName, Converter={StaticResource NameToInitialsConv}}"/>
    </Grid>
</ContentPage>
```

### MauiApp Extensions

Extensions for platform-specific code that can be used in **MauiProgram.cs**.

- For WinUI
  - `window.TryMicaOrAcrylic();`
- For MacCatalyst
  - `windowScene.RestrictWindowMinimumSize(new CGSize(600, 400))`
  - `windowScene.RestrictWindowMaximumSize(new CGSize(1920, 1080))`
  - `windowScene.RestrictWindowSize(new CGSize(600, 400), new CGSize(1920, 1080))`

```csharp
#if WINDOWS10_0_17763_0_OR_GREATER
using CommonHelpers.Maui.Platforms.Windows;

#elif MACCATALYST
using CommonHelpers.Maui.Platforms.MacCatalyst;
using UIKit;
using CoreGraphics;

#endif

public static MauiApp CreateMauiApp()
{
    // ... your other boiler plate startup code here

    builder.ConfigureLifecycleEvents(events =>
    {
    
#if WINDOWS10_0_17763_0_OR_GREATER
        events.AddWindows(wndLifeCycleBuilder =>
        {
            wndLifeCycleBuilder.OnWindowCreated(window =>
            {
                // For automatic Mica or Acrylic support
                window.TryMicaOrAcrylic();
            });
        });
    });
    
#elif MACCATALYST
    events.AddiOS(wndLifeCycleBuilder =>
    {
        wndLifeCycleBuilder.SceneWillConnect((scene, session, options) =>
        {
            if (scene is UIWindowScene windowScene)
            {
                // Can be used for restricting the MacOS window's min, max (or both) size.
                windowScene.RestrictWindowMinimumSize(new CGSize(600, 400));
            }
        });
    });
#endif
    return builder.Build();
}
```

### More to come

This is only the beginning, I plan to build this out as an indispensable companion to the already fantastic `CommonHelpers` library.
