using Windows.ApplicationModel.Resources.Core;

namespace SoftwareKobo.Social.Sina.Weibo.Utils
{
    internal static class DeviceHelper
    {
        /// <summary>
        /// 获取程序当前运行环境是否是桌面环境。
        /// </summary>
        internal static bool IsDesktop
        {
            get
            {
                var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
                return qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Desktop";
            }
        }

        /// <summary>
        /// 获取程序当前运行环境是否是移动设备环境。
        /// </summary>
        internal static bool IsMobile
        {
            get
            {
                var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
                return qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile";
            }
        }
    }
}