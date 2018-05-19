using System;
using System.Collections.Generic;
using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Data.DBModels;
using DynamiConfig.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DynamiConfig.Business.Services.Classes
{
    public class ConfigurationDataService : BaseService, IConfigurationDataService
    {
        public List<ConfigurationModel> GetAllActiveConfigurationEntitiesByAppName(string applicationName)
        {
            List<ConfigurationModel> result = null;

            try
            {
                var collection = MongoDbContext.GetCollection<ConfigurationModel>("ConfigValues");
                if (collection != null)
                {
                    var filter = Builders<ConfigurationModel>.Filter.Eq(a => a.IsActive, true);
                    filter = filter & Builders<ConfigurationModel>.Filter.Eq(a => a.ApplicationName, applicationName);
                    result = collection.FindSync(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public ConfigurationModel InsertEntity(ConfigurationModel entity)
        {
            ConfigurationModel result = null;

            try
            {
                var collection = MongoDbContext.GetCollection<ConfigurationModel>("ConfigValues");
                if (collection != null)
                {
                    collection.InsertOne(entity);
                    if (entity.Id != default(ObjectId))
                    {
                        result = entity;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool UpdateEntity(ConfigurationModel entity)
        {
            var result = false;

            try
            {
                var collection = MongoDbContext.GetCollection<ConfigurationModel>("ConfigValues");
                if (collection != null)
                {
                    var filter = Builders<ConfigurationModel>.Filter.Eq("_id", entity.Id);
                    var replaceOneResult = collection.ReplaceOne(filter, entity);
                    result = replaceOneResult.ModifiedCount > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool DeleteEntity(ObjectId objectId)
        {
            var result = false;

            try
            {
                var collection = MongoDbContext.GetCollection<ConfigurationModel>("ConfigValues");
                if (collection != null)
                {
                    var filter = Builders<ConfigurationModel>.Filter.Eq("_id", objectId);
                    DeleteResult deleteResult = collection.DeleteOne(filter);
                    result = deleteResult.DeletedCount > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
