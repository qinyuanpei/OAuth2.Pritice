using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2.ChannelElements;
using DotNetOpenAuth.OAuth2.Messages;
using Newtonsoft.Json;
using OAuth2.Pritice.JwtAuthorization;

namespace OAuth2.Pritice.AuthorizationHost.Authorization
{
    public class JwtAuthorizationServerHost : IAuthorizationServerHost
    {
        public ICryptoKeyStore CryptoKeyStore { get; }

        public INonceStore NonceStore { get; }

        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            var userName = accessRequest.ClientIdentifier;
            var response = new AutomatedUserAuthorizationCheckResponse(accessRequest, true, userName);
            return response;
        }

        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
        {
            throw new NotImplementedException();
        }

        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
            //var accessToken = new JwtAccessToken();
            //accessToken.ClientId = accessTokenRequestMessage.ClientIdentifier;
            //accessToken.Lifetime = TimeSpan.FromMinutes(10);

            //var result = new AccessTokenResult(accessToken);
            return null;
        }

        public IClientDescription GetClient(string clientIdentifier)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            return true;
        }
    }

    public class JwtAccessToken : AccessToken
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        protected override string Serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                access_token = JwtTokenManager.GenerateToken(ClientId),
                token_type = "jwt",
                expire_in = this.Lifetime
            });
        }
    }

}