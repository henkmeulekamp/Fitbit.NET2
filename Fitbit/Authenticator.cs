using System;
using System.Collections.Specialized;
using System.Net;
using Fitbit.Api.Config;
using Fitbit.Api.Models;
using Fitbit.Config;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;
using Fitbit.Models;

namespace Fitbit.Api
{
    public class Authenticator
    {
        private readonly FitBitConfiguration _configuration;

        public Authenticator(FitBitConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration", "Configuration is a required parameter.");

            _configuration = configuration;
        }
        /// <summary>
        /// Use this method first to retrieve the url to redirect the user to to allow the url.
        /// Once they are done there, Fitbit will redirect them back to the predetermined completion URL
        /// </summary>
        /// <returns></returns>
        public AuthStep GetAuthUrlToken()
        {
            var client = CreateRestClient();

            var request = new RestRequest(_configuration.RequestTokenUrl, Method.POST);
            IRestResponse response = client.Execute(request);

            if (HttpStatusCode.OK != response.StatusCode)
            {
                throw new Exception(string.Format("Request Token Step Failed: {0}, {1}\n{2}",
                    response.StatusCode, response.StatusDescription, response.Content));
            }

            NameValueCollection qs = HttpUtility.ParseQueryString(response.Content);
            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];

            request = new RestRequest(_configuration.AuthorizeUrl);
            request.AddParameter("oauth_token", oauthToken);
            string url = client.BuildUri(request).ToString();

            return new AuthStep
                       {
                           OauthToken = oauthToken,
                           AuthTokenSecret = oauthTokenSecret,
                           ClientUrl = url  
                       };
        }

        private RestClient CreateRestClient()
        {
            var client = new RestClient(_configuration.BaseUrl)
                             {
                                 Authenticator =
                                     OAuth1Authenticator.ForRequestToken(
                                         _configuration.Consumerkey,
                                         _configuration.ConsumerSecret)
                             };
            return client;
        }

        public AuthCredential ProcessApprovedAuthCallback(string tempAuthToken, string verifier, string tokenSecret)
        {
            var client = CreateRestClient();

            var request = new RestRequest(_configuration.AccessTokenUrl, Method.POST);

            client.Authenticator = OAuth1Authenticator.ForAccessToken(
                _configuration.Consumerkey, _configuration.ConsumerSecret, tempAuthToken, tokenSecret, verifier);

            IRestResponse response = client.Execute(request);

            NameValueCollection qs = HttpUtility.ParseQueryString(response.Content);
            string oauthToken = qs["oauth_token"];
            string oauthTokenSecret = qs["oauth_token_secret"];
            string encodedUserId = qs["encoded_user_id"];

            return new AuthCredential
                       {
                           AuthToken = oauthToken,
                           AuthTokenSecret = oauthTokenSecret,
                           UserId = encodedUserId
                       };
        }


    }
}
