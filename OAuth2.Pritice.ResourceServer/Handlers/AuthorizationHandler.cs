using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using DotNetOpenAuth.OAuth2;
using OAuth2.Pritice.ResourceServer.Configuration;
using System.Security.Cryptography;

namespace OAuth2.Pritice.ResourceServer.Handlers
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private async Task<IPrincipal> ValidateToken(HttpRequestMessage request, params string[] scopes)
        {
            var signPublicKey = (RSACryptoServiceProvider)ResourceServerConfiguration.Default.SigningCertificate.PublicKey.Key;
            var encryptPrivateKey = (RSACryptoServiceProvider)ResourceServerConfiguration.Default.EncryptionCertificate.PrivateKey;
            var tokenAnalyzer = new StandardAccessTokenAnalyzer(signPublicKey, encryptPrivateKey);
            var resourceServer = new DotNetOpenAuth.OAuth2.ResourceServer(tokenAnalyzer);
            return await resourceServer.GetPrincipalAsync(request, requiredScopes: scopes);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //验证Header中的
            var authorization = request.Headers.Authorization;
            if (authorization!=null && authorization.Scheme == "Bearer")
            {
                try
                {
                    var principal = await ValidateToken(request);
                    if(principal!=null)
                    {
                        HttpContext.Current.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
                catch(Exception ex)
                {
                    return await base.SendAsync(new BadRequestResult(request).Request, cancellationToken);
                }
            }

            return await SendAsync(request, cancellationToken);
        }
    }
}