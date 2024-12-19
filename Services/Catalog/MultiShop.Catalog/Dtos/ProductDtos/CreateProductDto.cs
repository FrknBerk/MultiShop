using MongoDB.Bson;
using MultiShop.Catalog.Entities.Abstract;

namespace MultiShop.Catalog.Dtos.ProductDtos
{
    public class CreateProductDto : IElasticsearchModal
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
