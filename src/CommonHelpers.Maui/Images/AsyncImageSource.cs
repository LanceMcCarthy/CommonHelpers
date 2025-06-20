#pragma warning disable JMA001

using CommonHelpers.Tasks;

namespace CommonHelpers.Maui.Images;

public static class AsyncImageSource
{
    public static NotifyTaskCompletion<ImageSource> FromUriAndResource(string uri, string resource)
        => FromUriAndResource(new Uri(uri), resource);

    public static NotifyTaskCompletion<ImageSource> FromUriAndResource(Uri uri, string resource) 
        => FromTask(Task.Run(() => ImageSource.FromUri(uri)), ImageSource.FromResource(resource));

    public static NotifyTaskCompletion<ImageSource> FromTask(Task<ImageSource> t, ImageSource s) 
        => new (t, s);
}