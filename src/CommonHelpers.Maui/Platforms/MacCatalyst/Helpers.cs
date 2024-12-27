using CoreGraphics;
using UIKit;

namespace CommonHelpers.Maui.Platforms.MacCatalyst;

public static class Helpers
{
    public static void RestrictWindowMinimumSize(
        this UIWindowScene scene,
        CGSize minimumSize)
    {
        if (scene is { SizeRestrictions: not null })
        {
            scene.SizeRestrictions.MinimumSize = minimumSize;
        }
    }

    public static void RestrictWindowMaximumSize(
        this UIWindowScene scene,
        CGSize maximumSize)
    {
        if (scene is { SizeRestrictions: not null })
        {
            scene.SizeRestrictions.MaximumSize = maximumSize;
        }
    }

    public static void RestrictWindowSize(
        this UIWindowScene scene,
        CGSize minimumSize,
        CGSize maximumSize)
    {
        if (scene is { SizeRestrictions: not null })
        {
            scene.SizeRestrictions.MinimumSize = minimumSize;
            scene.SizeRestrictions.MaximumSize = maximumSize;
        }
    }
}