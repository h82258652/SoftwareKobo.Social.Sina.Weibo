using System;
using Windows.Security.Authentication.Web;

namespace SoftwareKobo.Social.Weibo
{
    public class AuthorizeException : Exception
    {
        public WebAuthenticationResult Result
        {
            get;
            set;
        }

        public AuthorizeException()
        {
        }

        public AuthorizeException(string message) : base(message)
        {
        }

        public AuthorizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}