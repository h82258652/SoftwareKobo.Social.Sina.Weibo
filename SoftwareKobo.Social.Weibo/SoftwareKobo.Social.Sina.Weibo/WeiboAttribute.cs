using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace SoftwareKobo.Social.Sina.Weibo
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class WeiboAttribute : Attribute
    {
        private static WeiboAttribute _instance;

        public WeiboAttribute(string appKey, string appSecret, string redirectUrl)
        {
            this.AppKey = appKey;
            this.AppSecret = appSecret;
            this.RedirectUrl = redirectUrl;
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

        public string RedirectUrl
        {
            get;
            set;
        }

        internal static WeiboAttribute GetInstance()
        {
            _instance = _instance ?? Application.Current.GetType().GetTypeInfo().Assembly.GetCustomAttribute<WeiboAttribute>();
            if (_instance == null)
            {
                throw new ArgumentNullException("WeiboAttribute is not defined in main assembly.", nameof(_instance));
            }
            return _instance;
        }
    }
}