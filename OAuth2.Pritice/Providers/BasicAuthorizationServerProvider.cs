using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace OAuth2.Pritice.Providers
{
    public class BasicAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            var isValidate = context.TryGetBasicCredentials(out clientId, out clientSecret);
            if (!isValidate) context.TryGetFormCredentials(out clientId, out clientSecret);

            if(string.IsNullOrEmpty(context.ClientId))
            {
                context.SetError("invalid client_id", "client_id can not be empty");
                return Task.FromResult(0);
            }

            if(!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("client_secret", clientSecret);
            }

            //Todo: validate client_id via ClientRepository
            context.Validated();
            return Task.FromResult(0);
        }
    }
}