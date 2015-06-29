using SoftwareKobo.Social.Weibo.Utils;
using System;
using Windows.Storage;

namespace SoftwareKobo.Social.Weibo
{
    internal static class LocalAccessToken
    {
        private static readonly ApplicationDataContainer _weiboDatas = ApplicationData.Current.LocalSettings.CreateContainer("weibo", ApplicationDataCreateDisposition.Always);

        internal static string Token
        {
            get
            {
                return (string)_weiboDatas.Values["Token"];
            }
            set
            {
                _weiboDatas.Values["Token"] = value;
            }
        }

        internal static string Uid
        {
            get
            {
                return (string)_weiboDatas.Values["Uid"];
            }
            set
            {
                _weiboDatas.Values["Uid"] = value;
            }
        }

        internal static long ExpiresAt
        {
            get
            {
                return (long)_weiboDatas.Values["ExpiresAt"];
            }
            set
            {
                _weiboDatas.Values["ExpiresAt"] = value;
            }
        }

        /// <summary>
        /// 未授权->false；已授权，但过期->false；已授权，未过期->true
        /// </summary>
        internal static bool Useable
        {
            get
            {
                if (_weiboDatas.Values.ContainsKey("Token") == false)
                {
                    return false;
                }
                if (string.IsNullOrEmpty((string)_weiboDatas.Values["Token"]))
                {
                    return false;
                }
                if (_weiboDatas.Values.ContainsKey("Uid") == false)
                {
                    return false;
                }
                if (string.IsNullOrEmpty((string)_weiboDatas.Values["Uid"]))
                {
                    return false;
                }
                if (_weiboDatas.Values.ContainsKey("ExpiresAt") == false)
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