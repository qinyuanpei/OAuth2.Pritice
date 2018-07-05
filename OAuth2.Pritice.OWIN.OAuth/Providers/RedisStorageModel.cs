using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.Pritice.Providers
{
    public class RedisStorageModel<T>
    {
        public RedisStorageModel(T value)
        {
            this.Value = value;
        }

        public long Id { get; set; }
        public T Value { get; set; }
    }
}