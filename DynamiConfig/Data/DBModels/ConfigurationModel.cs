using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DynamiConfig.Data.DBModels
{
    public class ConfigurationModel
    {
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Value")]
        public object Value { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }

        [BsonElement("ApplicationName")]
        public string ApplicationName { get; set; }
    }
}
