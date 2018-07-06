using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Redis;
using System.Configuration;
using OAuth2.Pritice.Providers;
using OAuth2.Pritice.OWIN.OAuth.Models;

namespace OAuth2.Pritice.Providers
{
    public class RedisStorageProvider
    {
        /// <summary>
        /// Redis Client Manager
        /// </summary>
        private readonly BasicRedisClientManager clientManager;

        /// <summary>
        /// Cconstructor
        /// </summary>
        public RedisStorageProvider()
        {
            var connectionString = ConfigurationManager.AppSettings["RedisConnectionString"];
            clientManager = new BasicRedisClientManager(connectionString);
        }

        /// <summary>
        /// Storage object with typeof(TEntity)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">instance of TEntity</param>
        /// <param name="expired">expired time</param>
        public void Set<TEntity>(TEntity entity,TimeSpan expired)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                var model = new RedisStorageModel<TEntity>(entity);
                model.Id = storage.GetNextSequence();
                storage.Store(model);
            }
        }

        /// <summary>
        /// Storage object with typeof(TEntity) by key
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="key">key</param>
        /// <param name="entity">instance of TEntity</param>
        /// <param name="expired">expired time</param>
        public void Set<TEntity>(string key, TEntity entity, TimeSpan expired)
        {
            using (var client = clientManager.GetClient())
            {
                client.Set<TEntity>(key, entity, expired);
                client.Save();
            }
        }

        /// <summary>
        /// Get object with typeof(TEntity) by id
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TEntity Get<TEntity>(long id)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                return storage.GetById(id).Value;
            }
        }

        /// <summary>
        /// Get object with typeof(TEntity) by key
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public TEntity Get<TEntity>(string key)
        {
            using (var client = clientManager.GetClient())
            {
                return client.Get<TEntity>(key);
            }
        }

        /// <summary>
        /// Get all objects with typeof(TEntity)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll<TEntity>()
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<RedisStorageModel<TEntity>>();
                return storage.GetAll().Select(e => e.Value);
            }
        }

        /// <summary>
        /// Delete object with typeof(TEntity) by id
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="id">id</param>
        /// <returns></returns>
        public void Delete<TEntity>(long id)
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<TEntity>();
                storage.DeleteById(id);
            }
        }

        /// <summary>
        /// Get all objects with typeof(TEntity)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <returns></returns>
        public void DeleteAll<TEntity>()
        {
            using (var client = clientManager.GetClient())
            {
                var storage = client.As<TEntity>();
                storage.DeleteAll();
            }
        }
    }
}