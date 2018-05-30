using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2.ChannelElements;
using DotNetOpenAuth.OAuth2.Messages;
using System.Security.Cryptography;

namespace OAuth2.Pritice.AuthorizationHost.Authorization
{
    public class DefaultAuthorizationServerHost : IAuthorizationServerHost
    {
        /// <summary>
        /// NonceStore
        /// </summary>
        public INonceStore NonceStore { get; }

        /// <summary>
        /// Store for CrtptoKey
        /// </summary>
        public ICryptoKeyStore CryptoKeyStore { get; }

        /// <summary>
        /// Configuration for Authorization Server
        /// </summary>
        private readonly AuthorizationServerHostConfiguration configuration;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration">Configuration for Authorization Server</param>
        public DefaultAuthorizationServerHost(AuthorizationServerHostConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            //var userName = string.IsNullOrEmpty(accessRequest.UserName) ? accessRequest.UserName : accessRequest.ClientIdentifier;
            var response = new AutomatedUserAuthorizationCheckResponse(accessRequest, true, "test");
            return response;
        }

        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
        {
            throw new NotImplementedException();
        }

        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
            var accessToken = new AuthorizationServerAccessToken();
            accessToken.Lifetime = configuration.TokenLifetime;//设置Token的有效时间

            //设置加密公钥
            accessToken.ResourceServerEncryptionKey =
                (RSACryptoServiceProvider)configuration.EncryptionCertificate.PublicKey.Key;

            //设置签名私钥
            accessToken.AccessTokenSigningKey = (RSACryptoServiceProvider)configuration.SigningCertificate.PrivateKey;

            //生成Token
            var result = new AccessTokenResult(accessToken);
            return result;
        }

        public IClientDescription GetClient(string clientIdentifier)
        {
            //TODO:
            //在这里需要根据clientIdentifier来校验用户

            return new AuthorizationClient()
            {
                ClientId = clientIdentifier,
                ClientType = ClientType.Public,
                ClientSecret = "1"
            };
        }

        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            return true;
        }
    }
}