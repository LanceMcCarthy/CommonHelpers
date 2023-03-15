using CoreGraphics;
using UIKit;

namespace CommonHelpers.Maui.Platforms.MacCatalyst;

public static class MacWindowHelpers
{
    public static void RestrictWindowMinimumSize(
        this UIWindowScene scene,
        CGSize minimumSize)
    {
        if (scene is { SizeRestrictions: { } } windowScene)
        {
            windowScene.SizeRestrictions.MinimumSize = minimumSize;
        }
    }

    public static void RestrictWindowMaximumSize(
        this UIWindowScene scene,
        CGSize maximumSize)
    {
        if (scene is { SizeRestrictions: { } } windowScene)
        {
            windowScene.SizeRestrictions.MaximumSize = maximumSize;
        }
    }

    public static void RestrictWindowSize(
        this UIWindowScene scene,
        CGSize minimumSize,
        CGSize maximumSize)
    {
        if (scene is { SizeRestrictions: { } } windowScene)
        {
            windowScene.SizeRestrictions.MinimumSize = minimumSize;
            windowScene.SizeRestrictions.MaximumSize = maximumSize;
        }
    }
}