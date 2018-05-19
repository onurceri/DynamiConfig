using DynamiConfig.Data.DBModels;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DynamiConfig.Business.Services.Interfaces
{
    public interface IConfigurationDataService
    {
        List<ConfigurationModel> GetAllActiveConfigurationEntitiesByAppName(string serviceName);

        ConfigurationModel InsertEntity(ConfigurationModel entity);

        bool UpdateEntity(ConfigurationModel entity);

        bool DeleteEntity(ObjectId objectId);
    }
}
