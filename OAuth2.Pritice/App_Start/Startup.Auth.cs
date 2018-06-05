using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Infrastructure;
using OAuth2.Pritice.Providers;

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
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
                TokenEndpointPath = new PathString("/oauth2/token"),

                //Authorization Server
                Provider = new BasicAuthorizationServerProvider(),
                
                //Authorization Code
                AuthorizationCodeProvider = new AuthorizationCodeProvider(),
                
                //Access Token
                AccessTokenProvider = new AccessTokenProvider(),
                

                //Refresh Token
                RefreshTokenProvider = new RefreshTokenProvider(),
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthBearerTokens(options);
        }

        #region Access Token

        private void ReceiveAccessToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }

        private void CreateAccessToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
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

        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.FromResult(0);
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.FromResult(0);
        }

        private Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return Task.FromResult(0);
        }

        #endregion
    }
}
