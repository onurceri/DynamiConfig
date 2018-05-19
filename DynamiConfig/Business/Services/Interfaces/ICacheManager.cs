using StackExchange.Redis;
using System.Collections.Generic;

namespace DynamiConfig.Business.Services.Interfaces
{
    public interface ICacheManager
    {
        bool Set(string key, object value);
        RedisValue Get(string key);
        bool Exists(string key);
        List<string> GetAllKeys();
        bool Remove(string key);
    }
}
