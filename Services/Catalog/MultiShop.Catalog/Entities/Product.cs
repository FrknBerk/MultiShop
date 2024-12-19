using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MultiShop.Catalog.Entities.Abstract;

namespace MultiShop.Catalog.Entities
{
    public class Product : IElasticsearchModal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }

        public string BrandId { get; set; }
        [BsonIgnore]
        public Brand Brand { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid(); 
    }
}
