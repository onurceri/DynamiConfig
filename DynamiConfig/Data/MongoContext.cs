using MongoDB.Driver;
using System;

namespace DynamiConfig.Data
{
    public sealed class MongoContext
    {
        public MongoContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static string _connectionString;
        private static IMongoDatabase _mongoDatabase;

        public static IMongoDatabase GetMongoDatabase
        {
            get
            {
                if (_mongoDatabase == null)
                {
                    _mongoDatabase = GetDatabase();
                }
                return _mongoDatabase;
            }
        }

        private static IMongoDatabase GetDatabase()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("connectionstring cannot be null");
            }
            MongoClient client = new MongoClient(_connectionString);
            var _databaseName = MongoUrl.Create(_connectionString).DatabaseName;
            if (string.IsNullOrEmpty(_databaseName))
            {
                throw new Exception("databaseName cannot be null");
            }
            return client.GetDatabase(_databaseName);
        }
    }
}
