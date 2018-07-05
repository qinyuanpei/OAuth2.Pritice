using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using System.Configuration;
using OAuth2.Pritice.Providers;

namespace OAuth2.Pritice.Providers
{
    public class RedisStorageProvider
    {
        private readonly BasicRedisClientManager clientManager;

        public RedisStorageProvider()
        {
            var connectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
            clientManager = new BasicRedisClientManager(connectionString);
        }

        public void Save<TEntity>(TEntity entity,TimeSpan expired)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                var model = new RedisStorageModel<TEntity>(entity);
                model.Id = storage.GetNextSequence();
                storage.Store(model);
            }
        }

        public void Save(string key,string value)
        {
            using (var client = clientManager.GetClient())
            {
                client.Set<string>(key, value);
                client.Save();
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
                return storage.GetAll().Select(e => e.Value);
            }
        }
    }
}