using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using OAuth2.Pritice.JwtAuthorization;
using Microsoft.Owin.Security.OAuth;

namespace OAuth2.Pritice.Providers
{
    public class JwtTokenProvider : IAuthenticationTokenProvider
    {

        public void Create(AuthenticationTokenCreateContext context)
        {
            var token = JwtTokenManager.GenerateToken("qinyuanpei");
            context.SetToken(token);
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() => Create(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var principal = JwtTokenManager.ValidateToken(context.Token);
            context.DeserializeTicket(context.Token);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() => Receive(context));
        }
    }
}