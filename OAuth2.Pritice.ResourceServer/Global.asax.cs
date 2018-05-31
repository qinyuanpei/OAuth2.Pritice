using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using OAuth2.Pritice.ResourceServer.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace OAuth2.Pritice.ResourceServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ResourceServerConfiguration.Default = new ResourceServerConfiguration();
            ResourceServerConfiguration.Default.SigningCertificate =
                new X509Certificate2(System.Web.HttpContext.Current.Server.MapPath("~\\Certificates\\signserver.cer"),"1234567890");
            ResourceServerConfiguration.Default.EncryptionCertificate =
                new X509Certificate2(System.Web.HttpContext.Current.Server.MapPath("~\\Certificates\\signserver.pfx"), "1234567890");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
