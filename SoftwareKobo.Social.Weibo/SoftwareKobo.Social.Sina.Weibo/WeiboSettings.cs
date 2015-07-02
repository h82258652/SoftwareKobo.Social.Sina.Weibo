using System.Reflection;
using Windows.UI.Xaml;

namespace SoftwareKobo.Social.Sina.Weibo
{
    public static class WeiboSettings
    {
        static WeiboSettings()
        {
            LoadFromMainProject();
        }

        public static string AppKey
        {
            get;
            set;
        }

        public static string AppSecret
        {
            get;
            set;
        }

        public static string RedirectUri
        {
            get;
            set;
        }

        private static void LoadFromMainProject()
        {
            Assembly mainProjectAssembly = Application.Current.GetType().GetTypeInfo().Assembly;
            WeiboAttribute weiboAttribute = mainProjectAssembly.GetCustomAttribute<WeiboAttribute>();
            if (weiboAttribute != null)
            {
                AppKey = weiboAttribute.AppKey;
                AppSecret = weiboAttribute.AppSecret;
                RedirectUri = weiboAttribute.RedirectUri;
            }
        }
    }
}