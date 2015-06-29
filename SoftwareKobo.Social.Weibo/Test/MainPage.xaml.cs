using SoftwareKobo.Social.Weibo;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BtnShareText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WeiboClient client = await WeiboClient.CreateAsync();
                var shareResult = await client.ShareTextAsync(txtShareText.Text);
                if (shareResult.IsSuccess)
                {
                    await new MessageDialog("分享成功").ShowAsync();
                }
                else
                {
                    await new MessageDialog("分享失败，错误码" + shareResult.ErrorCode).ShowAsync();
                }
            }
            catch (ArgumentException)
            {
                await new MessageDialog("请输入分享文本，且不能为空白字符串").ShowAsync();
            }
            catch (AuthorizeException ex)
            {
                if (ex.Result.ResponseStatus == WebAuthenticationStatus.UserCancel)
                {
                    await new MessageDialog("你取消了授权").ShowAsync();
                }
                else if (ex.Result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    await new MessageDialog("网络故障").ShowAsync();
                }
            }
            catch (HttpException ex)
            {
                await new MessageDialog("网络故障：" + ex.ErrorStatus.ToString()).ShowAsync();
            }
        }

        private async void BtnShareImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WeiboClient client = await WeiboClient.CreateAsync();

                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/唱歌1.png"));
                var data = (await FileIO.ReadBufferAsync(file)).ToArray();

                var shareResult = await client.ShareImageAsync(data, txtShareImage.Text);
                if (shareResult.IsSuccess)
                {
                    await new MessageDialog("分享成功").ShowAsync();
                }
                else
                {
                    await new MessageDialog("分享失败，错误码" + shareResult.ErrorCode).ShowAsync();
                }
            }
            catch (ArgumentException)
            {
                await new MessageDialog("请输入图片描述，且不能为空白字符串").ShowAsync();
            }
            catch (AuthorizeException ex)
            {
                if (ex.Result.ResponseStatus == WebAuthenticationStatus.UserCancel)
                {
                    await new MessageDialog("你取消了授权").ShowAsync();
                }
                else if (ex.Result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    await new MessageDialog("网络故障").ShowAsync();
                }
            }
            catch (HttpException ex)
            {
                await new MessageDialog("网络故障：" + ex.ErrorStatus.ToString()).ShowAsync();
            }
        }
    }
}