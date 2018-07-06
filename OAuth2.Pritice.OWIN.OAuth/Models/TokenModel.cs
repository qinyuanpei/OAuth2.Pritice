using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Pritice.Models
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ExpireTine { get; set; }
    }
}