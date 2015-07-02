using Newtonsoft.Json;

namespace SoftwareKobo.Social.Sina.Weibo.Models
{
    [JsonObject]
    public class ShareResultBase
    {
        [JsonProperty("error")]
        public string Error
        {
            get;
            set;
        }

        [JsonProperty("error_code")]
        public int ErrorCode
        {
            get;
            set;
        }

        [JsonProperty("request")]
        public string Request
        {
            get;
            set;
        }
        
        /// <summary>
        /// 分享是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return ErrorCode <= 0;
            }
        }
    }
}