using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace OAuth2.Pritice.Providers
{
    public class AuthorizationCodeProvider : IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> authorizationCodes = new ConcurrentDictionary<string, string>();

        public void Create(AuthenticationTokenCreateContext context)
        {
            //构造Authorization Code
            var guid = Guid.NewGuid().ToString("n");
            var stamp = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var code = guid + stamp;

            //设置Authorization Code
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddHours(12);
            context.SetToken(code);

            authorizationCodes[context.Token] = context.SerializeTicket();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() => Create(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var ticket = string.Empty;
            var result = authorizationCodes.TryRemove(context.Token, out ticket);
            if (result) context.DeserializeTicket(ticket);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() => Receive(context));
        }
    }
}