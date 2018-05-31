using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace OAuth2.Pritice.ResourceServer.Configuration
{
    public class ResourceServerConfiguration
    {
        /// <summary>
        /// 加密证书
        /// </summary>
        public X509Certificate2 EncryptionCertificate { get; set; }

        /// <summary>
        /// 签名证书
        /// </summary>
        public X509Certificate2 SigningCertificate { get; set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        public static ResourceServerConfiguration Default { get; set; }
    }
}