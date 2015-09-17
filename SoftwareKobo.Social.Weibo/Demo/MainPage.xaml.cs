using SoftwareKobo.Social.Sina.Weibo;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
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
            catch (ArgumentException ex)
            {
                // 空字符串或长度超过 140。
                await new MessageDialog(ex.Message).ShowAsync();
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
            catch (ArgumentException ex)
            {
                // 空字符串或长度超过 140。
                await new MessageDialog(ex.Message).ShowAsync();
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