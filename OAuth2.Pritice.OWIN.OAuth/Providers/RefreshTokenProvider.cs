using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace OAuth2.Pritice.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> refreshTokens = new ConcurrentDictionary<string, string>();

        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n"));
            refreshTokens[context.Token] = context.SerializeTicket();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() => Create(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var ticket = string.Empty;
            var result = refreshTokens.TryRemove(context.Token, out ticket);
            if (result) context.DeserializeTicket(ticket);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() => Receive(context));
        }
    }
}