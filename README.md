# SoftwareKobo.Social.Sina.Weibo
Windows 10 新浪微博 SDK

（目前仅有简单的分享功能）

# 使用方法：
## 1、添加微博分享账号信息到你的主项目中。

在你的项目中添加
```
[assembly:Weibo(yourAppKey,yourAppSecret,yourAppRedirectUri)]
```
建议在AssemblyInfo.cs文件中添加

参考此文件：https://github.com/h82258652/SoftwareKobo.Social.Sina.Weibo/blob/master/SoftwareKobo.Social.Weibo/Test/Properties/AssemblyInfo.cs

yourAppRedirectUri 为授权回调页，请到新浪微博开发平台中的高级信息中设置

## 2、在需要的地方创建 WeiboClient

创建 WeiboClient，则会弹出用户授权页（如果本地没有已授权数据）

如需清除本地授权数据，请清空或删除LocalSettings下的名为weibo的ApplicationDataContainer。

可以调用WeiboClient.ClearAuthorize()方法来清除。

## 3、选择分享文本还是分享图片

分享文本 ShareText 方法

分享图片 ShareImage 方法

需要注意 text 参数不能为空白字符串，不能超过 140 字符上限（一个中文一个字符，2个数字或英文算一个字符），否则会抛出 ArgumentException。

另外请调用完毕后检查返回对象的IsSuccess属性和ErrorCode属性。

ErrorCode对应的信息请查看http://open.weibo.com/wiki/Error_code

<div style="color:red;">另外还要注意 ErrorCode 为 21332 的场合，这个状态码在上面微博文档中是没有的。具体情境为：用户本地授权了 App，然后在微博设置中删除了授权。如果返回该状态码的话，建议逻辑是：清空本地授权 access token（参考上面 Part 2 的清除本地授权数据），然后再重新执行授权和分享。</div>

2、3点请注意捕获异常

正确捕获异常和使用请参考https://github.com/h82258652/SoftwareKobo.Social.Sina.Weibo/blob/master/SoftwareKobo.Social.Weibo/Test/MainPage.xaml.cs

### PS：不要问我为什么没有封装微博API的其它功能，本项目主打分享。
### PS2：生产环境中使用请自行负责。
