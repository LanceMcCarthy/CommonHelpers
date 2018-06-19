using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CommonHelpers.Common.Args;

namespace CommonHelpers.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Helper method to POST binary image data to an API endpoint that expects the data to be accompanied by a parameter
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFilePath">Valid File path of the image</param>
        /// <param name="apiUrl">The API's http or https endpoint</param>
        /// <param name="parameterName">The name of the parameter the API expects the image data in</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostImageDataAsync(this HttpClient client, string imageFilePath, string apiUrl, string parameterName)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFilePath == null)
                throw new ArgumentNullException(nameof(imageFilePath), "You must have a valid StorageFile for this method to work");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            try
            {
                byte[] fileBytes = File.ReadAllBytes(imageFilePath);

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);
                return await client.PostAsync(new Uri(apiUrl), multipartContent);

            }
            catch (Exception ex)
            {
                throw ex; //do whatever make sense in your app (ie. log it, show it to the user, etc.)
            }
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStreamAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the returned Stream
        /// </summary>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <returns>Stream content of the GET request result</returns>
        public static async Task<Stream> DownloadStreamWithProgressAsync(string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.ExpectContinue = false;

                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                    //Important - this makes it possible to rewind and re-read the stream
                    await response.Content.LoadIntoBufferAsync();

                    //NOTE - This Stream will need to be closed by the caller
                    var stream = await response.Content.ReadAsStreamAsync();

                    int receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    while (true)
                    {
                        var buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        //We're done reading, break out of the loop
                        if (bytesRead == 0) break;

                        receivedBytes += bytesRead;

                        if (progessReporter != null)
                        {
                            var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                            progessReporter.Report(args);
                        }

                        Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                    }

                    stream.Position = 0;
                    return stream;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DownloadStreamWithProgressAsync Exception\r\n{ex}");
                return null;
            }
        }

        public static async Task<string> SendImageDataWithDownloadProgressAsync(string imageFilePath, string apiUrl, string parameterName, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFilePath == null)
                throw new ArgumentNullException(nameof(imageFilePath), "You must have a valid file path");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.ExpectContinue = false;

                byte[] fileBytes = File.ReadAllBytes(imageFilePath);

                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);

                using (var response = await client.PostAsync(new Uri(apiUrl), multipartContent))
                {
                    //Important - this makes it possible to rewind and re-read the stream
                    await response.Content.LoadIntoBufferAsync();

                    //NOTE - This Stream will need to be closed by the caller
                    var stream = await response.Content.ReadAsStreamAsync();

                    int receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    while (true)
                    {
                        var buffer = new byte[4096];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        receivedBytes += bytesRead;

                        var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                        progessReporter.Report(args);

                        Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                    }

                    stream.Position = 0;
                    var stringContent = new StreamReader(stream);
                    return stringContent.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// Stand-in replacement for HttpClient.GetStringAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the Stream object
        /// </summary>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <returns>Stream content of the GET request result</returns>
        public static async Task<string> DownloadStringWithProgressAsync(string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            using (var stream = await DownloadStreamWithProgressAsync(url, progessReporter))
            {
                if (stream == null)
                    return null;

                var stringContent = new StreamReader(stream);
                return stringContent.ReadToEnd();
            }
        }
    }
}
