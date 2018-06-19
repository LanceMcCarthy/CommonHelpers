using CommonHelpers.Common;
using CommonHelpers.Services.DataModels;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonHelpers.Services
{
    public class ComicVineApiService : IDisposable
    {
        private readonly HttpClient client;
        private readonly string apiKey;

        /// <summary>
        /// See the API docs for usage, pretty simple. Provide your API key and come up with a unique user agent
        /// </summary>
        /// <param name="apiKey">API Key, get one here for free: https://comicvine.gamespot.com/api/</param>
        /// <param name="uniqueUseAgentString">This needs to be a unique string, identifying the application. For example "MyAppByLanceloSoftware"</param>
        public ComicVineApiService(string apiKey, string uniqueUseAgentString)
        {
            var handler = new HttpClientHandler { AllowAutoRedirect = false };

            client = new HttpClient(handler) { BaseAddress = new Uri("https://comicvine.gamespot.com/api/") };
            client.DefaultRequestHeaders.Add("User-Agent", uniqueUseAgentString);

            this.apiKey = apiKey;
        }

        /// <summary>
        /// Gets a list of characters using the offset for the firts item and the number of items.
        /// Example: GetCharactersAsync(51, 25) would return 25 characters starting at the 51st character in the list
        /// </summary>
        /// <param name="offset">The starting position of the characters to fetch.</param>
        /// <param name="limit">The numer of characters to fetch</param>
        /// <returns>A CharacterResult object that contains the list and other metadata to help with next fetch</returns>
        public async Task<CharactersResult> GetCharactersAsync(int offset, int limit = 25)
        {
            var query = "characters?format=json" +
                        $"&api_key={apiKey}" +
                        $"&offset={offset}" +
                        $"&limit={limit}";

            // This is neccessary because the API does a redirect
            using (var response = await client.GetAsync(query))
            {
                if (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    using (var fallbackResponseMessage = await client.GetAsync(response.Headers.Location))
                    using (var streamResult = await fallbackResponseMessage.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(streamResult))
                    {
                        var jsonResult = await reader.ReadToEndAsync();
                        return JsonHelper<CharactersResult>.Deserialize(jsonResult);
                    }
                }
                else
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonHelper<CharactersResult>.Deserialize(jsonResult);
                }
            }
        }
        
        /// <summary>
        /// Gets a list of videos using the offset for the firts item and the number of items.
        /// Example: GetVideosAsync(51, 25) would return 25 characters starting at the 51st video in the list
        /// </summary>
        /// <param name="offset">The starting position of the videos to fetch.</param>
        /// <param name="limit">The numer of characters to fetch</param>
        /// <returns>A VideoResult object that contains the list and other metadata to help with next fetch</returns>
        public async Task<VideosResult> GetVideosAsync(int offset, int limit = 25)
        {
            var query = "videos?format=json" +
                        $"&api_key={apiKey}" +
                        $"&offset={offset}" +
                        $"&limit={limit}";

            using (var response = await client.GetAsync(query))
            {
                if (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.MovedPermanently)
                {
                    using (var fallbackResponseMessage = await client.GetAsync(response.Headers.Location))
                    using (var streamResult = await fallbackResponseMessage.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(streamResult))
                    {
                        var jsonResult = await reader.ReadToEndAsync();
                        return JsonHelper<VideosResult>.Deserialize(jsonResult);
                    }
                }
                else
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonHelper<VideosResult>.Deserialize(jsonResult);
                }
            }
        }

        /// <summary>
        /// Provides a convenient MemoryStream of an image at the specified URL
        /// </summary>
        /// <param name="url">Url to the image</param>
        /// <returns>MemoryStream</returns>
        public async Task<MemoryStream> GetImageAsync(string url)
        {
            using (var response = await client.GetAsync(url))
            {
                var ms = new MemoryStream();
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                return ms;
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
