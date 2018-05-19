using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Extensions;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace DynamiConfig.Data
{
    public class RedisContext : ICacheManager
    {
        private static IDatabase redisContext;
        private static ConnectionMultiplexer connectionMultiplexer;

        private static object syncRoot = new Object();
        private static string _connectionString;

        private static IDatabase RedisClient
        {
            get
            {
                if (redisContext == null)
                {
                    lock (syncRoot)
                    {
                        if (redisContext == null)
                        {
                            ConnectToRedis();
                        }
                    }
                }
                return redisContext;
            }
        }

        private static void ConnectToRedis()
        {
            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { _connectionString },
                ConnectTimeout = int.MaxValue,
                ResponseTimeout = int.MaxValue,
                SyncTimeout = int.MaxValue,
                AllowAdmin = true,
                AbortOnConnectFail = false,
                Proxy = Proxy.None,
                ConnectRetry = 10
            };
            connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            redisContext = connectionMultiplexer.GetDatabase();
        }

        public static void SetConnectionString(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }
        }

        #region Methods
        public bool Set(string key, object value)
        {
            return RedisClient.StringSet(key, value.ToRedisJson().ToString());
        }

        public RedisValue Get(string key)
        {
            return RedisClient.StringGet(key);
        }

        public bool Exists(string key)
        {
            return RedisClient.KeyExists(key);
        }

        public List<string> GetAllKeys()
        {
            List<string> result = new List<string>();
            var allList = connectionMultiplexer.GetEndPoints(true);
            foreach (var item in allList)
            {
                var keyList = connectionMultiplexer.GetServer(item).Keys();

                foreach (var key in keyList)
                {
                    result.Add(key);
                }
            }
            return result;
        }

        public bool Remove(string key)
        {
            return RedisClient.KeyDelete(key);
        } 
        #endregion
    }
}
