using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities.Abstract
{
    public interface IElasticsearchModal
    {
        Guid Id { get; set; }
    }
}
