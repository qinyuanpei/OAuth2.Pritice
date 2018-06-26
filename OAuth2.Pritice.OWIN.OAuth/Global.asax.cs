using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using OAuth2.Pritice;
using System.Data.Entity;
using OAuth2.Pritice.JwtAuthorization.Models;

namespace OAuth2.Pritice
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<IdentityModel>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
