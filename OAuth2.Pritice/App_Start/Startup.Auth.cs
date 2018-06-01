using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using OAuth2.Pritice.Providers;

using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;

namespace OAuth2.Pritice
{
    public partial class Startup
    { 
        public void ConfigureAuth(IAppBuilder app)
        {
            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                ApplicationCanDisplayErrors = true,
                AuthorizeEndpointPath = new PathString("/auth"),
                TokenEndpointPath = new PathString("/auth/token"),

                //Authorization Server
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnValidateClientRedirectUri = ValidateClientRedirectUri,
                    OnValidateClientAuthentication = ValidateClientAuthentication,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                    OnGrantClientCredentials = GrantClientCredetails
                },

                //Authorization Code
                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode
                },

                //Access Token
                AccessTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAccessToken,
                    OnReceive = ReceiveAccessToken
                },

                //Refresh Token
                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken
                }
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerTokens(options);
        }

        #region Access Token

        private void ReceiveAccessToken(AuthenticationTokenReceiveContext obj)
        {
            throw new NotImplementedException();
        }

        private void CreateAccessToken(AuthenticationTokenCreateContext obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Refresh Token

        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext obj)
        {
            throw new NotImplementedException();
        }

        private void CreateRefreshToken(AuthenticationTokenCreateContext obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Authorization Code

        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext obj)
        {
            throw new NotImplementedException();
        }

        private void CreateAuthenticationCode(AuthenticationTokenCreateContext obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Authorization Server

        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext arg)
        {
            throw new NotImplementedException();
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext arg)
        {
            throw new NotImplementedException();
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext arg)
        {
            throw new NotImplementedException();
        }

        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext arg)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
