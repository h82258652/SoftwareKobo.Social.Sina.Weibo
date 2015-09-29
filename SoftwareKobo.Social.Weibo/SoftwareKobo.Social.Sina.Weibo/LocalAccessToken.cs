using SoftwareKobo.Social.Sina.Weibo.Utils;
using System;
using Windows.Storage;

namespace SoftwareKobo.Social.Sina.Weibo
{
    internal static class LocalAccessToken
    {
        private static ApplicationDataContainer WeiboDatas
        {
            get
            {
                return ApplicationData.Current.LocalSettings.CreateContainer("weibo", ApplicationDataCreateDisposition.Always);
            }
        }

        internal static string Token
        {
            get
            {
                return (string)WeiboDatas.Values["Token"];
            }
            set
            {
                WeiboDatas.Values["Token"] = value;
            }
        }

        internal static string Uid
        {
            get
            {
                return (string)WeiboDatas.Values["Uid"];
            }
            set
            {
                WeiboDatas.Values["Uid"] = value;
            }
        }

        internal static long ExpiresAt
        {
            get
            {
                return (long)WeiboDatas.Values["ExpiresAt"];
            }
            set
            {
                WeiboDatas.Values["ExpiresAt"] = value;
            }
        }

        /// <summary>
        /// 未授权->false；已授权，但过期->false；已授权，未过期->true
        /// </summary>
        internal static bool Useable
        {
            get
            {
                if (WeiboDatas.Values.ContainsKey("Token") == false)
                {
                    return false;
                }
                if (string.IsNullOrEmpty((string)WeiboDatas.Values["Token"]))
                {
                    return false;
                }
                if (WeiboDatas.Values.ContainsKey("Uid") == false)
                {
                    return false;
                }
                if (string.IsNullOrEmpty((string)WeiboDatas.Values["Uid"]))
                {
                    return false;
                }
                if (WeiboDatas.Values.ContainsKey("ExpiresAt") == false)
                {
                    return false;
                }
                long expiresAt = ExpiresAt;
                if (expiresAt <= 0)
                {
                    return false;
                }

                var expiresTime = DateTimeHelper.FromTimestamp(expiresAt);
                return expiresTime > DateTime.Now;
            }
        }
    }
}