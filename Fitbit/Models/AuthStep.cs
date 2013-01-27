using System;
using Fitbit.Helpers;

namespace Fitbit.Api.Models
{
    [Serializable]
    public class AuthStep
    {
        public string OauthToken { get; set; }
        public string AuthTokenSecret { get; set; }
        public string ClientUrl { get; set; }

        
        public override string ToString()
        {
            return XmlHelper.XmlSerializeToString(this);
        }
    }
}