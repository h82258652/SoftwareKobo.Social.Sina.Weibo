using System;

namespace SoftwareKobo.Social.Sina.Weibo
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class WeiboAttribute : Attribute
    {
        public WeiboAttribute(string appKey, string appSecret, string redirectUri)
        {
            this.AppKey = appKey;
            this.AppSecret = appSecret;
            this.RedirectUri = redirectUri;
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