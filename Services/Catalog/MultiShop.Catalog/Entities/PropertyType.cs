using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
    public class PropertyType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }

    }
}
