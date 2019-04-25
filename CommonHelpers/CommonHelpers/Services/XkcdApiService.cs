using CommonHelpers.Services.DataModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommonHelpers.Common;

namespace CommonHelpers.Services
{
    /// <summary>
    /// A fast and easy way to test online image fetching.
    /// </summary>
    public class XkcdApiService : IDisposable
    {
        #region Singleton Members

        private static XkcdApiService _current;

        public static XkcdApiService Current => _current ?? (_current = new XkcdApiService());

        #endregion

        #region Instance Members

        private readonly HttpClient client;

        public XkcdApiService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://xkcd.com/")
            };
        }

        /// <summary>
        /// Gets a specific comic from XKCD, including Gardens
        /// </summary>
        /// <param name="comicNumber">Comic number</param>
        /// <returns></returns>
        public async Task<XkcdComic> GetComicAsync(int comicNumber)
        {
            using (var response = await client.GetAsync($"{comicNumber}/info.0.json", HttpCompletionOption.ResponseContentRead))
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonResult))
                    return new XkcdComic { Title = "No Result", Transcript = $"There was no comic available for comic #${comicNumber} or the service has changed. If this continues to happen, please open an Issue on GitHub at http://bit.ly/CommonHelpers." };

                var result = JsonHelper<XkcdComic>.Deserialize(jsonResult);

                return result ?? new XkcdComic { Title = "Bad Result", Transcript = "The returned comic data could not be deserialized properly. If this continues to happen, please open an Issue on GitHub at http://bit.ly/CommonHelpers" };
            }
        }

        /// <summary>
        /// Gets latest comic available, including Gardens
        /// </summary>
        /// <returns></returns>
        public async Task<XkcdComic> GetNewestComicAsync()
        {
            using (var response = await client.GetAsync("info.0.json", HttpCompletionOption.ResponseContentRead))
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonResult))
                    return new XkcdComic { Title = "No Result", Transcript = $"There is no latest comic available or the service has changed. If this continues to happen, please open an Issue on GitHub at http://bit.ly/CommonHelpers." };

                var result = JsonHelper<XkcdComic>.Deserialize(jsonResult);

                return result ?? new XkcdComic { Title = "Bad Result", Transcript = "The returned comic data could not be deserialized properly. If this continues to happen, please open an Issue on GitHub at http://bit.ly/CommonHelpers." };
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }

        #endregion
    }
}