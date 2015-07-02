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

参考此文件：https://github.com/h82258652/SoftwareKobo.Social.Sina.Weibo/blob/master/SoftwareKobo.Social.Sina.Weibo/Test/Properties/AssemblyInfo.cs

yourAppRedirectUri 为授权回调页，请到新浪微博开发平台中的高级信息中设置

## 2、在需要的地方创建 WeiboClient

创建 WeiboClient，则会弹出用户授权页（如果本地没有已授权数据）

如需清除本地授权数据，请清空或删除LocalSettings下的名为weibo的ApplicationDataContainer。

## 3、选择分享文本还是分享图片

分享文本 ShareText 方法

分享图片 ShareImage 方法

需要注意 text 参数不能为空白字符串，超过140字符上限目前未封装，请调用完毕后检查返回对象的IsSuccess属性和ErrorCode属性。

ErrorCode对应的信息请查看http://open.weibo.com/wiki/Error_code

2、3点请注意捕获异常

正确捕获异常和使用请参考https://github.com/h82258652/SoftwareKobo.Social.Sina.Weibo/blob/master/SoftwareKobo.Social.Sina.Weibo/Test/MainPage.xaml.cs

### PS：不要问我为什么没有封装微博API的其它功能，本项目主打分享。
### PS2：生产环境中使用请自行负责。
