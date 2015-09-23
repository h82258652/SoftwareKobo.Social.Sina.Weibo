using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftwareKobo.Social.Sina.Weibo.Extensions;
using SoftwareKobo.Social.Sina.Weibo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;

namespace SoftwareKobo.Social.Sina.Weibo
{
    public class WeiboClient
    {
        static WeiboClient()
        {
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
        }

        private WeiboClient()
        {
        }

        /// <summary>
        /// 创建分享对象，若需要，则弹出授权窗口。
        /// </summary>
        /// <returns>微博分享对象。</returns>
        /// <exception cref="AuthorizeException">用户取消授权获取授权过程中网络异常。</exception>
        /// <exception cref="HttpException">获取 AccessToken 过程中网络异常。</exception>
        public static async Task<WeiboClient> CreateAsync()
        {
            WeiboClient client = new WeiboClient();
            if (LocalAccessToken.Useable == false)
            {
                // 未授权或者授权已过期，请求重新授权。
                string authorizeCode = await client.GetAuthorizeCodeAsync();
                await client.Authorize(authorizeCode);
            }
            return client;
        }

        /// <summary>
        /// 获取授权码，将会弹出授权窗口。
        /// </summary>
        /// <returns>授权码。</returns>
        /// <exception cref="AuthorizeException">用户取消授权或者授权过程中网络异常。</exception>
        private async Task<string> GetAuthorizeCodeAsync()
        {
            Uri requestUri = new Uri("https://api.weibo.com/oauth2/authorize");
            requestUri = requestUri.AddOrUpdateQuery("client_id", WeiboSettings.AppKey);
            requestUri = requestUri.AddOrUpdateQuery("redirect_uri", WeiboSettings.RedirectUri);

            if (DeviceHelper.IsDesktop)
            {
                requestUri = requestUri.AddOrUpdateQuery("display", "client");
            }
            else if (DeviceHelper.IsMobile)
            {
                requestUri = requestUri.AddOrUpdateQuery("display", "mobile");
            }

            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, requestUri, new Uri(WeiboSettings.RedirectUri));
            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    string responseUrl = result.ResponseData;
                    string authorizeCode = new Uri(responseUrl).GetQueryParameter("code");
                    return authorizeCode;

                case WebAuthenticationStatus.UserCancel:
                    throw new AuthorizeException("user cancel authorize")
                    {
                        Result = result
                    };

                case WebAuthenticationStatus.ErrorHttp:
                    throw new AuthorizeException("http connection error")
                    {
                        Result = result
                    };

                default:
                    throw new AuthorizeException("unknow error")
                    {
                        Result = result
                    };
            }
        }

        /// <summary>
        /// 使用授权码换取 AccessToken，并保存到缓存中。
        /// </summary>
        /// <param name="authorizeCode"></param>
        /// <returns></returns>
        private async Task Authorize(string authorizeCode)
        {
            Uri uri = new Uri("https://api.weibo.com/oauth2/access_token");

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("client_id", WeiboSettings.AppKey);
            postData.Add("client_secret", WeiboSettings.AppSecret);
            postData.Add("grant_type", "authorization_code");
            postData.Add("code", authorizeCode);
            postData.Add("redirect_uri", WeiboSettings.RedirectUri);

            HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(postData);

            using (HttpClient client = new HttpClient())
            {
                DateTime sendRequestTime = DateTime.Now;

                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsync(uri, content);
                }
                catch (Exception ex)
                {
                    throw new HttpException("network error", ex);
                }
                string json = await response.Content.ReadAsStringAsync();

                JObject accessToken = JsonConvert.DeserializeObject<JObject>(json);
                string token = (string)accessToken["access_token"];
                long expires = (long)accessToken["expires_in"];
                string uid = (string)accessToken["uid"];

                LocalAccessToken.Token = token;
                LocalAccessToken.Uid = uid;
                LocalAccessToken.ExpiresAt = sendRequestTime.ToTimestamp() + expires;
            }
        }


        /// <summary>
        /// 分享文本到微博中。
        /// </summary>
        /// <param name="text">需要分享的文本</param>
        /// <returns>分享结果</returns>
        /// <exception cref="ArgumentNullException">text 为 null</exception>
        /// <exception cref="ArgumentException">text 为空白字符串</exception>
        /// <exception cref="AuthorizeException">授权已过期，并且用户取消重新授权</exception>
        /// <exception cref="HttpException">网络异常</exception>
        /// <remarks>执行完成后请检查对象的 IsSuccess 属性，以获取是否成功</remarks>
        public async Task<Models.Weibo> ShareTextAsync(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("text could not be empty", nameof(text));
            }
            VerifyTextLength(text);

            if (LocalAccessToken.Useable == false)
            {
                string authorizeCode = await this.GetAuthorizeCodeAsync();
                await this.Authorize(authorizeCode);
            }

            Uri uri = new Uri("https://api.weibo.com/2/statuses/update.json");

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("access_token", LocalAccessToken.Token);
            postData.Add("status", text);

            HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(postData);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsync(uri, content);
                }
                catch (Exception ex)
                {
                    throw new HttpException("network error", ex);
                }
                return await response.Content.ReadAsJsonAsync<Models.Weibo>();
            }
        }

        /// <summary>
        /// 分享图片到微博
        /// </summary>
        /// <param name="image">图片数据</param>
        /// <param name="text">图片描述</param>
        /// <returns>分享结果</returns>
        /// <exception cref="ArgumentNullException">image 为 null</exception>
        /// <exception cref="ArgumentNullException">text 为 null</exception>
        /// <exception cref="ArgumentException">text 为空字符串</exception>
        /// <exception cref="AuthorizeException">授权已过期，并且用户取消重新授权</exception>
        /// <exception cref="HttpException">网络异常</exception>
        /// <remarks>执行完成后请检查对象的 IsSuccess 属性，以获取是否成功</remarks>
        public async Task<Models.Weibo> ShareImageAsync(byte[] image, string text)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("text could not be empty", nameof(text));
            }
            VerifyTextLength(text);

            if (LocalAccessToken.Useable == false)
            {
                string authorizeCode = await this.GetAuthorizeCodeAsync();
                await this.Authorize(authorizeCode);
            }

            Uri uri = new Uri("https://upload.api.weibo.com/2/statuses/upload.json");

            HttpBufferContent bufferContent = new HttpBufferContent(image.AsBuffer());

            HttpMultipartFormDataContent content = new HttpMultipartFormDataContent();

            content.Add(new HttpStringContent(LocalAccessToken.Token), "access_token");
            content.Add(new HttpStringContent(text), "status");
            content.Add(bufferContent, "pic", "pic.jpg");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await client.PostAsync(uri, content);
                }
                catch (Exception ex)
                {
                    throw new HttpException("network error", ex);
                }
                return await response.Content.ReadAsJsonAsync<Models.Weibo>();
            }
        }

        private const int MAX_TEXT_LENGTH = 140;

        private static void VerifyTextLength(string text)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            if (gb2312.GetByteCount(text) > MAX_TEXT_LENGTH * 2)
            {
                throw new ArgumentException("text too long", nameof(text));
            }
        }
    }
}
