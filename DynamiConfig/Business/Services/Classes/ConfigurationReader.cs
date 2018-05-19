using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Data.DBModels;
using DynamiConfig.Extensions;
using DynamiConfig.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DynamiConfig.Business.Services.Classes
{
    public class ConfigurationReader : IConfigurationReader
    {
        public ConfigurationReader(string applicationName, long refreshTimerIntervalInMs = 15000)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentNullException("applicationName argument cannot be null");
            }

            _applicationName = applicationName;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            Task.Factory.StartNew(() => Load());
        }

        #region Props
        private string _applicationName;
        private long _refreshTimerIntervalInMs;
        ManualResetEvent thSafe = new ManualResetEvent(false);
        private TimeSpan? RenewPeriod
        {
            get
            {
                return TimeSpan.FromMilliseconds(_refreshTimerIntervalInMs);
            }
        }
        #endregion

        #region Methods
        private void Load()
        {
            Action internalLoad = () =>
            {
                var entity = new List<ConfigurationModel>();

                try
                {
                    entity = LoadData();
                    thSafe.Reset();
                    foreach (var item in entity)
                    {
                        SetItem(item.Name, item.Value + "&" + item.Type);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    thSafe.Set();
                }
            };

            if (RenewPeriod.HasValue)
            {
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        internalLoad.Invoke();
                        Thread.Sleep(RenewPeriod.Value);
                    }
                });
            }
            else
            {
                Task.Factory.StartNew(internalLoad);
            }
        }

        private List<ConfigurationModel> LoadData()
        {
            return IoC.Resolve<IConfigurationDataService>().GetAllActiveConfigurationEntitiesByAppName(_applicationName);
        }

        private void SetItem(string key, object value)
        {
            IoC.Resolve<ICacheService>().Set(key, value);
        }

        public T GetValue<T>(string key)
        {
            T result = default(T);

            try
            {
                if (key.IsNotNull())
                {
                    var redisValue = IoC.Resolve<ICacheService>().Get(key);
                    if (!redisValue.IsNull)
                    {
                        var redisValStr = redisValue.ToString().Replace("\"", "");
                        var _val = redisValStr.Split("&")[0].Trim();
                        var _valType = redisValStr.Split("&")[1].Trim();
                        string fullTypeName = "System.String";
                        switch (_valType)
                        {
                            case "Int":
                                fullTypeName = "System.Int32";
                                break;
                            case "Boolean":
                                fullTypeName = "System.Booelan";
                                break;
                            default:
                                break;
                        }
                        var value = Convert.ChangeType(_val, Type.GetType(fullTypeName));
                        result = (T)value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        } 
        #endregion
    }
}
