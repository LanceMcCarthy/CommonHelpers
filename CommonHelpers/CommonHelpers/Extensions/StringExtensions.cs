using System;

namespace CommonHelpers.Extensions
{
    public static class StringExtensions
    {
        public static string TimeOfDaySalutation()
        {
            var now = DateTime.Now;

            return
                now.Hour < 12 ? "Good morning" :
                now.Hour < 18 ? "Good afternoon" :
                now.Hour < 21 ? "Good evening" :
                /* otherwise */ "Good night";
        }
    }
}