using Newtonsoft.Json;

namespace SoftwareKobo.Social.Sina.Weibo.Models
{
    [JsonObject]
    public class Visible
    {
        [JsonProperty("type")]
        public int Type
        {
            get;
            set;
        }

        [JsonProperty("list_id")]
        public int ListId
        {
            get;
            set;
        }
    }
}