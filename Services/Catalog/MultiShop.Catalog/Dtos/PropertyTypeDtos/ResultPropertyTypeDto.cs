using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Dtos.PropertyTypeDtos
{
    public class ResultPropertyTypeDto
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
