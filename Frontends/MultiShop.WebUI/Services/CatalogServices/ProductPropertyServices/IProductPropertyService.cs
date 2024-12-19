using MultiShop.DtoLayer.CatalogDtos.ProductPropertyDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductPropertyServices
{
    public interface IProductPropertyService
    {
        Task<List<ResultProductPropertyDto>> GetAllProductPropertyAsync();
        Task<ResultProductPropertyDto> GetByPropertyIdProductPropertyAsync(string id);
        Task CreateProductPropertyAsync(CreateProductPropertyDto createProductPropertyDto);
        Task DeleteProductPropertyAsync(string id);
        Task UpdateProductPropertyAsync(UpdateProductPropertyDto updateProductPropertyDto);
    }
}
