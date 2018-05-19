using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Data;
using MongoDB.Driver;

namespace DynamiConfig.Business.Services
{
    public class BaseService
    {
        protected IMongoDatabase MongoDbContext { get; set; }
        protected ICacheManager CacheManager { get; set; }

        public BaseService()
        {
            MongoDbContext = MongoContext.GetMongoDatabase;
            CacheManager = new RedisContext();
        }
    }
}
