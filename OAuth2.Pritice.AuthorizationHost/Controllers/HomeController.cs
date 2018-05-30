using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuth2.Pritice.AuthorizationHost.Controllers
{
    public class HomeController : ApiController
    {
        // GET: api/Home
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello, This is a OAuth2 Pritice." };
        }
    }
}
