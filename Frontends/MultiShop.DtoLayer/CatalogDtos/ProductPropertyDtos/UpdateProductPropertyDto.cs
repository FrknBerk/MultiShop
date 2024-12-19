using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.ProductPropertyDtos
{
    public class UpdateProductPropertyDto
    {
        public string Id { get; set; }
        public string PropertyId { get; set; }
        public string PropertyValue { get; set; }
        public string ProductId { get; set; }
    }
}
