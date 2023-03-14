# CommonHelpers.Maui

This is a library containing some commonly needed helpers. The goal is to build on top of the more popular CommonHelpers package, but with .NET MAUI specialized functionality.

#### App Extensions

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

#### Behaviors

A `BehaviorBase<T>` class that lets you create your own, or use the out-of-the-box `EventToCommandBehavior` implementation.

#### Converters

There are several commonly used IValueConverter implementations:

- `IntToDoubleConverter`
- `InvertBoolConverter`
- `NullToBoolConverter`
- `StringToUriConverter`

As well as a couple boutique ones:

- `ConfigurableBoolConverter`
- `NameToInitialsConverter`

#### More to come

This is just the beginning, I plan to build this out as an indispensable comparion to the CommonHelpers library.
