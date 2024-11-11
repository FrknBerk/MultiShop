using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.FavoriteProductDtos
{
    public class CreateFavoriteProductDto
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
