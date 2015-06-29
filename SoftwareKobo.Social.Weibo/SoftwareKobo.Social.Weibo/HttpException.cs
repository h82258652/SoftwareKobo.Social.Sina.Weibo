using System;
using Windows.Web;

namespace SoftwareKobo.Social.Weibo
{
    public class HttpException : Exception
    {
        public HttpException()
        {
        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public WebErrorStatus ErrorStatus
        {
            get
            {
                return WebError.GetStatus(this.HResult);
            }
        }
    }
}