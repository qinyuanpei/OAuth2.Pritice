using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Pritice.OWIN.OAuth.Models
{
    public class ClientModel
    {
        [Key]
        public int Id { get; set; }
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
