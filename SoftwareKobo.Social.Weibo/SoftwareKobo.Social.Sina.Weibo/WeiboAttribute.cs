using System;

namespace SoftwareKobo.Social.Sina.Weibo
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class WeiboAttribute : Attribute
    {
        public WeiboAttribute(string appKey, string appSecret, string redirectUri)
        {
            AppKey = appKey;
            AppSecret = appSecret;
            RedirectUri = redirectUri;
        }

        public string AppKey
        {
            get;
            set;
        }

        public string AppSecret
        {
            get;
            set;
        }

        public string RedirectUri
        {
            get;
            set;
        }
    }
}