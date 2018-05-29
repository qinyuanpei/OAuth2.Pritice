using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Pritice.AuthorizationHost.Authorization
{
    public class AuthorizationClient : IClientDescription
    {
        /// <summary>
        /// Id of Client
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// URL of Callback
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// Secret of Client
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Type of Client
        /// </summary>
        public ClientType ClientType { get; set; }
       
        /// <summary>
        /// Default Callback
        /// </summary>
        public Uri DefaultCallback 
        {
            get
            {
                return string.IsNullOrEmpty(Callback) ? null : new Uri(Callback);
            }
        }

        public bool HasNonEmptySecret
        {
            get
            {
                return string.IsNullOrEmpty(ClientSecret);
            }
        }

        public bool IsCallbackAllowed(Uri callback)
        {
            return string.IsNullOrEmpty(this.Callback);
        }

        public bool IsValidClientSecret(string secret)
        {
            return string.IsNullOrEmpty(ClientSecret);
        }
    }
}