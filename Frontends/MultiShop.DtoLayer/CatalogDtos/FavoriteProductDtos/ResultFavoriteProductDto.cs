﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.CatalogDtos.FavoriteProductDtos
{
    public class ResultFavoriteProductDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
