using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using OAuth2.Pritice.Providers;

using Microsoft.Owin.Security.OAuth;

namespace OAuth2.Pritice
{
    public partial class Startup
    { 
        public void ConfigureAuth(IAppBuilder app)
        {
            var options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                Provider = new OAuthApplicationServerProvider(),
                AccessTokenProvider = new BasicTokenProvider(),
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/auth/account/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30)
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerTokens(options);
        }
    }
}
