using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using OAuth2.Pritice.Models;

namespace OAuth2.Pritice.Providers
{
    public class AccessTokenProvider : IAuthenticationTokenProvider
    {
        private readonly RedisStorageProvider redis = new RedisStorageProvider();
        private readonly TimeSpan expired = TimeSpan.FromMinutes(10);

        public void Create(AuthenticationTokenCreateContext context)
        {
            var tokenModel = new TokenModel();
            tokenModel.Token = context.SerializeTicket();
            tokenModel.CreateTime = DateTime.Now;
            tokenModel.ExpireTine = tokenModel.CreateTime.AddMinutes(30);
            redis.Set<TokenModel>(tokenModel, expired);
            context.SetToken(tokenModel.Token);
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