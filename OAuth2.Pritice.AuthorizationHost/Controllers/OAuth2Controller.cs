using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OAuth2.Pritice.AuthorizationHost.Authorization;
using System.Threading.Tasks;

namespace OAuth2.Pritice.AuthorizationHost.Controllers
{
    public class OAuth2Controller : ApiController
    {
        private readonly AuthorizationServer authorizationServer = new AuthorizationServer(
            new DefaultAuthorizationServerHost(
                AuthorizationServerHostConfiguration.Default
            )
        );

        public async Task<HttpResponseMessage> Create()
        {
            var response = await authorizationServer.HandleTokenRequestAsync(Request);
            return response;
        }
    }
}
