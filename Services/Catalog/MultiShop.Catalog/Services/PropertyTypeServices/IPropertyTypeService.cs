using MultiShop.Catalog.Dtos.PropertyTypeDtos;

namespace MultiShop.Catalog.Services.PropertyTypeServices
{
    public interface IPropertyTypeService
    {
        Task CreatePropertyTypeAsync(CreatePropertyTypeDto createPropertyTypeDto);
        Task UpdatePropertyTypeAsync(UpdatePropertyTypeDto updatePropertyTypeDto);
        Task DeletePropertyTypeAsync(string id);
        Task<List<ResultPropertyTypeDto>> GetAllPropertyTypeAsync();
        Task<UpdatePropertyTypeDto> GetByIdPropertyTypeAsync(string id);
        Task<List<ResultPropertyTypeDto>> GetByCategoryIdPropertyTypeAsync(string categoryId);
    }
}
