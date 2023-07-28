using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using WinRT;

namespace CommonHelpers.Maui.Platforms.Windows;

public static class WindowHelpers
{
    public static void TryMicaOrAcrylic(this Microsoft.UI.Xaml.Window window)
    {
        var dispatcherQueueHelper = new WindowsSystemDispatcherQueueHelper(); // in Platforms.Windows folder
        dispatcherQueueHelper.EnsureWindowsSystemDispatcherQueueController();

        // Hooking up the policy object
        var configurationSource = new SystemBackdropConfiguration
        {
            IsInputActive = true
        };

        configurationSource.Theme = ((FrameworkElement)window.Content).ActualTheme switch
        {
            ElementTheme.Dark => SystemBackdropTheme.Dark,
            ElementTheme.Light => SystemBackdropTheme.Light,
            ElementTheme.Default => SystemBackdropTheme.Default,
            _ => configurationSource.Theme
        };

        // Let's try Mica first
        if (MicaController.IsSupported())
        {
            var micaController = new MicaController();
            micaController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
            micaController.SetSystemBackdropConfiguration(configurationSource);

            window.Activated += (sender, args) =>
            {
                if (args.WindowActivationState is WindowActivationState.CodeActivated or WindowActivationState.PointerActivated)
                {
                    // Handle situation where a window is activated and placed on top of other active windows.
                    if (micaController == null)
                    {
                        micaController = new MicaController();
                        micaController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                        micaController.SetSystemBackdropConfiguration(configurationSource);
                    }

                    if (configurationSource != null)
                        configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
                }
            };

            window.Closed += (sender, args) =>
            {
                if (micaController != null)
                {
                    micaController.Dispose();
                    micaController = null;
                }

                configurationSource = null;
            };
        }
        // If no Mica, maybe we can use Acrylic instead
        else if (DesktopAcrylicController.IsSupported())
        {
            var acrylicController = new DesktopAcrylicController();
            acrylicController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
            acrylicController.SetSystemBackdropConfiguration(configurationSource);

            window.Activated += (sender, args) =>
            {
                if (args.WindowActivationState is WindowActivationState.CodeActivated or WindowActivationState.PointerActivated)
                {
                    // Handle situation where a window is activated and placed on top of other active windows.
                    if (acrylicController == null)
                    {
                        acrylicController = new DesktopAcrylicController();
                        acrylicController.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                        acrylicController.SetSystemBackdropConfiguration(configurationSource);
                    }
                }

                if (configurationSource != null)
                    configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
            };

            window.Closed += (sender, args) =>
            {
                if (acrylicController != null)
                {
                    acrylicController.Dispose();
                    acrylicController = null;
                }

                configurationSource = null;
            };
        }
    }
}