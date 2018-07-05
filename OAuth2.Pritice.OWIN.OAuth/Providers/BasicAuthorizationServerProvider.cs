using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using OAuth2.Pritice.OWIN.OAuth.Models;

namespace OAuth2.Pritice.Providers
{
    public class BasicAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// client_secret
        /// </summary>
        private const string SECRET_KEY = "client_secret";

        /// <summary>
        /// IdentityModel
        /// </summary>
        private IdentityModel identityContext = new IdentityModel();

        /// <summary>
        /// Redis Provider
        /// </summary>
        private RedisStorageProvider redis = new RedisStorageProvider();

        /// <summary>
        /// 授权服务器对客户端验证逻辑
        /// </summary>
        /// <param name="context">OAuth上下文</param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            var isValidate = context.TryGetBasicCredentials(out clientId, out clientSecret);
            if (!isValidate) context.TryGetFormCredentials(out clientId, out clientSecret);

            if (string.IsNullOrEmpty(clientId))
            {
                context.SetError("invalid client_id", "client_id can not be empty");
                return Task.FromResult(0);
            }

            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set(SECRET_KEY, clientSecret);
            }

            var clientRrpository = identityContext.Clients;
            var client = clientRrpository.FirstOrDefault(e => e.ClientId == clientId);
            if (client != null) context.Validated();
            return Task.FromResult(0);
        }

        /// <summary>
        /// 客户端模式下对客户端授权逻辑
        /// </summary>
        /// <param name="context">OAuth上下文</param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var client_secret = context.OwinContext.Get<string>(SECRET_KEY);

            var clientRrpository = identityContext.Clients;
            var client = clientRrpository.FirstOrDefault(e => e.ClientId == context.ClientId);
            if (client != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,context.ClientId),
                };

                var identity = new ClaimsIdentity(
                    claims,
                    OAuthDefaults.AuthenticationType
                );

                context.Validated(identity);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// 密码模式下对客户端授权逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var username = context.UserName;
            var password = context.Password;

            var userRrpository = identityContext.Users;
            var user = userRrpository.FirstOrDefault(e => e.UserName == context.UserName && e.Password == context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType),
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x))
                );

                context.Validated(identity);
            }

            return Task.FromResult(0);
        }
    }
}