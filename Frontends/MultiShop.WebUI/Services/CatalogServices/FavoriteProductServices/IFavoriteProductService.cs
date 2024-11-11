using MultiShop.DtoLayer.CatalogDtos.FavoriteProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FavoriteProductServices
{
    public interface IFavoriteProductService
    {
        Task CreateFavoriteProductAsync(CreateFavoriteProductDto createFavoriteProductDto);
        Task<List<ResultFavoriteProductDto>> GetByUserIdFavoriteProdutAsync(string userId);
        Task DeleteFavoriteProductAsync(string id);
    }
}
