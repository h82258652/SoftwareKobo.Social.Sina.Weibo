using System;

namespace SoftwareKobo.Social.Weibo.Utils
{
    internal static class DateTimeHelper
    {
        internal static long ToTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        internal static DateTime FromTimestamp(long unixTimestamp)
        {
            DateTime utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return utcDateTime.AddSeconds(unixTimestamp).ToLocalTime();
        }
    }
}