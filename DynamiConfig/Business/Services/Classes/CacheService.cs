using System;
using System.Collections.Generic;
using DynamiConfig.Business.Services.Interfaces;
using StackExchange.Redis;

namespace DynamiConfig.Business.Services.Classes
{
    public class CacheService : BaseService, ICacheService
    {
        private IConfigurationDataService _configService;

        public CacheService(IConfigurationDataService configService)
        {
            _configService = configService;
        }
        
        public RedisValue Get(string key)
        {
            RedisValue retVal;

            try
            {
                retVal = CacheManager.Get(key);
            }
            catch (Exception e)
            {
                throw e;
            }

            return retVal;
        }

        public bool IsExist(string key)
        {
            return Get(key).IsNull ? false : true;
        }

        public void Remove(string key)
        {
            CacheManager.Remove(key);
        }

        public void Set(string key, object value)
        {
            try
            {
                CacheManager.Set(key, value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetAllKeyList()
        {
            var result = new List<string>();

            try
            {
                result = CacheManager.GetAllKeys();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}
