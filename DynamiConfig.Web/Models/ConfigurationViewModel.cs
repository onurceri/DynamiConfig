using System.Collections.Generic;

namespace DynamiConfig.Web.Models
{
    public class ConfigurationViewModel
    {
        public List<ConfigModel> ConfigurationList { get; set; }

        public ConfigModel NewConfigModel { get; set; } = new ConfigModel();

        public ConfigModel UpdateConfigModel { get; set; }

        public bool CreateResult { get; set; }

        public string UpdatedObjectId { get; set; }
        public bool UpdateResult { get; set; }

        public string DeletedObjectId { get; set; }
        public bool DeleteResult { get; set; }
    }
}
