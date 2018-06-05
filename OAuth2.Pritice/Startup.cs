using Owin;
using System;
using Microsoft.Owin;
using System.Web.Http;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(OAuth2.Pritice.Startup))]

namespace OAuth2.Pritice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //配置Web API
            ConfigureWebApi();

            //配置OAuth2
            ConfigureAuth(app);
        }
    }
}
