using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using System.Configuration;
using OAuth2.Pritice.Providers;

namespace OAuth2.Pritice.Providers
{
    public class RedisAccessProvider
    {
        private readonly BasicRedisClientManager clientManager;

        public RedisAccessProvider()
        {
            var connectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
            clientManager = new BasicRedisClientManager(connectionString);
        }

        public void Save<TEntity>(RedisStorageModel<TEntity> model,TimeSpan expired)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                model.Id = storage.GetNextSequence();
                storage.Store(model);
                storage.Save();
            }
        }


        public void Delete<TEntity>(long id)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<TEntity>();
                storage.DeleteById(id);
                storage.Save();
            }
        }

        public IEnumerable<TEntity> All<TEntity>()
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                return storage.GetAll().Select(e => e.Value).ToList();
            }
        }
    }
}