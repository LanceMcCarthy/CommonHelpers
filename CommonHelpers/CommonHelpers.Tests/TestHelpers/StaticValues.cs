using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelpers.Tests.TestHelpers
{
    public class StaticValues
    {
        /// <summary>
        /// API Key, get your own FREE here: https://comicvine.gamespot.com/api/
        /// Or risk being cut off due to over usage
        /// </summary>
        public static string ComicVineApiKey = "ee9e1a44c8daaa976d6ed1737bb2697b822bcddc";

        /// <summary>
        /// Custom User Agent String
        /// - IMPORTANT -
        /// You'll need to create a unique UA string for your app, otherwise the API will treat you as a bot and return a 403.
        /// As an example, use your app and company name, like: "AwesomeApp_AwesomeSoftwareCompany"
        /// </summary>
        public static string UniqueUserAgentString = "LancelotSoftwareLLC_CommonHelpers_Demo";
    }
}
