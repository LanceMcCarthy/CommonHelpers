using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices;
using Windows.System; // For DllImport
using WinRT;

namespace CommonHelpers.Maui.Platforms.Windows;

public static class Helpers
{
    public static void TryMicaOrAcrylic(this Microsoft.UI.Xaml.Window window)
    {
        var dispatcherQueueHelper = new DispatcherQueueHelper(); // Defined below
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

            window.Activated += (s, e) =>
            {
                if (e.WindowActivationState is WindowActivationState.CodeActivated or WindowActivationState.PointerActivated)
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
                    configurationSource.IsInputActive = e.WindowActivationState != WindowActivationState.Deactivated;
            };

            window.Closed += (s, e) =>
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

public class DispatcherQueueHelper
{
    [StructLayout(LayoutKind.Sequential)]
    private struct DispatcherQueueOptions
    {
        internal int dwSize;
        internal int threadType;
        internal int apartmentType;
    }
 
    [DllImport("CoreMessaging.dll")]
    private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

    private object dispatcherQueueController = null;
 
    public void EnsureWindowsSystemDispatcherQueueController()
    {
        if (DispatcherQueue.GetForCurrentThread() != null || dispatcherQueueController != null)
            return;

        DispatcherQueueOptions options;
#pragma warning disable CA2263
        options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
#pragma warning restore CA2263
        options.threadType = 2;    // DQTYPE_THREAD_CURRENT
        options.apartmentType = 2; // DQTAT_COM_STA
 
#pragma warning disable CA1806
        CreateDispatcherQueueController(options, ref dispatcherQueueController);
#pragma warning restore CA1806
    }
}