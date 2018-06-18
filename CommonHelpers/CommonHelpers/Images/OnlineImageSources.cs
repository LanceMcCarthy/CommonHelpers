using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonHelpers.Images
{
    public enum BingImageResolution { Unspecified, _800x600, _1024x768, _1366x768, _1920x1080, _1920x1200 }

    public class OnlineImageSources : IDisposable
    {
        private HttpClient client;

        public OnlineImageSources()
        {
            client = new HttpClient();
        }

        public async Task<Uri> GetBingImageOfTheDayAsync(BingImageResolution resolution = BingImageResolution.Unspecified, string market = "en-ww")
        {
            var request = new Uri($"http://www.bing.com/hpimagearchive.aspx?n=1&mkt={market}");

            var result = await client.GetStringAsync(request);

            var targetElement = resolution == BingImageResolution.Unspecified ? "url" : "urlBase";

            var pathString = XDocument.Parse(result).Descendants(targetElement).First().Value;

            var resolutionString = resolution == BingImageResolution.Unspecified ? "" : $"{resolution}.jpg";

            return new Uri($"http://www.bing.com{pathString}{resolutionString}");
        }



        public void Dispose()
        {
            client?.Dispose();
        }
    }
}