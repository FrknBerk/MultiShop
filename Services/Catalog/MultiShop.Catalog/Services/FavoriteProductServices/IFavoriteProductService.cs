using MultiShop.Catalog.Dtos.FavoriteProductDtos;

namespace MultiShop.Catalog.Services.FavoriteProductServices
{
    public interface IFavoriteProductService
    {
        Task CreateFavoriteProductAsync(CreateFavoriteProductDto createFavoriteProductDto);
        Task<List<ResultFavoriteProductDto>> GetByUserIdListAsync(string userId);
        Task DeleteFavoriteProductAsync(string id);
    }
}
