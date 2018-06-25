﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Pritice.JwtAuthorization.Models
{
    public class ClientModel
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Id of Client
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Secret of Client
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 重定向地址
        /// </summary>
        public string RedirectURL { get; set; }
    }
}