using OAuth2.Pritice.OWIN.OAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OAuth2.Pritice.Providers;

namespace OAuth2.Pritice.Controllers
{
    public class OAuth2Controller : ApiController
    {
        private IdentityModel identityContext = new IdentityModel();
        private RedisAccessProvider redis = new RedisAccessProvider();

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
        public IEnumerable<ClientModel> All()
        {
            return identityContext.Clients.ToList();
        }

        [HttpGet]
        [Route("oauth2/tokens")]
        public IEnumerable<string> Tokens()
        {
            return redis.All<string>();
        }
            
    }
}