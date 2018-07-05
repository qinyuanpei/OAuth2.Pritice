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
                ApplicationCanDisplayErrors = false,
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),
                TokenEndpointPath = new PathString("/oauth2/token"),
                Provider = new BasicAuthorizationServerProvider(),
                AuthorizationCodeProvider = new AuthorizationCodeProvider(),
                //AccessTokenProvider = new AccessTokenProvider(),
                AccessTokenProvider = new AccessTokenProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthBearerTokens(options);
        }
    }
}
