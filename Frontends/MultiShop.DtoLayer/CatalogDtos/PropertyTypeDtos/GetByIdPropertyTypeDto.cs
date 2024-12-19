using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.PropertyTypeDtos
{
    public class GetByIdPropertyTypeDto
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string CategoryId { get; set; }
    }
}
