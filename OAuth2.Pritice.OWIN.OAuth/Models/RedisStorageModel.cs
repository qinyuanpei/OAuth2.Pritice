using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Pritice.OWIN.OAuth.Models
{
    public class RedisStorageModel<TValue>
    {
        public RedisStorageModel(TValue value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Identity
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// TValue
        /// </summary>
        public TValue Value { get; set; }
    }
}