using MultiShop.Catalog.Dtos.ProductPropertyDtos;

namespace MultiShop.Catalog.Services.ProductPropertyServices
{
    public interface IProductPropertyService
    {
        Task CreateProductPropertyAsync(CreateProductPropertyDto createProductPropertyDto);
        Task UpdateProductPropertyAsync(UpdateProductPropertyDto updateProductPropertyDto);
        Task DeleteProductPropertyAsync(string id);
        Task<List<ResultProductPropertyDto>> GetAllProductPropertyAsync();
        Task<ResultProductPropertyDto> GetByIdProductPropertyAsync(string id);
    }
}
