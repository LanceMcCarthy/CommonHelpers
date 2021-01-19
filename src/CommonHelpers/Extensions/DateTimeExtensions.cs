using System;
using System.Globalization;

namespace CommonHelpers.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string DateToUnixTimestamp(DateTime date)
        {
            var ts = date - UnixStartDate;
            return ts.TotalSeconds.ToString("0", NumberFormatInfo.InvariantInfo);
        }

        public static DateTime UnixTimestampToDate(string timestamp)
        {
            if (string.IsNullOrEmpty(timestamp)) return DateTime.MinValue;
            try
            {
                return UnixTimestampToDate(long.Parse(timestamp, NumberStyles.Any, NumberFormatInfo.InvariantInfo));
            }
            catch (FormatException)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime UnixTimestampToDate(long timestamp)
        {
            return UnixStartDate.AddSeconds(timestamp);
        }
    }
}
