using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<ResultProductDto> GetByIdProductAsync(string id);
        Task<List<ResultProductWithCategoryDto>> GetProductcWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId);
        Task<List<ResultProductDto>> SearchProductNameAsync(string productName);
    }
}
