using Windows.ApplicationModel.Resources.Core;

namespace SoftwareKobo.Social.Weibo.Utils
{
    public static class DeviceHelper
    {
        public static bool IsDesktop
        {
            get
            {
                var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
                return qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Desktop";
            }
        }

        public static bool IsMobile
        {
            get
            {
                var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
                return qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile";
            }
        }
    }
}