using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.Social.Weibo.Extensions
{
    internal static class HttpContentExtension
    {
        internal static async Task<T> ReadAsJsonAsync<T>(this IHttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}