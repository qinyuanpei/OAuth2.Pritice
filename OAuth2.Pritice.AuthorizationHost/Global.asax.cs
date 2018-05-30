using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using OAuth2.Pritice.AuthorizationHost.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace OAuth2.Pritice.AuthorizationHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AuthorizationServerHostConfiguration.Default = new AuthorizationServerHostConfiguration();
            AuthorizationServerHostConfiguration.Default.SigningCertificate =
                new X509Certificate2(System.Web.HttpContext.Current.Server.MapPath("~\\Certificates\\signserver.pfx"),"1234567890");
            AuthorizationServerHostConfiguration.Default.EncryptionCertificate =
                new X509Certificate2(System.Web.HttpContext.Current.Server.MapPath("~\\Certificates\\signserver.cer"), "1234567890");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
