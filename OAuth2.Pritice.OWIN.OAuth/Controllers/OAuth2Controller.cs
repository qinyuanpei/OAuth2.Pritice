﻿using OAuth2.Pritice.OWIN.OAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OAuth2.Pritice.Providers;
using OAuth2.Pritice.Models;

namespace OAuth2.Pritice.Controllers
{
    public class OAuth2Controller : ApiController
    {
        private IdentityModel identityContext = new IdentityModel();
        private RedisStorageProvider redis = new RedisStorageProvider();

        [HttpPost]
        [Route("oauth2/register")]
        public ClientModel Register(dynamic obj)
        {
            var client = new ClientModel()
            {
                AppName = obj.appName,
                RedirectURL = obj.redirectURL,
                ClientId = Guid.NewGuid().ToString("n"),
                ClientSecret = Guid.NewGuid().ToString("n")
            };

            identityContext.Clients.Add(client);
            identityContext.SaveChanges();
            return client;
        }

        [HttpGet]
        [Route("oauth2/clients")]
        public IEnumerable<ClientModel> GetAllClients()
        {
            return identityContext.Clients.ToList();
        }

        [HttpGet]
        [Route("oauth2/tokens")]
        public IEnumerable<TokenModel> GetAllToken()
        {
            return redis.GetAll<TokenModel>();
        }

        [HttpDelete]
        [Route("oauth2/tokens")]
        public void DeleteAllTokens()
        {
            redis.DeleteAll<TokenModel>();
        }

        [HttpDelete]
        [Route("oauth2/tokens/{id}")]
        public void DeleteToken(long id)
        {
            redis.Delete<TokenModel>(id);
        }
            
    }
}