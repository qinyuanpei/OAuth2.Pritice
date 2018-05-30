using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace OAuth2.Pritice.AuthorizationHost.Authorization
{
    public class AuthorizationServerHostConfiguration
    {
        /// <summary>
        /// Certificate for Signing
        /// </summary>
        public X509Certificate2 SigningCertificate { get; set; }

        /// <summary>
        /// Certificate for Encryption
        /// </summary>
        public X509Certificate2 EncryptionCertificate { get; set; }

        /// <summary>
        /// Token Expire Time
        /// </summary>
        public TimeSpan TokenLifetime { get; set; }

        /// <summary>
        /// Default Configuration
        /// </summary>
        public static AuthorizationServerHostConfiguration Default { get; set; }
    }
}