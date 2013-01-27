using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fitbit.Api.Config;
using Fitbit.Config;
using NUnit.Framework;
using Fitbit;
using Fitbit.Api;
using Fitbit.Models;

namespace Fitbit.IntegrationTests
{
    [TestFixture]
    class AuthenticationTests
    {
        public AuthenticationTests()
        {
            var config = new FitBitConfiguration(Configuration.ConsumerKey,
                                                 Configuration.ConsumerSecret);

            authenticator = new Fitbit.Api.Authenticator(config);

        }

        private Authenticator authenticator;

        /// <summary>
        /// STEP 1
        /// Run this first, it will succeed and then use the outputed URL to get the TempAuthToken and TempAuthVerifier
        /// STEP 2
        /// Place both of these is Configuration.cs in "STEP 2" as the next authentication test depends on these
        /// NOTE: Do this quickly. Your temp token expire within minutes
        /// </summary>
        [Test]
        public void Can_Retrieve_Access_Token_Authorization_Url()
        {
            var authResult = authenticator.GetAuthUrlToken();

            Assert.IsNotNull(authResult);
            Console.Write("authUrl:" + authResult.ClientUrl);           

            Assert.That(authResult.ClientUrl, Is.StringStarting("https://api.fitbit.com/oauth/authorize?oauth_token="));
        }

        /// <summary>
        /// STEP 3
        /// With the the user having approved the test app, and in possession of temp auth token and verifier
        /// Asks Fitbit for the permanent AuthToken and AuthTokenSecret
        /// </summary>
        [Test]
        public void Can_Retrieve_Access_Token_And_Access_Token_Secret()
        {
            AuthCredential authCredential = authenticator.ProcessApprovedAuthCallback(Configuration.TempAuthToken, "123456", Configuration.TempAuthVerifier);

            Assert.IsNotNull(authCredential.AuthToken);
            Console.WriteLine("AuthToken: " + authCredential.AuthToken);

            Assert.IsNotNull(authCredential.AuthTokenSecret);
            Console.WriteLine("AuthTokenSecret: " + authCredential.AuthTokenSecret);

            Assert.IsNotNull(authCredential.UserId); //encoded Fitbit UserId
            Console.WriteLine("Fitbit UserId: " + authCredential.UserId);


        }




    }
}
