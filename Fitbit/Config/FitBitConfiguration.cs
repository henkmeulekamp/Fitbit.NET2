using System;

namespace Fitbit.Api.Config
{
    [Serializable]
    public class FitBitConfiguration
    {
        public FitBitConfiguration()
        {
            SetDefaultUrls();
        }

        private void SetDefaultUrls()
        {
            //setup default values for the urls
            RequestTokenUrl = "/oauth/request_token";
            AccessTokenUrl = "/oauth/access_token";
            AuthorizeUrl = "/oauth/authorize";
            BaseUrl = "https://api.fitbit.com";
        }

        public FitBitConfiguration(string consumerSecret, string consumerkey) : this()
        {
            ConsumerSecret = consumerSecret;
            Consumerkey = consumerkey;
        }

        public string Consumerkey { get; set; }

        public string ConsumerSecret { get; set; }

        /// <summary>
        /// http://api.fitbit.com/oauth/request_token
        /// </summary>
        public string RequestTokenUrl { get; set; }

        /// <summary>
        /// http://api.fitbit.com/oauth/access_token
        /// </summary>
        public string AccessTokenUrl { get; set; }

        /// <summary>
        /// http://www.fitbit.com/oauth/authorize
        /// </summary>
        public string AuthorizeUrl { get; set; }

        /// <summary>
        /// https://api.fitbit.com
        /// </summary>
        public string BaseUrl { get; set; }
             
    }
}
