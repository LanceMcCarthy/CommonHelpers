using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming
namespace CommonHelpers.Services
{
    public enum BingImageResolution { Unspecified, _800x600, _1024x768, _1366x768, _1920x1080, _1920x1200 }

    public class BingImageService : IDisposable
    {
        #region Singleton members

        private static BingImageService _current;

        public static BingImageService Current => _current ?? (_current = new BingImageService());

        #endregion

        #region Instance members

        private readonly HttpClient client;

        public BingImageService()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Gets the Bing Image of The Day
        /// </summary>
        /// <param name="resolution">Sets the resolution of the image. The default is Unspecified, which usually returns 1366x768.</param>
        /// <param name="market">Sets the market to retrieve that image of the day. The default is Worldwide ("ww")</param>
        /// <returns>Image URL</returns>
        public async Task<string> GetBingImageOfTheDayAsync(BingImageResolution resolution = BingImageResolution.Unspecified, string market = "en-ww")
        {
            var request = new Uri($"http://www.bing.com/hpimagearchive.aspx?n=1&mkt={market}");

            var result = await client.GetStringAsync(request);

            var targetElement = resolution == BingImageResolution.Unspecified ? "url" : "urlBase";

            var pathString = XDocument.Parse(result).Descendants(targetElement).First().Value;

            var resolutionString = resolution == BingImageResolution.Unspecified ? "" : $"{resolution}.jpg";

            return $"http://www.bing.com{pathString}{resolutionString}";
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        #endregion
    }
}