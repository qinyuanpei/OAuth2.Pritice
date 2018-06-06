using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace OAuth2.Pritice.Providers
{
    public class AccessTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
            return Task.FromResult(0);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
            return Task.FromResult(0);
        }
    }
}