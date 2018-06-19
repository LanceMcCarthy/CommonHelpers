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
                    return new XkcdComic { Title = "Whoops", Transcript = $"There was no comic to be found" };

                var result = JsonHelper<XkcdComic>.Deserialize(jsonResult);

                return result ?? new XkcdComic { Title = "Json Schmason", Transcript = $"Someone didnt like the way the comic's json tasted and spit it back out" };
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
                    return new XkcdComic { Title = "Whoops", Transcript = $"There was no comic to be found" };

                var result = JsonHelper<XkcdComic>.Deserialize(jsonResult);
                
                return result ?? new XkcdComic { Title = "Json Schmason", Transcript = $"Someone didnt like the way the comic's json tasted and spit it back out" };
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}