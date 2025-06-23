namespace CommonHelpers.Maui.MarkupExtensions;

[ContentProperty("Source")]
public class ImageUriExtension : IMarkupExtension
{
    public string ImageUri { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return GetImageUri();
    }

    private ImageSource GetImageUri()
    {
        try
        {
            if (string.IsNullOrEmpty(ImageUri))
                return null;

            if (File.Exists(ImageUri))
            {
                return new FileImageSource { File = ImageUri };
            }
            else
            {
                return new UriImageSource { Uri = new Uri(ImageUri) };
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"------ImageUriExtension Exception--------\r\n{ex}");
            return null;
        }
    }
}