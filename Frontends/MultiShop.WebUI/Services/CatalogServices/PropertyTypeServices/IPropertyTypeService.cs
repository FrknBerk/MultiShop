﻿using MultiShop.DtoLayer.CatalogDtos.PropertyTypeDtos;

namespace MultiShop.WebUI.Services.CatalogServices.PropertyTypeServices
{
    public interface IPropertyTypeService
    {
        Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync();
        Task<UpdatePropertyTypeDto> GetByIdPropertyTypeAsync(string id);
        Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto);
        Task DeletePropertyTypeAsync(string id);
        Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto);
    }
}