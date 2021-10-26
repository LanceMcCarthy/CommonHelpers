using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
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

            var fileBytes = File.ReadAllBytes(imageFilePath);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);
            return await client.PostAsync(new Uri(apiUrl), multipartContent);
        }

        /// <summary>
        /// Helper method to POST binary image data to an API endpoint that expects the data to be accompanied by a parameter
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFilePath">Valid File path of the image</param>
        /// <param name="apiUrl">The API's http or https endpoint</param>
        /// <param name="parameterName">The name of the parameter the API expects the image data in</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostImageDataAsync(this HttpClient client, string imageFilePath, string apiUrl, string parameterName, CancellationToken token)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "HttpClient was null");

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFilePath == null)
                throw new ArgumentNullException(nameof(imageFilePath), "You must have a valid StorageFile for this method to work");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");
            
            var fileBytes = File.ReadAllBytes(imageFilePath);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);
            return await client.PostAsync(new Uri(apiUrl), multipartContent, token);
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStreamAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the returned Stream
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <returns>Stream content of the GET request result</returns>
        public static async Task<Stream> DownloadStreamWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            client.DefaultRequestHeaders.ExpectContinue = false;

            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            //Important - this makes it possible to rewind and re-read the stream
            await response.Content.LoadIntoBufferAsync();

            //NOTE - This Stream will need to be closed by the caller
            var stream = await response.Content.ReadAsStreamAsync();

            var receivedBytes = 0;
            var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

            while (true)
            {
                var buffer = new byte[4096];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0) break;

                receivedBytes += bytesRead;

                progessReporter.Report(new DownloadProgressArgs(receivedBytes, receivedBytes));

                Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
            }

            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Stand-in replacement for HttpClient.GetStreamAsync that can report download progress.
        /// IMPORTANT - The caller is responsible for disposing the returned Stream
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Stream content of the GET request result</returns>
        public static async Task<Stream> DownloadStreamWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter, CancellationToken token)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");
            
            client.DefaultRequestHeaders.ExpectContinue = false;

            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

            //Important - this makes it possible to rewind and re-read the stream
            await response.Content.LoadIntoBufferAsync();

            //NOTE - This Stream will need to be closed by the caller
            var stream = await response.Content.ReadAsStreamAsync();

            var receivedBytes = 0;
            var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

            while (true)
            {
                var buffer = new byte[4096];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                if (bytesRead == 0) break;

                receivedBytes += bytesRead;

                progessReporter.Report(new DownloadProgressArgs(receivedBytes, receivedBytes));

                Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
            }

            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Makes a POST request to an endpoint with image data that reports upload progress, with cancellation support.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFilePath">Url of where to download the stream from</param>
        /// <param name="apiUrl">Endpoint URL</param>
        /// <param name="parameterName">POST request parameter name</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <returns>String content of the GET request result</returns>
        public static async Task<string> SendImageDataWithDownloadProgressAsync(this HttpClient client, string imageFilePath, string apiUrl, string parameterName, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFilePath == null)
                throw new ArgumentNullException(nameof(imageFilePath), "You must have a valid file path");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            client.DefaultRequestHeaders.ExpectContinue = false;

            var fileBytes = File.ReadAllBytes(imageFilePath);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);

            using (var response = await client.PostAsync(new Uri(apiUrl), multipartContent))
            {
                //Important - this makes it possible to rewind and re-read the stream
                await response.Content.LoadIntoBufferAsync();

                //NOTE - This Stream will need to be closed by the caller
                var stream = await response.Content.ReadAsStreamAsync();

                var receivedBytes = 0;
                var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                while (true)
                {
                    var buffer = new byte[4096];
                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

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
                return await stringContent.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Makes a POST request to an endpoint with image data that reports upload progress, with cancellation support.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="imageFilePath">Url of where to download the stream from</param>
        /// <param name="apiUrl">Endpoint URL</param>
        /// <param name="parameterName">POST request parameter name</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>String content of the GET request result</returns>
        public static async Task<string> SendImageDataWithDownloadProgressAsync(this HttpClient client, string imageFilePath, string apiUrl, string parameterName, IProgress<DownloadProgressArgs> progessReporter, CancellationToken token)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl), "You must set a URL for the API endpoint");

            if (imageFilePath == null)
                throw new ArgumentNullException(nameof(imageFilePath), "You must have a valid file path");

            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException(nameof(parameterName), "You must set a parameter name for the image data");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");
            
            client.DefaultRequestHeaders.ExpectContinue = false;

            var fileBytes = File.ReadAllBytes(imageFilePath);

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(fileBytes), parameterName);

            using (var response = await client.PostAsync(new Uri(apiUrl), multipartContent, token))
            {
                //Important - this makes it possible to rewind and re-read the stream
                await response.Content.LoadIntoBufferAsync();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var receivedBytes = 0;
                    var totalBytes = Convert.ToInt32(response.Content.Headers.ContentLength);

                    while (true)
                    {
                        var buffer = new byte[4096];
                        var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                        if (bytesRead == 0)
                            break;

                        receivedBytes += bytesRead;

                        var args = new DownloadProgressArgs(receivedBytes, receivedBytes);
                        progessReporter.Report(args);

                        Debug.WriteLine($"Progress: {receivedBytes} of {totalBytes} bytes read");
                    }

                    stream.Position = 0;

                    string result;

                    using (var stringContent = new StreamReader(stream))
                    {
                        result = await stringContent.ReadToEndAsync();
                    }

                    return result;
                }
            }
        }

        /// <summary>
        /// Replacement for HttpClient.GetStringAsync that can report download progress.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <returns>String content of the GET request result</returns>
        public static async Task<string> DownloadStringWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");

            using (var stream = await DownloadStreamWithProgressAsync(client, url, progessReporter))
            {
                if (stream == null)
                    return null;

                var stringContent = new StreamReader(stream);
                return await stringContent.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Replacement for HttpClient.GetStringAsync that can report download progress.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="url">Url of where to download the stream from</param>
        /// <param name="progessReporter">Args for reporting progress of the download operation</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>String content of the GET request result</returns>
        public static async Task<string> DownloadStringWithProgressAsync(this HttpClient client, string url, IProgress<DownloadProgressArgs> progessReporter, CancellationToken token)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client), "The HttpClient was null. You must use a valid HttpClient instance to use this extension method.");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url), "You must set a URL for the API endpoint");

            if (progessReporter == null)
                throw new ArgumentNullException(nameof(progessReporter), "ProgressReporter was null");
            
            using (var stream = await DownloadStreamWithProgressAsync(client, url, progessReporter, token))
            {
                if (stream == null)
                    return null;

                var stringContent = new StreamReader(stream);
                return await stringContent.ReadToEndAsync();
            }
        }
    }
}
