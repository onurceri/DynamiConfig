using StackExchange.Redis;
using System.Collections.Generic;

namespace DynamiConfig.Business.Services.Interfaces
{
    public interface ICacheService
    {
        RedisValue Get(string key);
        void Set(string key, object value);
        bool IsExist(string key);
        void Remove(string key);
        List<string> GetAllKeyList();
    }
}
