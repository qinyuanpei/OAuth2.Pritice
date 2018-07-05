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
        private readonly RedisAccessProvider redis = new RedisAccessProvider();
        private readonly TimeSpan expired = TimeSpan.FromMinutes(10);

        public void Create(AuthenticationTokenCreateContext context)
        {
            var token = context.SerializeTicket();
            redis.Save<string>(new RedisStorageModel<string>(token), expired);
            context.SetToken(context.SerializeTicket());
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() => Create(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() => Receive(context));
        }
    }
}