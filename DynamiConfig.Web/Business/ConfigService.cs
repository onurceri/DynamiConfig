using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Data.DBModels;
using DynamiConfig.Web.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace DynamiConfig.Web.Business
{
    public class ConfigService : IConfigService
    {
        public ConfigService(IConfigurationDataService configurationService)
        {
            ConfigurationService = configurationService;
        }

        private IConfigurationDataService ConfigurationService { get; set; }

        public List<ConfigModel> GetActiveConfigList()
        {
            List<ConfigModel> result = null;

            try
            {
                var activeConfigList = ConfigurationService.GetAllActiveConfigurationEntitiesByAppName("SERVICE-A");
                if (activeConfigList != null)
                {
                    result = new List<ConfigModel>();
                    foreach (var config in activeConfigList)
                    {
                        result.Add(new ConfigModel()
                        {
                            Id = config.Id.ToString(),
                            ApplicationName = config.ApplicationName,
                            IsActive = config.IsActive,
                            Name = config.Name,
                            Type = config.Type,
                            EntityValue = config.Value.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool DeleteConfigEntity(string objectId)
        {
            var result = false;

            try
            {
                result = ConfigurationService.DeleteEntity(new ObjectId(objectId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool CreateConfigEntity(ConfigModel entity)
        {
            var result = false;

            try
            {
                var insertedId = ConfigurationService.InsertEntity(new ConfigurationModel() {
                    ApplicationName = entity.ApplicationName,
                    Name = entity.Name,
                    IsActive = true,
                    Type = entity.Type,
                    Value = entity.EntityValue
                });

                if (insertedId != null) result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool UpdateConfigEntity(ConfigModel entity)
        {
            bool result = false;

            try
            {
                ConfigurationService.UpdateEntity(new ConfigurationModel()
                {
                    ApplicationName = entity.ApplicationName,
                    Name = entity.Name,
                    Value = entity.EntityValue,
                    IsActive = entity.IsActive,
                    Type = entity.Type
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
