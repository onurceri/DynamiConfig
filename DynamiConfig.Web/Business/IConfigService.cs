using DynamiConfig.Web.Models;
using System.Collections.Generic;

namespace DynamiConfig.Web.Business
{
    public interface IConfigService
    {
        List<ConfigModel> GetActiveConfigList();

        bool DeleteConfigEntity(string objectId);

        bool CreateConfigEntity(ConfigModel entity);

        bool UpdateConfigEntity(ConfigModel entity);
    }
}
