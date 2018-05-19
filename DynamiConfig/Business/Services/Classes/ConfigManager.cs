using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Data.DBModels;

namespace DynamiConfig.Business.Services.Classes
{
    public class ConfigManager : IConfigManager
    {
        private IConfigurationDataService _configurationDataService { get; set; }
        private ICacheService _cacheService { get; set; }
        private List<ConfigurationModel> configValueList { get; set; }
        private static object syncRoot = new Object();

        public ConfigManager(IConfigurationDataService configurationDataService, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _configurationDataService = configurationDataService;
        }

        public void DoCacheWork(string applicationName, long refreshTimerIntervalInMs)
        {
            configValueList = GetConfigValuesFromDatabase(applicationName);
            DeleteAllKeys(configValueList);
            SetAllKeys(configValueList);
        }

        private void SetAllKeys(List<ConfigurationModel> configList)
        {
            if (configValueList != null)
            {
                foreach (var configValue in configValueList)
                {
                    _cacheService.Set($"{configValue.Name}&{configValue.Type}", configValue);
                }
            }
        }

        private void DeleteAllKeys(List<ConfigurationModel> configList)
        {
            if (configValueList != null)
            {
                foreach (var configValue in configList)
                {
                    _cacheService.Remove(configValue.Name);
                }
            }
        }

        public List<string> GetAllKeyListFromCache()
        {
            return _cacheService.GetAllKeyList();
        }

        private List<ConfigurationModel> GetConfigValuesFromDatabase(string applicationName)
        {
            List<ConfigurationModel> configValueList = null;

            try
            {
                configValueList = _configurationDataService.GetAllActiveConfigurationEntitiesByAppName(applicationName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return configValueList;
        }
    }
}
